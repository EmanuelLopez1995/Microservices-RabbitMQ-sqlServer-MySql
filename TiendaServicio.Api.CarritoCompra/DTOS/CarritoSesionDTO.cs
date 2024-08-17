namespace TiendaServicio.Api.CarritoCompra.DTOS
{
    public class CarritoSesionDTO
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoSesionDetalleDTO> ListaProductos { get; set; }
    }
}
