
namespace AutorLibro.Modelo
{
    public class Autor
    {
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string AutorGuid { get; set; }
        public ICollection<GradoAcademico> ListaGradoAcademico { get; set; }
        public ICollection<Libro> ListaLibro { get; set; }
    }
}
