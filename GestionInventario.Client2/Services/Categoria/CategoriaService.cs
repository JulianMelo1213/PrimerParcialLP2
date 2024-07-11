using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionInventarios.Shared.DTOs.Categoria;
using GestionInventario.Client2.Services.Categoria;

namespace GestionInventario.Client2.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _httpClient;

        public CategoriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoriaGetDTO>> GetCategoriasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CategoriaGetDTO>>("api/categoriums");
        }

        public async Task<CategoriaGetDTO> GetCategoriaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CategoriaGetDTO>($"api/categoriums/{id}");
        }

        public async Task<bool> CreateCategoriaAsync(CategoriaInsertDTO categoria)
        {
            var response = await _httpClient.PostAsJsonAsync("api/categoriums", categoria);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCategoriaAsync(int id, CategoriaPutDTO categoria)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/categoriums/{id}", categoria);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/categoriums/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
