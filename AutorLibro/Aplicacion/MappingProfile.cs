using AutoMapper;
using AutorLibro.Modelo;
using TiendaServicio.Api.AutorLibro.DTOS;

namespace TiendaServicio.Api.AutorLibro.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Autor, AutorDTO>();
            CreateMap<Libro, LibroDTO>();

        }
    }
}
