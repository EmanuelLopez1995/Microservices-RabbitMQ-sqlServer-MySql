using AutorLibro.Modelo;
using AutorLibro.Persistencia;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicio.Api.AutorLibro.DTOS;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace TiendaServicio.Api.AutorLibro.Aplicacion
{
    public class ConsultaLibro
    {
        public class ListaLibros : IRequest<List<LibroDTO>>
        {
            //  Si recibe el AutorGuid va a traer los libros del autor
            public string? AutorGuid { get; set; }
        }
        public class Handler : IRequestHandler<ListaLibros, List<LibroDTO>>
        {
            public readonly ContextoAutorLibro _contexto;
            private readonly IMapper _mapper;
            public Handler(ContextoAutorLibro contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }


            public async Task<List<LibroDTO>> Handle(ListaLibros request, CancellationToken cancellationToken)
            {
                if (!string.IsNullOrEmpty(request.AutorGuid))
                {
                    List<Libro> libros= await _contexto.Libros
                        .Where(libro => libro.AutorGuid == request.AutorGuid)
                        .ToListAsync();
                    return _mapper.Map<List<Libro>, List<LibroDTO>>(libros);
                }
                else
                {
                    List<Libro> libros = await _contexto.Libros.ToListAsync();
                    return _mapper.Map<List<Libro>, List<LibroDTO>>(libros);

                }
            }

        }

    }
}
