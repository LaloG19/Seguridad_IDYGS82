using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Commands.Comercial;
using ApplicationCore.Commands.Logs;
using ApplicationCore.DTOs.Logs;


namespace Host.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        private readonly IMediator _mediator;
        
        public DashboardController(IDashboardService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
            
        }

        [Route("getData")]
        [HttpGet]

        public async Task<IActionResult> GetUsuarios()
        {
            var result = await _service.GetData();
            return Ok(result);
        }


        /// <summary>
        /// Crea un comercial (Vendedor pues)
        /// </summary>
        /// <returns></returns>

        [HttpPost("Create")]

        public async Task<ActionResult<Response<int>>> Create(CreateComercialCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Obtener la IP del cliente
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetIP")]

        public async Task<IActionResult> GetIp()
        {
            var result = await _service.GetIp();
            return Ok(result);
        }

        /// <summary>
        /// Crear log (Datos de petición HTTP)
        /// </summary>
        /// <returns></returns>

        [HttpPost("CreateLogs")]
        public async Task<ActionResult<Response<int>>> CreateLogs([FromBody] LogsDto request)
        {
            var result = await _service.CreateLogs(request);
            return Ok(result);
        }

        //[HttpGet()]
        //[Authorize]
        //public async Task<IActionResult> GastoPendienteArea()
        //{
        //    var origin = Request.Headers["origin"];
        //    return Ok("test");
        //}


    }
}
