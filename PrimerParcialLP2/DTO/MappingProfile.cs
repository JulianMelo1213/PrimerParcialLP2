using AutoMapper;
using PrimerParcialLP2.DTO.Ajuste;
using PrimerParcialLP2.DTO.Almacen;
using PrimerParcialLP2.DTO.Categoria;
using PrimerParcialLP2.DTO.Entrada;
using PrimerParcialLP2.DTO.Inventario;
using PrimerParcialLP2.DTO.Producto;
using PrimerParcialLP2.DTO.Proveedor;
using PrimerParcialLP2.DTO.Salida;

using PrimerParcialLP2.Models;

namespace PrimerParcialLP2.DTO
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeos de Producto
            CreateMap<ProductoGetDTO, Models.Producto>().ReverseMap();
            CreateMap<ProductoPutDTO, Models.Producto>().ReverseMap();
            CreateMap<ProductoInsertDTO, Models.Producto>().ReverseMap();


            // Mapeos de Proveedor
            CreateMap<ProveedorGetDTO, Models.Proveedor>().ReverseMap();
            CreateMap<ProveedorPutDTO, Models.Proveedor>().ReverseMap();
            CreateMap<ProveedorInsertDTO, Models.Proveedor>().ReverseMap();


            // Mapeos de Almacen
            CreateMap<AlmacenGetDTO, Models.Almacen>().ReverseMap();
            CreateMap<AlmacenPutDTO, Models.Almacen>().ReverseMap();
            CreateMap<AlmacenInsertDTO, Models.Almacen>().ReverseMap();


            // Mapeos de Categoria
            CreateMap<CategoriaGetDTO, Categorium>().ReverseMap();
            CreateMap<CategoriaPutDTO, Categorium>().ReverseMap();
            CreateMap<CategoriaInsertDTO, Categorium>().ReverseMap();



            // Mapeos de Entrada
            CreateMap<EntradaGetDTO, Entradum>().ReverseMap();
            CreateMap<EntradaPutDTO, Entradum>().ReverseMap();
            CreateMap<EntradaInsertDTO, Entradum>().ReverseMap();



            // Mapeos de Salida
            CreateMap<SalidaGetDTO, Salidum>().ReverseMap();
            CreateMap<SalidaPutDTO, Salidum>().ReverseMap();
            CreateMap<SalidaInsertDTO, Salidum>().ReverseMap();


            // Mapeos de Ajuste
            CreateMap<AjusteGetDTO, Models.Ajuste>().ReverseMap();
            CreateMap<AjustePutDTO, Models.Ajuste>().ReverseMap();
            CreateMap<AjusteInsertDTO, Models.Ajuste>().ReverseMap();


            // Mapeos de Inventario
            CreateMap<InventarioGetDTO, Models.Inventario>().ReverseMap();
            CreateMap<InventarioPutDTO, Models.Inventario>().ReverseMap();
            CreateMap<InventarioInsertDTO, Models.Inventario>().ReverseMap();

        }
    }

}
