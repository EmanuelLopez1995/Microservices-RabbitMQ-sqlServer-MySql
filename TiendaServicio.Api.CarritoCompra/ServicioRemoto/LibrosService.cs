using System.Text.Json;
using TiendaServicio.Api.CarritoCompra.InterfazRemota;
using TiendaServicio.Api.CarritoCompra.ModeloRemoto;

namespace TiendaServicio.Api.CarritoCompra.ServicioRemoto
{
    public class LibrosService : ILibrosService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibrosService> _logger;

        public LibrosService(IHttpClientFactory httpClient, ILogger<LibrosService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }


        public async Task<(bool resultado, LibroRemoto Libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            //Creamos el cliente y nos conectamos con el endpoint de AutorLibro para obtener el libro por id
            try
            {
                //we create the connection with books
                var client = _httpClient.CreateClient("LibrosAutores");
                var response = await client.GetAsync($"api/Autorlibro/{LibroId}");
                if (response.IsSuccessStatusCode)
                {
                    //continue here read the content of the response
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };

                    var result = JsonSerializer.Deserialize<LibroRemoto>(content, options);

                    return (true, result, null);

                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
