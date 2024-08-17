using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.CarritoCompra.Modelo;
using TiendaServicio.Api.CarritoCompra.Persistencia;
using TiendaServicio.Api.CarritoCompra.RabbitMq;

namespace TiendaServicio.Api.CarritoCompra.Aplicacion
{
    public class NuevoCarrito
    {
        public class Execute : IRequest
        {
            public DateTime FechaCompra { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {

            private readonly ContextoCarritoCompra _contexto;
            private readonly Cola _cola;

            public Handler(ContextoCarritoCompra context, Cola cola)
            {
                _contexto = context;
                _cola = cola;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                //insert the sessioncart
                var carritoSesion = new CarritoSesion
                {
                    FechaCompra = request.FechaCompra,
                };


                //insert
                _contexto.CarritoSesion.Add(carritoSesion);


                var value = await _contexto.SaveChangesAsync();


                if (value == 0)
                {
                    throw new Exception("Error al insertar carrito sesion");
                }


                //the id of the session cart
                int id = carritoSesion.CarritoSesionId;


                foreach (var producto in request.ProductoLista)
                {
                    var detail = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = producto
                    };
                    _contexto.CarritoSesionDetalle.Add(detail);
                }

                //WE CHANGE VALUE
                value = await _contexto.SaveChangesAsync();

                if (value > 0)
                {
                    await _cola.ProcesarAsync(carritoSesion);
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar");
            }
        }
    }
}
