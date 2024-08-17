using AutorLibro.Modelo;
using TiendaServicio.Api.AutorLibro.DTOS;
using AutorLibro.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace TiendaServicio.Api.AutorLibro.Aplicacion
{
    public class ConsultaAutorFiltro
    {
        public class AutorUnico : IRequest<AutorDTO>
        {
            public string AutorGuid { get; set; }
        }


        public class Handler : IRequestHandler<AutorUnico, AutorDTO>
        {
            public readonly ContextoAutorLibro _contexto;
            private readonly IMapper _mapper;
            public Handler(ContextoAutorLibro contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }


            public async Task<AutorDTO> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                //retorna el autor por guid
                var autor = await _contexto.Autores
                    .Where(x => x.AutorGuid == request.AutorGuid)
                    .FirstOrDefaultAsync();
                var autorDto = _mapper.Map<Autor, AutorDTO>(autor);
                return autorDto;
            }
        }
    }
}
