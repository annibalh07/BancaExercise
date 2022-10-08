using BpInterface.Core.Interfaces.Services;
using BpInterface.Core.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BpInterface.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        #region Properties
        private readonly ICuentaService _cuentaService;
        private readonly ILogger<CuentaController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the Cuenta Controller
        /// </summary>
        public CuentaController(ICuentaService cuentaService, ILogger<CuentaController> logger)
        {
            _cuentaService = cuentaService;
            _logger = logger;
        }
        #endregion

        [HttpGet("{numeroCuenta}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CuentaResponse>> GetCuenta(string numeroCuenta)
        {
            CuentaResponse clienteDto = await _cuentaService.GetByAsync(numeroCuenta);

            if (clienteDto != null) return Ok(clienteDto);

            return NotFound();
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CuentaResponse>> CreateCuenta([FromBody] CuentaRequestForCreate cuentaRequest)
        {
            CuentaResponse createdCuenta = await _cuentaService.CreateAsync(cuentaRequest);

            var actionName = nameof(GetCuenta);
            var routeValues = new { createdCuenta.NumeroCuenta };

            return CreatedAtAction(actionName, routeValues, createdCuenta);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCuenta([FromBody] CuentaRequestForUpdate cuentaRequest)
        {
            await _cuentaService.UpdateAsync(cuentaRequest);

            return NoContent();
        }

        [HttpDelete("{numeroCuenta}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCuenta(string numeroCuenta)
        {
            await _cuentaService.DeleteAsync(numeroCuenta);

            return NoContent();
        }
    }
}
