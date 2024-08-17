using AutorLibro.Modelo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicio.Api.AutorLibro.Aplicacion;
using TiendaServicio.Api.AutorLibro.DTOS;
using static System.Reflection.Metadata.BlobBuilder;

namespace TiendaServicio.Api.AutorLibro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorLibroController : ControllerBase
    {
        //Inyectamos mediator de mediatR para enviar los datos

        private readonly IMediator _mediator;
        public AutorLibroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AgregarAutor")]
        public async Task<ActionResult<Unit>> Create(NuevoAutor.Execute data)
        {
            return await _mediator.Send(data);
        }

        [HttpPost("AgregarLibro")]
        public async Task<ActionResult<Unit>> CreateBook(NuevoLibro.ExecuteBook data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet("GetAllLibros")]
        public async Task<ActionResult<List<LibroDTO>>> GetLibros()
        {
            var query = new ConsultaLibro.ListaLibros();


            var libros = await _mediator.Send(query);


            return Ok(libros);
        }
        [HttpGet("GetLibrosPorAutor")]
        public async Task<ActionResult<List<LibroDTO>>> GetLibrosPorAutor([FromQuery] string autorGuid)
        {
            var query = new ConsultaLibro.ListaLibros
            {
                AutorGuid = autorGuid
            };


            var libros = await _mediator.Send(query);


            return Ok(libros);
        }

        [HttpGet("GetAllAutores")]
        public async Task<ActionResult<List<AutorDTO>>> GetAutores()
        {
            var query = new ConsultaAutor.ListaAutor();


            var autores = await _mediator.Send(query);


            return Ok(autores);
        }


        [HttpGet("GetAutorPorGuid")]
        public async Task<ActionResult<AutorDTO>> GetAutorGuid([FromQuery] string autorGuid)
        {
            var query = new ConsultaAutorFiltro.AutorUnico
            {
                AutorGuid = autorGuid
            };


            var autorUnico = await _mediator.Send(query);


            return Ok(autorUnico);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDTO>> GetAutorLibro(Guid id)
        {
            return await _mediator.Send(new ConsultaLibroFiltro.LibroUnico { LibroId = id });
        }
    }
}
