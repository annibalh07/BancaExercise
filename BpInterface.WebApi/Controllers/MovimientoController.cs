using BpInterface.Core.Interfaces.Services;
using BpInterface.Core.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BpInterface.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        #region Properties
        private readonly IMovimientoService _movimientoService;
        private readonly ILogger<MovimientoController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the Movimiento Controller
        /// </summary>
        public MovimientoController(IMovimientoService movimientoService, ILogger<MovimientoController> logger)
        {
            _movimientoService = movimientoService;
            _logger = logger;
        }
        #endregion

        [HttpGet("{numeroCuenta}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MovimientoReporteResponse>>> GetMovimiento(string numeroCuenta)
        {
            List<MovimientoReporteResponse> movimientoResponse = await _movimientoService.GetByAsync(numeroCuenta);

            return Ok(movimientoResponse);
        }

        [HttpGet("{numeroCuenta}/reporte")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MovimientoReporteResponse>>> GetReporteMovimientos(string numeroCuenta, [FromQuery] MovimientoSearchParams movimientoSearchParams)
        {
            List<MovimientoReporteResponse> movimientoResponse = await _movimientoService.GetReporteByAsync(numeroCuenta, movimientoSearchParams);

            return Ok(movimientoResponse);
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<List<MovimientoReporteResponse>>> DoTransaction([FromBody] MovimientoRequestForCreate movimientoRequest)
        {
            MovimientoReporteResponse createdMovimiento = await _movimientoService.CreateAsync(movimientoRequest);

            var actionName = nameof(GetMovimiento);
            var routeValues = new { createdMovimiento.NumeroCuenta };

            return CreatedAtAction(actionName, routeValues, createdMovimiento);
        }
    }
}
