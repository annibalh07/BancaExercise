using BpInterface.Core.Exceptions;
using BpInterface.Core.Interfaces.Services;
using BpInterface.Core.Models.Dto;
using BpInterface.Infrastructure.Data.Entities;
using BpInterface.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BpInterface.Services.Implementations
{
    public class CuentaService : ICuentaService
    {
        #region Properties
        private readonly IDbRepository<Cliente> _clienteRepository;
        private readonly IDbRepository<Cuenta> _cuentaRepository;
        private readonly IDbRepository<Movimiento> _movimientoRepository;

        #endregion

        #region Constructors

        public CuentaService(IDbRepository<Cuenta> cuentaRepository, IDbRepository<Cliente> clienteRepository, IDbRepository<Movimiento> movimientoRepository)
        {
            _cuentaRepository = cuentaRepository;
            _clienteRepository = clienteRepository;
            _movimientoRepository = movimientoRepository;
        }

        #endregion

        public async Task<CuentaResponse> CreateAsync(CuentaRequestForCreate cuentaRequest)
        {
            var cliente = await _clienteRepository.GetAllIQueryable()
                .Where(t => t.Identificacion == cuentaRequest.IdentificacionCliente).FirstOrDefaultAsync();

            if (cliente == null)
            {
                throw new HttpResponseException
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Msg = $"Cliente con identificación {cuentaRequest.IdentificacionCliente} no encontrado"
                };
            }

            var cuenta = new Cuenta
            {
                ClienteId = cliente.Id,
                Saldo = cuentaRequest.Saldo,
                LimiteDiario = cuentaRequest.LimiteDiario,
                TipoCuenta = cuentaRequest.TipoCuenta,
                NumeroCuenta = cuentaRequest.NumeroCuenta
            };

            await _cuentaRepository.AddAsync(cuenta);
            await _cuentaRepository.SaveChangesAsync();

            return new CuentaResponse
            {
                ClienteNombres = cliente.Nombres,
                Estado = cuenta.Estado,
                LimiteDiario = cuenta.LimiteDiario,
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.Saldo,
                TipoCuenta = cuenta.TipoCuenta
            };
        }

        public async Task DeleteAsync(string numeroCuenta)
        {
            var cuenta = await GetCuenta(numeroCuenta);

            var cuentasConMovimientos = _movimientoRepository.GetAllIQueryable().Where(t => t.NumeroCuenta == cuenta.NumeroCuenta).Count();

            if (cuentasConMovimientos > 0)
            {
                throw new HttpResponseException
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Msg = $"Cuenta {numeroCuenta} no se puede eliminar. Tiene una o más movimientos asociadas"
                };
            }

            _cuentaRepository.Delete(cuenta);

            await _clienteRepository.SaveChangesAsync();
        }

        public async Task<CuentaResponse> GetByAsync(string numeroCuenta)
        {
            var cuenta = await GetCuenta(numeroCuenta);

            var cliente = await _clienteRepository.GetAllIQueryable()
                .Where(t => t.Id == cuenta.ClienteId).FirstOrDefaultAsync();

            return new CuentaResponse
            {
                ClienteNombres = cliente!.Nombres,
                Estado = cuenta.Estado,
                LimiteDiario = cuenta.LimiteDiario,
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.Saldo,
                TipoCuenta = cuenta.TipoCuenta
            };
        }

        public async Task<List<CuentaResponse>> GetByAsync(int clienteId)
        {
            var cuentas = await (from c in _cuentaRepository.GetAllIQueryable()
                                 join cl in _clienteRepository.GetAllIQueryable() on c.ClienteId equals cl.Id
                                 where c.ClienteId == clienteId
                                 select new CuentaResponse
                                 {
                                     ClienteNombres = cl!.Nombres,
                                     Estado = c.Estado,
                                     LimiteDiario = c.LimiteDiario,
                                     NumeroCuenta = c.NumeroCuenta,
                                     SaldoInicial = c.Saldo,
                                     TipoCuenta = c.TipoCuenta
                                 }).ToListAsync();
            return cuentas;
        }

        public async Task UpdateAsync(CuentaRequestForUpdate cuentaRequest)
        {
            var cuenta = await GetCuenta(cuentaRequest.NumeroCuenta);

            cuenta.TipoCuenta = cuentaRequest.TipoCuenta;
            cuenta.Estado = cuentaRequest.Estado;
            cuenta.LimiteDiario = cuentaRequest.LimiteDiario;
            cuenta.Saldo = cuentaRequest.SaldoInicial;

            _cuentaRepository.Update(cuenta);
            await _clienteRepository.SaveChangesAsync();
        }

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
    }
}
