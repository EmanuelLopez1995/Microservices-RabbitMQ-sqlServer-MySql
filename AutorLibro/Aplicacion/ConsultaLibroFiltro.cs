using AutoMapper;
using AutorLibro.Modelo;
using AutorLibro.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.AutorLibro.DTOS;

namespace TiendaServicio.Api.AutorLibro.Aplicacion
{
    public class ConsultaLibroFiltro
    {
        public class LibroUnico : IRequest<LibroDTO>
        {
            public Guid? LibroId { get; set; }
        }
        public class Handler : IRequestHandler<LibroUnico, LibroDTO>
        {
            public readonly ContextoAutorLibro _contexto;
            private readonly IMapper _mapper;

            public Handler(ContextoAutorLibro context, IMapper mapper)
            {
                _contexto = context;
                _mapper = mapper;
            }

            public async Task<LibroDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.Libros
                    .Where(x => x.LibroGuid == request.LibroId.ToString())
                    .FirstOrDefaultAsync();

                // mapper
                var DTOBook = _mapper.Map<Libro, LibroDTO>(libro);
                if (DTOBook == null)
                {
                    throw new Exception("No book found");
                }

                return DTOBook;

            }
        }
    }
}
