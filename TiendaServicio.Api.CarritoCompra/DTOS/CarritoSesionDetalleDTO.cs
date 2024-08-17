namespace TiendaServicio.Api.CarritoCompra.DTOS
{
    public class CarritoSesionDetalleDTO
    {
        public Guid? LibroId { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public DateTime? FechaPublicacion { get; set; }
    }
}
