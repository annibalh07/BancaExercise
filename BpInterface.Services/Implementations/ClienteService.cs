using BpInterface.Core.Exceptions;
using BpInterface.Core.Interfaces.Services;
using BpInterface.Core.Models.Dto;
using BpInterface.Infrastructure.Data.Entities;
using BpInterface.Infrastructure.Repositories;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace BpInterface.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        #region Properties
        private readonly IDbRepository<Cliente> _clienteRepository;
        private readonly IDbRepository<Cuenta> _cuentaRepository;

        #endregion

        #region Constructors
        public ClienteService(IDbRepository<Cliente> clienteRepository, IDbRepository<Cuenta> cuentaRepository)
        {
            _clienteRepository = clienteRepository;
            _cuentaRepository = cuentaRepository;
        }
        #endregion

        public async Task<ClienteResponse> CreateAsync(ClienteRequestForCreate clienteRequest)
        {
            var cliente = new Cliente
            {                
                Identificacion = clienteRequest.Identificacion,
                Contrasenia = clienteRequest.Contrasenia,
                Direccion = clienteRequest.Direccion,
                Edad = clienteRequest.Edad,
                Genero = clienteRequest.Genero,
                Nombres = clienteRequest.Nombres,
                Telefono = clienteRequest.Telefono
            };

            await _clienteRepository.AddAsync(cliente);
            await _clienteRepository.SaveChangesAsync();

            return new ClienteResponse
            {
                Id = cliente.Id,
                Identificacion = cliente.Identificacion,
                Clave = cliente.Contrasenia,
                Direccion = cliente.Direccion,
                Edad = cliente.Edad,
                Estado = cliente.Estado,
                Genero = cliente.Genero,
                Nombres = cliente.Nombres,
                Telefono = cliente.Telefono
            };
        }

        public async Task DeleteAsync(string identificacion)
        {
            var cliente = await _clienteRepository.GetAllIQueryable().Where(t => t.Identificacion == identificacion).FirstOrDefaultAsync();
            
            ApplyDeleteValidations(identificacion, cliente);

            _clienteRepository.Delete(cliente!);
            await _clienteRepository.SaveChangesAsync();
        }

        public async Task<List<ClienteResponse>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllIQueryable().ToListAsync();

            List<ClienteResponse> clientesResponse = clientes.Select(cliente => new ClienteResponse
            {
                Id = cliente.Id,
                Identificacion = cliente.Identificacion,
                Clave = cliente.Contrasenia,
                Direccion = cliente.Direccion,
                Edad = cliente.Edad,
                Estado = cliente.Estado,
                Genero = cliente.Genero,
                Nombres = cliente.Nombres,
                Telefono = cliente.Telefono
            }).ToList();

            return clientesResponse;
        }

        public async Task<ClienteResponse?> GetByAsync(string identificacion)
        {
            var cliente = await _clienteRepository.GetAllIQueryable().Where(t => t.Identificacion == identificacion).FirstOrDefaultAsync();

            if (cliente == null) return null;

            return new ClienteResponse
            {
                Id = cliente.Id,
                Identificacion = cliente.Identificacion,
                Clave = cliente.Contrasenia,
                Direccion = cliente.Direccion,
                Edad = cliente.Edad,
                Estado = cliente.Estado,
                Genero = cliente.Genero,
                Nombres = cliente.Nombres,
                Telefono = cliente.Telefono
            };
        }

        public async Task UpdateAsync(ClienteRequestForUpdate clienteRequest)
        {
            var cliente = await _clienteRepository.GetAllIQueryable().Where(t => t.Identificacion == clienteRequest.Identificacion).FirstOrDefaultAsync();

            if (cliente == null)
            {
                throw new HttpResponseException
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Msg = $"Cliente con identificación {clienteRequest.Identificacion} no encontrado"
                };
            }

            cliente.Identificacion = clienteRequest.Identificacion;
            cliente.Estado = clienteRequest.Estado;
            cliente.Direccion = clienteRequest.Direccion;
            cliente.Edad = clienteRequest.Edad;
            cliente.Genero = clienteRequest.Genero;
            cliente.Nombres = clienteRequest.Nombres;

            _clienteRepository.Update(cliente);
            await _clienteRepository.SaveChangesAsync();
        }

        #region Private Methods

        private void ApplyDeleteValidations(string identificacion, Cliente? cliente)
        {
            if (cliente == null)
            {
                throw new HttpResponseException
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Msg = $"Cliente con identificación {identificacion} no encontrado"
                };
            }

            var cuentasAsociadas = _cuentaRepository.GetAllIQueryable().Where(t => t.ClienteId == cliente.Id).Count();

            if (cuentasAsociadas > 0)
            {
                throw new HttpResponseException
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Msg = $"Cliente con identificación {identificacion} no se puede eliminar. Tiene una o más cuentas asociadas"
                };
            }
        }

        #endregion
    }
}
