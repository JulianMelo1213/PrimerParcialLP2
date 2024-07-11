using GestionInventarios.Shared.DTOs.Producto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionInventario.Client2.Services.Producto
{
    public interface IProductoService
    {
        Task<List<ProductoGetDTO>> GetProductosAsync();
        Task<ProductoGetDTO> GetProductoByIdAsync(int id);
        Task<bool> CreateProductoAsync(ProductoInsertDTO producto);
        Task<bool> UpdateProductoAsync(int id, ProductoPutDTO producto);
        Task<bool> DeleteProductoAsync(int id);
    }
}
