using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionInventarios.Shared.DTOs.Producto;
using GestionInventario.Client2.Services.Producto;

namespace GestionInventario.Client2.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductoGetDTO>> GetProductosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductoGetDTO>>("api/productos");
        }

        public async Task<ProductoGetDTO> GetProductoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductoGetDTO>($"api/productos/{id}");
        }

        public async Task<bool> CreateProductoAsync(ProductoInsertDTO producto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/productos", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductoAsync(int id, ProductoPutDTO producto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/productos/{id}", producto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/productos/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
