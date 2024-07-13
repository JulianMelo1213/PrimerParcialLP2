using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrimerParcialLP2;
using PrimerParcialLP2.Models;
using System;

public class DbTestFixture<TContext> : IDisposable where TContext : DbContext
{
    public TContext Context { get; private set; }
    public IMapper Mapper { get; private set; }

    public DbTestFixture()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Configurar opciones para la base de datos en memoria
        var options = new DbContextOptionsBuilder<TContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        // Configurar el contexto de base de datos
        Context = (TContext)Activator.CreateInstance(typeof(TContext), options);
        Context.Database.EnsureCreated();

        // Configurar AutoMapper
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile()); 
        });
        Mapper = mappingConfig.CreateMapper();
    }

    public void Dispose()
    {
        // Liberar recursos al finalizar las pruebas
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
