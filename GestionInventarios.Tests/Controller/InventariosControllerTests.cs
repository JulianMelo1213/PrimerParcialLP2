using Microsoft.AspNetCore.Mvc;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Inventario;
using PrimerParcialLP2.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

public class InventariosControllerTests : IDisposable
{
    private readonly InventariosController _controller;
    private readonly DbTestFixture<GestionInventariosContext> _fixture;

    public InventariosControllerTests()
    {
        _fixture = new DbTestFixture<GestionInventariosContext>();
        _controller = new InventariosController(_fixture.Context, _fixture.Mapper);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public void Setup()
    {
        var inventarios = new List<Inventario>
        {
            new Inventario
            {
                InventarioId = 1,
                ProductoId = 1,
                Producto = new Producto { ProductoId = 1, Nombre = "Producto A" },
                AlmacenId = 1,
                Almacen = new Almacen { AlmacenId = 1, Nombre = "Almacen A" },
                Cantidad = 10,
                Fecha = DateTime.Now
            },
            new Inventario
            {
                InventarioId = 2,
                ProductoId = 2,
                Producto = new Producto { ProductoId = 2, Nombre = "Producto B" },
                AlmacenId = 2,
                Almacen = new Almacen { AlmacenId = 2, Nombre = "Almacen B" },
                Cantidad = 20,
                Fecha = DateTime.Now
            }
        };
        _fixture.Context.Inventarios.AddRange(inventarios);
        _fixture.Context.SaveChanges();
    }

    [Fact]
    public async Task GetInventario_ReturnsOkResult_WithInventarioGetDTO()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetInventario(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<InventarioGetDTO>(okResult.Value);
        Assert.Equal(1, returnValue.InventarioId);
        Assert.Equal("Producto A", returnValue.ProductoNombre);
        Assert.Equal("Almacen A", returnValue.AlmacenNombre);
    }

    [Fact]
    public async Task GetInventarios_ReturnsOkResult()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetInventarios();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<InventarioGetDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task PostInventario_ReturnsOkObjectResult()
    {
        // Arrange
        var inventarioInsertDTO = new InventarioInsertDTO { ProductoId = 2, AlmacenId = 2, Cantidad = 15, Fecha = DateTime.Now };

        // Act
        var result = await _controller.PostInventario(inventarioInsertDTO);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);

        // Verify that the ID is valid
        Assert.True(returnValue > 0);

        // Verify that the entity was added to the database
        var addedEntry = await _fixture.Context.Inventarios.FindAsync(returnValue);
        Assert.NotNull(addedEntry);
        Assert.Equal(inventarioInsertDTO.ProductoId, addedEntry.ProductoId);
        Assert.Equal(inventarioInsertDTO.AlmacenId, addedEntry.AlmacenId);
        Assert.Equal(inventarioInsertDTO.Cantidad, addedEntry.Cantidad);
        Assert.Equal(inventarioInsertDTO.Fecha, addedEntry.Fecha);
    }

    [Fact]
    public async Task PutInventario_ReturnsNoContentResult_WhenInventarioExists()
    {
        // Arrange
        Setup();
        var inventarioPutDTO = new InventarioPutDTO { InventarioId = 1, ProductoId = 1, AlmacenId = 1, Cantidad = 20, Fecha = DateTime.Now };

        // Act
        var result = await _controller.PutInventario(1, inventarioPutDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteInventario_ReturnsNoContent_WithValidId()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.DeleteInventario(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
