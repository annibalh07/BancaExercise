using BpInterface.Core.Models.Dto;

namespace BpInterface.Core.Interfaces.Services
{
    public interface IMovimientoService
    {
        Task<MovimientoReporteResponse> CreateAsync(MovimientoRequestForCreate movimientoRequest);
        Task<List<MovimientoReporteResponse>> GetByAsync(string numeroCuenta);
        Task<List<MovimientoReporteResponse>> GetReporteByAsync(string numeroCuenta, MovimientoSearchParams movimientoSearchParams);
    }
}
