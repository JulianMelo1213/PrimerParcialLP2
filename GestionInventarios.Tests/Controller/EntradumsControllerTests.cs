using Microsoft.AspNetCore.Mvc;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Entrada;
using PrimerParcialLP2.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

public class EntradumsControllerTests : IDisposable
{
    private readonly EntradumsController _controller;
    private readonly DbTestFixture<GestionInventariosContext> _fixture;

    public EntradumsControllerTests()
    {
        _fixture = new DbTestFixture<GestionInventariosContext>();
        _controller = new EntradumsController(_fixture.Context, _fixture.Mapper);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public void Setup()
    {
        var entradas = new List<Entradum>
        {
            new Entradum
            {
                EntradaId = 1,
                ProductoId = 1,
                Producto = new Producto { ProductoId = 1, Nombre = "Producto A" },
                Cantidad = 10,
                Fecha = DateTime.Now
            },
            new Entradum
            {
                EntradaId = 2,
                ProductoId = 2,
                Producto = new Producto { ProductoId = 2, Nombre = "Producto B" },
                Cantidad = 20,
                Fecha = DateTime.Now
            }
        };
        _fixture.Context.Entrada.AddRange(entradas);
        _fixture.Context.SaveChanges();
    }

    [Fact]
    public async Task GetEntradum_ReturnsOkResult_WithEntradaGetDTO()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetEntradum(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<EntradaGetDTO>(okResult.Value);
        Assert.Equal(1, returnValue.EntradaId);
        Assert.Equal("Producto A", returnValue.ProductoNombre);
    }

    [Fact]
    public async Task GetEntrada_ReturnsOkResult()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetEntrada();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<EntradaGetDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task PostEntradum_ReturnsOkObjectResult()
    {
        // Arrange
        var entradaInsertDTO = new EntradaInsertDTO { ProductoId = 2, Cantidad = 15, Fecha = DateTime.Now };

        // Act
        var result = await _controller.PostEntradum(entradaInsertDTO);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);

        // Verify that the ID is valid
        Assert.True(returnValue > 0);

        // Verify that the entity was added to the database
        var addedEntry = await _fixture.Context.Entrada.FindAsync(returnValue);
        Assert.NotNull(addedEntry);
        Assert.Equal(entradaInsertDTO.ProductoId, addedEntry.ProductoId);
        Assert.Equal(entradaInsertDTO.Cantidad, addedEntry.Cantidad);
        Assert.Equal(entradaInsertDTO.Fecha, addedEntry.Fecha);
    }

    [Fact]
    public async Task PutEntradum_ReturnsNoContentResult_WhenEntradaExists()
    {
        // Arrange
        Setup();
        var entradaPutDTO = new EntradaPutDTO { EntradaId = 1, ProductoId = 1, Cantidad = 20, Fecha = DateTime.Now };

        // Act
        var result = await _controller.PutEntradum(1, entradaPutDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteEntradum_ReturnsNoContent_WithValidId()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.DeleteEntradum(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
