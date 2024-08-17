using AutorLibro.Modelo;
using Microsoft.EntityFrameworkCore;

namespace AutorLibro.Persistencia
{
    public class ContextoAutorLibro : DbContext
    {
        public ContextoAutorLibro(DbContextOptions<ContextoAutorLibro> options) : base(options) { }

        //Deberemos ir inyectando todas las clases que se agreguen con ese nombre se creará la tabla en la BD
        public DbSet<Autor> Autores { get; set; }
        public DbSet<GradoAcademico> GradosAcademicos { get; set; }
        public DbSet<Libro> Libros { get; set; }

    }
}
