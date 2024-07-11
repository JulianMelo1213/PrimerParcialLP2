using GestionInventario.Client2;
using GestionInventario.Client2.Services;
using GestionInventario.Client2.Services.Ajuste;
using GestionInventario.Client2.Services.Almacen;
using GestionInventario.Client2.Services.Categoria;
using GestionInventario.Client2.Services.Entrada;
using GestionInventario.Client2.Services.Inventario;
using GestionInventario.Client2.Services.Producto;
using GestionInventario.Client2.Services.Proveedor;
using GestionInventario.Client2.Services.Salida;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7166") });
builder.Services.AddScoped<IAjusteServicio, AjusteService>();
builder.Services.AddScoped<IAlmacenService, AlmacenService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IEntradaService, EntradaService>();
builder.Services.AddScoped<IInventarioService, InventarioService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<ISalidaService, SalidaService>();






await builder.Build().RunAsync();
