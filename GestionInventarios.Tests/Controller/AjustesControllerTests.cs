using Microsoft.AspNetCore.Mvc;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Ajuste;
using PrimerParcialLP2.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

public class AjustesControllerTests : IDisposable
{
    private readonly AjustesController _controller;
    private readonly DbTestFixture<GestionInventariosContext> _fixture;

    public AjustesControllerTests()
    {
        _fixture = new DbTestFixture<GestionInventariosContext>();
        _controller = new AjustesController(_fixture.Context, _fixture.Mapper);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public void Setup()
    {
        var ajustes = new List<Ajuste>
        {
            new Ajuste
            {
                AjusteId = 1,
                ProductoId = 1,
                Producto = new Producto { ProductoId = 1, Nombre = "Producto A" },
                AlmacenId = 1,
                Almacen = new Almacen { AlmacenId = 1, Nombre = "Almacen A" },
                Cantidad = 10,
                Fecha = DateTime.Now,
                Tipo = "Incremento"
            },
            new Ajuste
            {
                AjusteId = 2,
                ProductoId = 2,
                Producto = new Producto { ProductoId = 2, Nombre = "Producto B" },
                AlmacenId = 2,
                Almacen = new Almacen { AlmacenId = 2, Nombre = "Almacen B" },
                Cantidad = 20,
                Fecha = DateTime.Now,
                Tipo = "Decremento"
            }
        };
        _fixture.Context.Ajustes.AddRange(ajustes);
        _fixture.Context.SaveChanges();
    }

    [Fact]
    public async Task GetAjuste_ReturnsOkResult_WithAjusteGetDTO()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetAjuste(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<AjusteGetDTO>(okResult.Value);
        Assert.Equal(1, returnValue.AjusteId);
    }

    [Fact]
    public async Task GetAjustes_ReturnsOkResult()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetAjustes();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<AjusteGetDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task PostAjuste_ReturnsOkObjectResult()
    {
        // Arrange
        var ajusteInsertDTO = new AjusteInsertDTO
        {
            ProductoId = 1,
            AlmacenId = 1,
            Cantidad = 10,
            Fecha = DateTime.Now,
            Tipo = "Incremento"
        };

        // Act
        var result = await _controller.PostAjuste(ajusteInsertDTO);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<int>(okResult.Value);
    }

    [Fact]
    public async Task PutAjuste_ReturnsNoContentResult_WhenAjusteExists()
    {
        // Arrange
        Setup();
        var ajustePutDTO = new AjustePutDTO
        {
            AjusteId = 1,
            ProductoId = 1,
            AlmacenId = 1,
            Cantidad = 10,
            Fecha = DateTime.Now,
            Tipo = "Decremento"
        };

        // Act
        var result = await _controller.PutAjuste(1, ajustePutDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteAjuste_ReturnsNoContent_WithValidId()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.DeleteAjuste(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
