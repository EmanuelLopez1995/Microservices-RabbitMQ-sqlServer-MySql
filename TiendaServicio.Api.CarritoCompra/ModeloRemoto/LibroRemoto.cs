namespace TiendaServicio.Api.CarritoCompra.ModeloRemoto
{
    public class LibroRemoto
    {
        public Guid? LibroId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorGuid { get; set; }
        public string Descripcion { get; set; }
    }
}
