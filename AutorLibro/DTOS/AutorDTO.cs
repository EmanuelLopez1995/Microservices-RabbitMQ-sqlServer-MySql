namespace TiendaServicio.Api.AutorLibro.DTOS
{
    public class AutorDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string AutorGuid { get; set; }
    }
}
