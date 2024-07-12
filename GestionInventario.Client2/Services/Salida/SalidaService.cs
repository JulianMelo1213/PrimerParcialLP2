using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionInventarios.Shared.DTOs.Salida;
using GestionInventarios.Shared.DTOs.Producto;
using GestionInventario.Client2.Services.Salida;

namespace GestionInventario.Client2.Services
{
    public class SalidaService : ISalidaService
    {
        private readonly HttpClient _httpClient;

        public SalidaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SalidaGetDTO>> GetSalidasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<SalidaGetDTO>>("api/salidums");
        }

        public async Task<SalidaGetDTO> GetSalidaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<SalidaGetDTO>($"api/salidums/{id}");
        }

        public async Task<bool> CreateSalidaAsync(SalidaInsertDTO salida)
        {
            var response = await _httpClient.PostAsJsonAsync("api/salidums", salida);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateSalidaAsync(int id, SalidaPutDTO salida)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/salidums/{id}", salida);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSalidaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/salidums/{id}");
            return response.IsSuccessStatusCode;
        }

        // Implementación del nuevo método
        public async Task<List<ProductoGetDTO>> GetProductosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoGetDTO>>("api/productos");
        }
    }
}
