using Microsoft.AspNetCore.Mvc;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Categoria;
using PrimerParcialLP2.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

public class CategoriumsControllerTests : IDisposable
{
    private readonly CategoriumsController _controller;
    private readonly DbTestFixture<GestionInventariosContext> _fixture;

    public CategoriumsControllerTests()
    {
        _fixture = new DbTestFixture<GestionInventariosContext>();
        _controller = new CategoriumsController(_fixture.Context, _fixture.Mapper);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }

    [Fact]
    public void Setup()
    {
        var categorias = new List<Categorium>
        {
            new Categorium { CategoriaId = 1, Nombre = "Categoría A" },
            new Categorium { CategoriaId = 2, Nombre = "Categoría B" }
        };
        _fixture.Context.Categoria.AddRange(categorias);
        _fixture.Context.SaveChanges();
    }

    [Fact]
    public async Task GetCategorium_ReturnsOkResult_WithCategoriaGetDTO()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetCategorium(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<CategoriaGetDTO>(okResult.Value);
        Assert.Equal(1, returnValue.CategoriaId);
        Assert.Equal("Categoría A", returnValue.Nombre);
    }

    [Fact]
    public async Task GetCategoria_ReturnsOkResult()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.GetCategoria();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<CategoriaGetDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task PostCategorium_ReturnsOkObjectResult()
    {
        // Arrange
        var categoriaInsertDTO = new CategoriaInsertDTO { Nombre = "Categoría C" };

        // Act
        var result = await _controller.PostCategorium(categoriaInsertDTO);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);

        // Verify that the ID is valid
        Assert.True(returnValue > 0);

        // Verify that the entity was added to the database
        var addedEntry = await _fixture.Context.Categoria.FindAsync(returnValue);
        Assert.NotNull(addedEntry);
        Assert.Equal(categoriaInsertDTO.Nombre, addedEntry.Nombre);
    }

    [Fact]
    public async Task PutCategorium_ReturnsNoContentResult_WhenCategoriaExists()
    {
        // Arrange
        Setup();
        var categoriaPutDTO = new CategoriaPutDTO { CategoriaId = 1, Nombre = "Categoría B" };

        // Act
        var result = await _controller.PutCategorium(1, categoriaPutDTO);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteCategorium_ReturnsNoContent_WithValidId()
    {
        // Arrange
        Setup();

        // Act
        var result = await _controller.DeleteCategorium(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
