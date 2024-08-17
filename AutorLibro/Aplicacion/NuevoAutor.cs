using AutorLibro.Modelo;
using AutorLibro.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static TiendaServicio.Api.AutorLibro.Aplicacion.NuevoAutor;
using System.Drawing;

namespace TiendaServicio.Api.AutorLibro.Aplicacion
{
    public class NuevoAutor
    { 
        public class Execute : IRequest
        {
            //Estos son los parametros que va a recibir la request
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            public readonly ContextoAutorLibro _contexto;
            public Handler(ContextoAutorLibro context)
            {
                _contexto = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                //Creamos el nuevo autor
                var autor = new Autor
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorGuid = Guid.NewGuid().ToString(),
                };


                //Se usa el contexto para agregar el autor
                _contexto.Autores.Add(autor);


                var valor = await _contexto.SaveChangesAsync();
                if (valor > 0)
                {
                    return Unit.Value;
                }


                //Si hay errores
                throw new Exception("No se pudo insertar el autor");
            }
        }
    }
}
