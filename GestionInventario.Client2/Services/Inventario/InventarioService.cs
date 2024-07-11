using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionInventarios.Shared.DTOs.Inventario;
using GestionInventario.Client2.Services.Inventario;

namespace GestionInventario.Client2.Services
{
    public class InventarioService : IInventarioService
    {
        private readonly HttpClient _httpClient;

        public InventarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<InventarioGetDTO>> GetInventariosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<InventarioGetDTO>>("api/inventarios");
        }

        public async Task<InventarioGetDTO> GetInventarioByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<InventarioGetDTO>($"api/inventarios/{id}");
        }

        public async Task<bool> CreateInventarioAsync(InventarioInsertDTO inventario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/inventarios", inventario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateInventarioAsync(int id, InventarioPutDTO inventario)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/inventarios/{id}", inventario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteInventarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/inventarios/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
