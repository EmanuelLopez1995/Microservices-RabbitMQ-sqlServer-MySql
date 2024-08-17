using AutorLibro.Modelo;
using AutorLibro.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace TiendaServicio.Api.AutorLibro.Aplicacion
{
    public class NuevoLibro
    {
        public class ExecuteBook : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public string AutorGuid { get; set; }
        }
        public class Handler : IRequestHandler<ExecuteBook>
        {
            public readonly ContextoAutorLibro _contexto;


            public Handler(ContextoAutorLibro context)
            {
                _contexto = context;
            }


            public async Task<Unit> Handle(ExecuteBook request, CancellationToken cancellationToken)
            {
                var libro = new Libro
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    Descripcion = request.Descripcion,
                    AutorGuid = request.AutorGuid,
                    LibroGuid = Guid.NewGuid().ToString(),
                    AutorId = _contexto.Autores.FirstOrDefault(a => a.AutorGuid == request.AutorGuid).AutorId,
                };

                _contexto.Libros.Add(libro);

                var valor = await _contexto.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }


                //if there were any errors
                throw new Exception("Could not insert book");
            }
        }
    }
}
