using BpInterface.Core.Models.Dto;

namespace BpInterface.Core.Interfaces.Services
{
    public interface IClienteService
    {
        Task<ClienteResponse> CreateAsync(ClienteRequestForCreate clienteRequest);
        Task DeleteAsync(string identificacion);
        Task<List<ClienteResponse>> GetAllAsync();
        Task<ClienteResponse?> GetByAsync(string identificacion);
        Task UpdateAsync(ClienteRequestForUpdate clienteRequest);
    }
}
