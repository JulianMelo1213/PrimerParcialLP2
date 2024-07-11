using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionInventarios.Shared.DTOs.Almacen;
using GestionInventario.Client2.Services.Almacen;

namespace GestionInventario.Client2.Services
{
    public class AlmacenService : IAlmacenService
    {
        private readonly HttpClient _httpClient;

        public AlmacenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AlmacenGetDTO>> GetAlmacenesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AlmacenGetDTO>>("api/almacens");
        }

        public async Task<AlmacenGetDTO> GetAlmacenByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AlmacenGetDTO>($"api/almacens/{id}");
        }

        public async Task<bool> CreateAlmacenAsync(AlmacenInsertDTO almacen)
        {
            var response = await _httpClient.PostAsJsonAsync("api/almacens", almacen);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAlmacenAsync(int id, AlmacenPutDTO almacen)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/almacens/{id}", almacen);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAlmacenAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/almacens/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
