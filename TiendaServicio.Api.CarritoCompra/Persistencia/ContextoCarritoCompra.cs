using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.CarritoCompra.Modelo;

namespace TiendaServicio.Api.CarritoCompra.Persistencia
{
    public class ContextoCarritoCompra : DbContext
    {
        public ContextoCarritoCompra(DbContextOptions<ContextoCarritoCompra> options) : base(options) { }
        //add the dbsets
        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }
    }
}
