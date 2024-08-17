using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.CarritoCompra.DTOS;
using TiendaServicio.Api.CarritoCompra.InterfazRemota;
using TiendaServicio.Api.CarritoCompra.Modelo;
using TiendaServicio.Api.CarritoCompra.Persistencia;

namespace TiendaServicio.Api.CarritoCompra.Aplicacion
{
    public class Query
    {
        public class Execute : IRequest<CarritoSesionDTO>
        {
            public int CarritoSesionId { get; set; }
        }
        public class Handler : IRequestHandler<Execute, CarritoSesionDTO>
        {
            private readonly ContextoCarritoCompra _contexto;
            private readonly ILibrosService _librosService;


            public Handler(ContextoCarritoCompra context, ILibrosService booksService)
            {
                _contexto = context;
                _librosService = booksService;
            }

            public async Task<CarritoSesionDTO> Handle(Execute request, CancellationToken cancellationToken)
            {
                var carritoSesion = await
                    _contexto.CarritoSesion
                    .FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);

                //control
                if (carritoSesion == null)
                {
                    return null;
                }

                var carritoSesionDetalle = await
                    _contexto.CarritoSesionDetalle
                    .Where(x => x.CarritoSesionId == request.CarritoSesionId)
                    .ToListAsync();


                //we are going to use it for the response
                var listaDetalleDto = new List<CarritoSesionDetalleDTO>();

                foreach (var book in carritoSesionDetalle)
                {
                    //call to service
                    var response = await
                        _librosService.GetLibro(new System.Guid(book.ProductoSeleccionado));

                    if (response.resultado)
                    {
                        //true, we continue
                        var libroRemoto = response.Libro;
                        var CartDetail = new CarritoSesionDetalleDTO
                        {
                            TituloLibro = libroRemoto.Titulo,
                            FechaPublicacion = libroRemoto.FechaPublicacion,
                            LibroId = new Guid(book.ProductoSeleccionado),
                            AutorLibro = libroRemoto.AutorGuid.ToString(),
                        };
                        listaDetalleDto.Add(CartDetail);
                    }
                }
                var carritoDTO = new CarritoSesionDTO
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCompra,
                    ListaProductos = listaDetalleDto
                };

                return carritoDTO;
            }
        }
    }
}
