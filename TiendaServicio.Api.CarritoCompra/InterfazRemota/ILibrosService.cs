using TiendaServicio.Api.CarritoCompra.ModeloRemoto;

namespace TiendaServicio.Api.CarritoCompra.InterfazRemota
{
    public interface ILibrosService
    {
        Task<(bool resultado, LibroRemoto Libro, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}
