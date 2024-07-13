using Microsoft.AspNetCore.Mvc;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Proveedor;
using PrimerParcialLP2.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

public class ProveedorsControllerTests : IDisposable
{
    private readonly ProveedorsController _controller;
    private readonly DbTestFixture<GestionInventariosContext> _fixture;

    public ProveedorsControllerTests()
    {
        _fixture = new DbTestFixture<GestionInventariosContext>();
        _controller = new ProveedorsController(_fixture.Context, _fixture.Mapper);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public void Setup()
    {
        var proveedores = new List<Proveedor>
        {
            new Proveedor { ProveedorId = 1, Nombre = "Proveedor A", Direccion = "Direccion A", Telefono = "12222222" },
            new Proveedor { ProveedorId = 2, Nombre = "Proveedor B", Direccion = "Direccion B", Telefono = "13333333" }
        };
        _fixture.Context.Proveedors.AddRange(proveedores);
        _fixture.Context.SaveChanges();
    }

    [Fact]
    public async Task GetProveedor_ReturnsOkResult_WithProveedorGetDTO()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetProveedor(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ProveedorGetDTO>(okResult.Value);
        Assert.Equal(1, returnValue.ProveedorId);
        Assert.Equal("Proveedor A", returnValue.Nombre);
        Assert.Equal("Direccion A", returnValue.Direccion);
        Assert.Equal("12222222", returnValue.Telefono);
    }

    [Fact]
    public async Task GetProveedors_ReturnsOkResult()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetProveedors();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<ProveedorGetDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task PostProveedor_ReturnsOkObjectResult()
    {
        // Arrange
        var proveedorInsertDTO = new ProveedorInsertDTO { Nombre = "Proveedor C", Direccion = "Direccion C", Telefono = "14444444" };

        // Act
        var result = await _controller.PostProveedor(proveedorInsertDTO);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);

        // Verify that the ID is valid
        Assert.True(returnValue > 0);

        // Verify that the entity was added to the database
        var addedEntry = await _fixture.Context.Proveedors.FindAsync(returnValue);
        Assert.NotNull(addedEntry);
        Assert.Equal(proveedorInsertDTO.Nombre, addedEntry.Nombre);
        Assert.Equal(proveedorInsertDTO.Direccion, addedEntry.Direccion);
        Assert.Equal(proveedorInsertDTO.Telefono, addedEntry.Telefono);
    }

    [Fact]
    public async Task PutProveedor_ReturnsNoContentResult_WhenProveedorExists()
    {
        // Arrange
        Setup();
        var proveedorPutDTO = new ProveedorPutDTO { ProveedorId = 1, Nombre = "Proveedor B", Direccion = "Direccion B", Telefono = "15555555" };

        // Act
        var result = await _controller.PutProveedor(1, proveedorPutDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteProveedor_ReturnsNoContent_WithValidId()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.DeleteProveedor(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
