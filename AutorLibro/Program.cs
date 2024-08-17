using AutorLibro.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.AutorLibro.Aplicacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ContextoAutorLibro>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDatabase"));
});

builder.Services.AddMediatR(typeof(NuevoAutor.Handler).Assembly);
builder.Services.AddMediatR(typeof(NuevoLibro.Handler).Assembly);
builder.Services.AddAutoMapper(typeof(ConsultaAutor.Handler));
builder.Services.AddAutoMapper(typeof(ConsultaAutorFiltro.Handler));
builder.Services.AddAutoMapper(typeof(ConsultaLibro.Handler));
builder.Services.AddAutoMapper(typeof(ConsultaLibroFiltro.Handler));




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
