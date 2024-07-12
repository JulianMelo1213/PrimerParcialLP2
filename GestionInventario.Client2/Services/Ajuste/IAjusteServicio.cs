using GestionInventarios.Shared.DTOs.Ajuste;
using GestionInventarios.Shared.DTOs.Producto;
using GestionInventarios.Shared.DTOs.Almacen;

namespace GestionInventario.Client2.Services.Ajuste
{
    public interface IAjusteServicio
    {
        Task<List<AjusteGetDTO>> GetAjustesAsync();
        Task<AjusteGetDTO> GetAjusteByIdAsync(int id);
        Task<bool> CreateAjusteAsync(AjusteInsertDTO ajuste);
        Task<bool> UpdateAjusteAsync(int id, AjustePutDTO ajuste);
        Task<bool> DeleteAjusteAsync(int id);

        // Nuevos métodos
        Task<List<ProductoGetDTO>> GetProductosAsync();
        Task<List<AlmacenGetDTO>> GetAlmacenesAsync();
    }
}