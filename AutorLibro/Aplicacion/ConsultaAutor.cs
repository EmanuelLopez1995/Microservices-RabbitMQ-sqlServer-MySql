using AutorLibro.Modelo;
using TiendaServicio.Api.AutorLibro.DTOS;
using AutorLibro.Persistencia;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static TiendaServicio.Api.AutorLibro.Aplicacion.ConsultaAutor;
using AutoMapper;

namespace TiendaServicio.Api.AutorLibro.Aplicacion
{
    public class ConsultaAutor
    {
        public class ListaAutor : IRequest<List<AutorDTO>>
        {
            //No recibe ningun parametro en la request 
        }


        public class Handler : IRequestHandler<ListaAutor, List<AutorDTO>>
        {
            public readonly ContextoAutorLibro _contexto;
            private readonly IMapper _mapper;
            public Handler(ContextoAutorLibro contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }


            public async Task<List<AutorDTO>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await _contexto.Autores.ToListAsync();
                var autoresDtos = _mapper.Map<List<Autor>, List<AutorDTO>>(autores);
                return autoresDtos;
            }
        }
    }
}
