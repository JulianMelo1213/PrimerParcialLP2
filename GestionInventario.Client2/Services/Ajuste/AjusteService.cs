using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestionInventarios.Shared.DTOs.Ajuste;
using GestionInventario.Client2.Services.Ajuste;

namespace GestionInventario.Client2.Services
{
    public class AjusteService : IAjusteServicio
    {
        private readonly HttpClient _httpClient;

        public AjusteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AjusteGetDTO>> GetAjustesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AjusteGetDTO>>("api/ajustes");
        }

        public async Task<AjusteGetDTO> GetAjusteByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AjusteGetDTO>($"api/ajustes/{id}");
        }

        public async Task<bool> CreateAjusteAsync(AjusteInsertDTO ajuste)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ajustes", ajuste);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAjusteAsync(int id, AjustePutDTO ajuste)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ajustes/{id}", ajuste);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAjusteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ajustes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
