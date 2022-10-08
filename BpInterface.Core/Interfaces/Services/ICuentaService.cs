using BpInterface.Core.Models.Dto;

namespace BpInterface.Core.Interfaces.Services
{
    public interface ICuentaService
    {
        Task<CuentaResponse> CreateAsync(CuentaRequestForCreate cuentaRequest);
        Task DeleteAsync(string numeroCuenta);
        Task<CuentaResponse> GetByAsync(string numeroCuenta);
        Task UpdateAsync(CuentaRequestForUpdate cuentaRequest);
        Task<List<CuentaResponse>> GetByAsync(int clienteId);
    }
}
