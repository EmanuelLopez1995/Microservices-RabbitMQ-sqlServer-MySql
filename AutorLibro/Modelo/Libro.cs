namespace AutorLibro.Modelo
{
    public class Libro
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; }
        public string AutorGuid { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Descripcion { get; set; }
        public string LibroGuid { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
