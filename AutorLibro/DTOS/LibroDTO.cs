namespace TiendaServicio.Api.AutorLibro.DTOS
{
    public class LibroDTO
    {
        public string Titulo { get; set; }
        public string AutorGuid { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Descripcion { get; set; }
        public string LibroGuid { get; set; }
    }
}
