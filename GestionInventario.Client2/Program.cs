using GestionInventario.Client2;
using GestionInventario.Client2.Services;
using GestionInventario.Client2.Services.Ajuste;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7166") });
builder.Services.AddScoped<IAjusteServicio, AjusteService>();



await builder.Build().RunAsync();
