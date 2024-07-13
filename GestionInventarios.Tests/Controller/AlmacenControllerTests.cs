using Microsoft.AspNetCore.Mvc;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Almacen;
using PrimerParcialLP2.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

public class AlmacensControllerTests : IDisposable
{
    private readonly AlmacensController _controller;
    private readonly DbTestFixture<GestionInventariosContext> _fixture;

    public AlmacensControllerTests()
    {
        _fixture = new DbTestFixture<GestionInventariosContext>();
        _controller = new AlmacensController(_fixture.Context, _fixture.Mapper);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public void Setup()
    {
        var almacens = new List<Almacen>
        {
            new Almacen { AlmacenId = 1, Nombre = "Almacen A", Ubicacion = "Direccion A" },
            new Almacen { AlmacenId = 2, Nombre = "Almacen B", Ubicacion = "Direccion B" }
        };
        _fixture.Context.Almacen.AddRange(almacens);
        _fixture.Context.SaveChanges();
    }

    [Fact]
    public async Task GetAlmacen_ReturnsOkResult_WithAlmacenGetDTO()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetAlmacen(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<AlmacenGetDTO>(okResult.Value);
        Assert.Equal(1, returnValue.AlmacenId);
        Assert.Equal("Almacen A", returnValue.Nombre);
    }

    [Fact]
    public async Task GetAlmacens_ReturnsOkResult()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetAlmacens();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<AlmacenGetDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task PostAlmacen_ReturnsOkObjectResult()
    {
        // Arrange
        var almacenInsertDTO = new AlmacenInsertDTO { Nombre = "Almacen C", Ubicacion = "Direccion C" };

        // Act
        var result = await _controller.PostAlmacen(almacenInsertDTO);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);

        // Verify that the ID is valid
        Assert.True(returnValue > 0);

        // Verify that the entity was added to the database
        var addedEntry = await _fixture.Context.Almacen.FindAsync(returnValue);
        Assert.NotNull(addedEntry);
        Assert.Equal(almacenInsertDTO.Nombre, addedEntry.Nombre);
        Assert.Equal(almacenInsertDTO.Ubicacion, addedEntry.Ubicacion);
    }

    [Fact]
    public async Task PutAlmacen_ReturnsNoContentResult_WhenAlmacenExists()
    {
        // Arrange
        Setup();
        var almacenPutDTO = new AlmacenPutDTO { AlmacenId = 1, Nombre = "Almacen B", Ubicacion = "Direccion B" };

        // Act
        var result = await _controller.PutAlmacen(1, almacenPutDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteAlmacen_ReturnsNoContent_WithValidId()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.DeleteAlmacen(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
