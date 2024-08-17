using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.CarritoCompra.Aplicacion;
using TiendaServicio.Api.CarritoCompra.InterfazRemota;
using TiendaServicio.Api.CarritoCompra.Persistencia;
using TiendaServicio.Api.CarritoCompra.RabbitMq;
using TiendaServicio.Api.CarritoCompra.ServicioRemoto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextoCarritoCompra>(option =>
{
    option.UseMySQL(builder.Configuration.GetConnectionString("ConexionDataBase"));
});

builder.Services.AddMediatR(typeof(NuevoCarrito.Handler).Assembly);

// usamos HttpClient  para comunicarnos con el otro microservicio
builder.Services.AddHttpClient("LibrosAutores", config =>
{
    //Nos conectamos con la url que definimos en la configuracion de appsettings
    config.BaseAddress = new Uri(builder.Configuration["Services:LibrosAutores"]);
});

builder.Services.AddScoped<ILibrosService, LibrosService>();

builder.Services.AddSingleton<Cola>();

// Add services to the container.

builder.Services.AddControllers();
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
