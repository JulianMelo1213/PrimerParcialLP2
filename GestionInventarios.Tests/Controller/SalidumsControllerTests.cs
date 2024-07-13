using Microsoft.AspNetCore.Mvc;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Salida;
using PrimerParcialLP2.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

public class SalidumsControllerTests : IDisposable
{
    private readonly SalidumsController _controller;
    private readonly DbTestFixture<GestionInventariosContext> _fixture;

    public SalidumsControllerTests()
    {
        _fixture = new DbTestFixture<GestionInventariosContext>();
        _controller = new SalidumsController(_fixture.Context, _fixture.Mapper);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public void Setup()
    {
        var salidas = new List<Salidum>
        {
            new Salidum
            {
                SalidaId = 1,
                ProductoId = 1,
                Producto = new Producto { ProductoId = 1, Nombre = "Producto A" },
                Cantidad = 5,
                Fecha = DateTime.Now
            },
            new Salidum
            {
                SalidaId = 2,
                ProductoId = 2,
                Producto = new Producto { ProductoId = 2, Nombre = "Producto B" },
                Cantidad = 10,
                Fecha = DateTime.Now
            }
        };
        _fixture.Context.Salida.AddRange(salidas);
        _fixture.Context.SaveChanges();
    }

    [Fact]
    public async Task GetSalida_ReturnsOkResult_WithSalidaGetDTO()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetSalida(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<SalidaGetDTO>(okResult.Value);
        Assert.Equal(1, returnValue.SalidaId);
        Assert.Equal("Producto A", returnValue.ProductoNombre);
    }

    [Fact]
    public async Task GetSalidas_ReturnsOkResult()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetSalidas();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<SalidaGetDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task PostSalida_ReturnsOkObjectResult()
    {
        // Arrange
        var salidaInsertDTO = new SalidaInsertDTO { ProductoId = 2, Cantidad = 10, Fecha = DateTime.Now };

        // Act
        var result = await _controller.PostSalida(salidaInsertDTO);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);

        // Verify that the ID is valid
        Assert.True(returnValue > 0);

        // Verify that the entity was added to the database
        var addedEntry = await _fixture.Context.Salida.FindAsync(returnValue);
        Assert.NotNull(addedEntry);
        Assert.Equal(salidaInsertDTO.ProductoId, addedEntry.ProductoId);
        Assert.Equal(salidaInsertDTO.Cantidad, addedEntry.Cantidad);
        Assert.Equal(salidaInsertDTO.Fecha, addedEntry.Fecha);
    }

    [Fact]
    public async Task PutSalida_ReturnsNoContentResult_WhenSalidaExists()
    {
        // Arrange
        Setup();
        var salidaPutDTO = new SalidaPutDTO { SalidaId = 1, ProductoId = 1, Cantidad = 10, Fecha = DateTime.Now };

        // Act
        var result = await _controller.PutSalida(1, salidaPutDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteSalida_ReturnsNoContent_WithValidId()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.DeleteSalida(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
