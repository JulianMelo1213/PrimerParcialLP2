using Microsoft.AspNetCore.Mvc;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Producto;
using PrimerParcialLP2.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

public class ProductosControllerTests : IDisposable
{
    private readonly ProductosController _controller;
    private readonly DbTestFixture<GestionInventariosContext> _fixture;

    public ProductosControllerTests()
    {
        _fixture = new DbTestFixture<GestionInventariosContext>();
        _controller = new ProductosController(_fixture.Context, _fixture.Mapper);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public void Setup()
    {
        var productos = new List<Producto>
        {
            new Producto { ProductoId = 1, Nombre = "Producto A", Descripcion = "Descripcion A", Precio = 10.0M },
            new Producto { ProductoId = 2, Nombre = "Producto B", Descripcion = "Descripcion B", Precio = 20.0M }
        };
        _fixture.Context.Productos.AddRange(productos);
        _fixture.Context.SaveChanges();
    }

    [Fact]
    public async Task GetProducto_ReturnsOkResult_WithProductoGetDTO()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetProducto(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ProductoGetDTO>(okResult.Value);
        Assert.Equal(1, returnValue.ProductoId);
        Assert.Equal("Producto A", returnValue.Nombre);
        Assert.Equal("Descripcion A", returnValue.Descripcion);
        Assert.Equal(10.0M, returnValue.Precio);
    }

    [Fact]
    public async Task GetProductos_ReturnsOkResult()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetProductos();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<ProductoGetDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task PostProducto_ReturnsOkObjectResult()
    {
        // Arrange
        var productoInsertDTO = new ProductoInsertDTO { Nombre = "Producto C", Descripcion = "Descripcion C", Precio = 30.0M };

        // Act
        var result = await _controller.PostProducto(productoInsertDTO);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);

        // Verify that the ID is valid
        Assert.True(returnValue > 0);

        // Verify that the entity was added to the database
        var addedEntry = await _fixture.Context.Productos.FindAsync(returnValue);
        Assert.NotNull(addedEntry);
        Assert.Equal(productoInsertDTO.Nombre, addedEntry.Nombre);
        Assert.Equal(productoInsertDTO.Descripcion, addedEntry.Descripcion);
        Assert.Equal(productoInsertDTO.Precio, addedEntry.Precio);
    }

    [Fact]
    public async Task PutProducto_ReturnsNoContentResult_WhenProductoExists()
    {
        // Arrange
        Setup();
        var productoPutDTO = new ProductoPutDTO { ProductoId = 1, Nombre = "Producto B", Descripcion = "Descripcion B", Precio = 20.0M };

        // Act
        var result = await _controller.PutProducto(1, productoPutDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteProducto_ReturnsNoContent_WithValidId()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.DeleteProducto(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
