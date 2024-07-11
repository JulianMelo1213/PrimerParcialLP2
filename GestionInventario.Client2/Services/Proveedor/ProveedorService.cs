using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionInventarios.Shared.DTOs.Proveedor;
using GestionInventario.Client2.Services.Proveedor;

namespace GestionInventario.Client2.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly HttpClient _httpClient;

        public ProveedorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProveedorGetDTO>> GetProveedoresAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProveedorGetDTO>>("api/proveedors");
        }

        public async Task<ProveedorGetDTO> GetProveedorByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProveedorGetDTO>($"api/proveedors/{id}");
        }

        public async Task<bool> CreateProveedorAsync(ProveedorInsertDTO proveedor)
        {
            var response = await _httpClient.PostAsJsonAsync("api/proveedors", proveedor);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProveedorAsync(int id, ProveedorPutDTO proveedor)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/proveedors/{id}", proveedor);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProveedorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/proveedors/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
