using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicio.Api.CarritoCompra.Aplicacion;
using TiendaServicio.Api.CarritoCompra.DTOS;

namespace TiendaServicio.Api.CarritoCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarritoCompraController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(NuevoCarrito.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("id")]
        public async Task<ActionResult<CarritoSesionDTO>> GetCarrito(int id) 
        {
            return await _mediator.Send(new Query.Execute { CarritoSesionId = id });    
        }
    }
}
