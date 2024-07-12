using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionInventarios.Shared.DTOs.Entrada;
using GestionInventarios.Shared.DTOs.Producto;
using GestionInventario.Client2.Services.Entrada;

namespace GestionInventario.Client2.Services
{
    public class EntradaService : IEntradaService
    {
        private readonly HttpClient _httpClient;

        public EntradaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EntradaGetDTO>> GetEntradasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EntradaGetDTO>>("api/entradums");
        }

        public async Task<EntradaGetDTO> GetEntradaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EntradaGetDTO>($"api/entradums/{id}");
        }

        public async Task<bool> CreateEntradaAsync(EntradaInsertDTO entrada)
        {
            var response = await _httpClient.PostAsJsonAsync("api/entradums", entrada);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEntradaAsync(int id, EntradaPutDTO entrada)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/entradums/{id}", entrada);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteEntradaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/entradums/{id}");
            return response.IsSuccessStatusCode;
        }

        // Implementación del nuevo método
        public async Task<List<ProductoGetDTO>> GetProductosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoGetDTO>>("api/productos");
        }
    }
}
