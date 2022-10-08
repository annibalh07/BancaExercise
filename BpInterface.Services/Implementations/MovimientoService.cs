using BpInterface.Core;
using BpInterface.Core.Exceptions;
using BpInterface.Core.Interfaces.Services;
using BpInterface.Core.Models.Dto;
using BpInterface.Infrastructure.Data.Entities;
using BpInterface.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BpInterface.Services.Implementations
{
    public class MovimientoService : IMovimientoService
    {
        #region Properties
        private readonly IDbRepository<Movimiento> _movimientoRepository;
        private readonly IDbRepository<Cliente> _clienteRepository;
        private readonly IDbRepository<Cuenta> _cuentaRepository;

        #endregion

        #region Constructors
        public MovimientoService(IDbRepository<Movimiento> movimientoRepository, IDbRepository<Cliente> clienteRepository, IDbRepository<Cuenta> cuentaRepository)
        {
            _movimientoRepository = movimientoRepository;
            _clienteRepository = clienteRepository;
            _cuentaRepository = cuentaRepository;
        }
        #endregion

        public async Task<MovimientoReporteResponse> CreateAsync(MovimientoRequestForCreate movimientoRequest)
        {
            var cuenta = await GetCuenta(movimientoRequest.NumeroCuenta);

            await ApplyValidations(movimientoRequest, cuenta);

            var saldoInicial = cuenta.Saldo;
            var movimientoValor = movimientoRequest.TipoMovimiento == TipoMovimiento.Retiro ? movimientoRequest.Valor * -1 : movimientoRequest.Valor;
            cuenta.Saldo += movimientoValor;
            
            _cuentaRepository.Update(cuenta);
            await _cuentaRepository.SaveChangesAsync();

            var movimiento = new Movimiento
            {
                SaldoInicial = saldoInicial,
                Fecha = movimientoRequest.Fecha,
                Valor = movimientoValor,
                NumeroCuenta = movimientoRequest.NumeroCuenta,
                TipoMovimiento = movimientoRequest.TipoMovimiento
            };

            await _movimientoRepository.AddAsync(movimiento);
            await _clienteRepository.SaveChangesAsync();

            var clienteNombres = await _clienteRepository.GetAllIQueryable().Where(t => t.Id == cuenta.ClienteId).Select(t => t.Nombres).FirstOrDefaultAsync();

            return new MovimientoReporteResponse
            {
                Fecha = movimiento.Fecha,
                TipoCuenta = cuenta.TipoCuenta,
                SaldoInicial = movimiento.SaldoInicial,
                Movimiento = movimiento.Valor,
                NumeroComprobante = movimiento.NumeroComprobante,
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoDisponible = cuenta.Saldo,
                NombreCliente = clienteNombres!
            };
        }

        public async Task<List<MovimientoReporteResponse>> GetByAsync(string numeroCuenta)
        {
            List<MovimientoReporteResponse> movimientos = await (from m in _movimientoRepository.GetAllIQueryable()
                                                                 join cu in _cuentaRepository.GetAllIQueryable() on m.NumeroCuenta equals cu.NumeroCuenta
                                                                 join cl in _clienteRepository.GetAllIQueryable() on cu.ClienteId equals cl.Id
                                                                 where m.NumeroCuenta == numeroCuenta
                                                                 select new MovimientoReporteResponse
                                                                 {
                                                                     Fecha = m.Fecha,
                                                                     TipoCuenta = cu.TipoCuenta,
                                                                     SaldoInicial = m.SaldoInicial,
                                                                     Movimiento = m.Valor,
                                                                     NumeroComprobante = m.NumeroComprobante,
                                                                     NumeroCuenta = cu.NumeroCuenta,
                                                                     SaldoDisponible = m.SaldoInicial + m.Valor,
                                                                     NombreCliente = cl.Nombres
                                                                 }
                ).ToListAsync();
            
            return movimientos;
        }

        public async Task<List<MovimientoReporteResponse>> GetReporteByAsync(string numeroCuenta, MovimientoSearchParams movimientoSearchParams)
        {
            List<MovimientoReporteResponse> movimientos = await (from m in _movimientoRepository.GetAllIQueryable()
                                                                 join cu in _cuentaRepository.GetAllIQueryable() on m.NumeroCuenta equals cu.NumeroCuenta
                                                                 join cl in _clienteRepository.GetAllIQueryable() on cu.ClienteId equals cl.Id
                                                                 where m.NumeroCuenta == numeroCuenta && m.Fecha.Date >= movimientoSearchParams.FechaInicio.Date && m.Fecha.Date <= movimientoSearchParams.FechaFin.Date
                                                                 select new MovimientoReporteResponse
                                                                 {
                                                                     Fecha = m.Fecha,
                                                                     TipoCuenta = cu.TipoCuenta,
                                                                     SaldoInicial = m.SaldoInicial,
                                                                     Movimiento = m.Valor,
                                                                     NumeroComprobante = m.NumeroComprobante,
                                                                     NumeroCuenta = cu.NumeroCuenta,
                                                                     SaldoDisponible = m.SaldoInicial + m.Valor,
                                                                     NombreCliente = cl.Nombres
                                                                 }
                ).ToListAsync();

            return movimientos;
        }

        #region Prievate Methods

        private async Task<Cuenta> GetCuenta(string numeroCuenta)
        {
            var cuenta = await _cuentaRepository.GetAllIQueryable().Where(t => t.NumeroCuenta == numeroCuenta).FirstOrDefaultAsync();

            if (cuenta == null)
            {
                throw new HttpResponseException
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Msg = $"Cuenta no existe. NumeroCuenta: {numeroCuenta}"
                };
            }

            return cuenta;
        }

        private async Task ApplyValidations(MovimientoRequestForCreate movimientoRequest, Cuenta cuenta)
        {
            if (movimientoRequest.TipoMovimiento == TipoMovimiento.Retiro)
            {
                if ((cuenta.Saldo - movimientoRequest.Valor) < 0)
                {
                    throw new HttpResponseException
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Msg = $"Saldo no disponible"
                    };
                };

                decimal totalRetiroDiario = await _movimientoRepository
                .GetAllIQueryable()
                .Where(t => t.TipoMovimiento == TipoMovimiento.Retiro && t.Fecha.Date == DateTime.Today.Date)
                .SumAsync(t => t.Valor);

                if (Math.Abs(totalRetiroDiario) + movimientoRequest.Valor > cuenta.LimiteDiario)
                {
                    throw new HttpResponseException
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Msg = $"Cupo diario Excedido"
                    };
                }
            }
        }

        #endregion
    }
}
