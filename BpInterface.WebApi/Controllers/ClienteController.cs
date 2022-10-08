using BpInterface.Core.Interfaces.Services;
using BpInterface.Core.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BpInterface.WebApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        #region Properties
        private readonly IClienteService _clientService;
        private readonly ICuentaService _cuentaService;
        private readonly ILogger<ClienteController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the Cliente Controller
        /// </summary>
        public ClienteController(IClienteService clientService, ILogger<ClienteController> logger, ICuentaService cuentaService)
        {
            _clientService = clientService;
            _logger = logger;
            _cuentaService = cuentaService;
        }
        #endregion

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ClienteResponse>>> GetClientes()
        {
            List<ClienteResponse> clientesResult = await _clientService.GetAllAsync();

            return Ok(clientesResult);
        }

        [HttpGet("{clientId}/cuentas")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CuentaResponse>>> GetCuentas(int clientId)
        {
            List<CuentaResponse> cuentasRespose = await _cuentaService.GetByAsync(clientId);

            return Ok(cuentasRespose);
        }

        [HttpGet("{identificacion}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteResponse>> GetCliente(string identificacion)
        {
            ClienteResponse? clienteResult = await _clientService.GetByAsync(identificacion);
                      
            if (clienteResult != null) return Ok(clienteResult);
            
            return NotFound();
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ClienteResponse>> CreateCliente([FromBody] ClienteRequestForCreate clienteRequest)
        {
            ClienteResponse createdClient = await _clientService.CreateAsync(clienteRequest);

            var actionName = nameof(GetCliente);
            var routeValues = new { createdClient.Identificacion };

            return CreatedAtAction(actionName, routeValues, createdClient);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCliente([FromBody] ClienteRequestForUpdate clienteRequest)
        {
            await _clientService.UpdateAsync(clienteRequest);

            return NoContent();
        }

        [HttpDelete("{identificacion}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCliente(string identificacion)
        {
            await _clientService.DeleteAsync(identificacion);

            return NoContent();
        }
    }
}
