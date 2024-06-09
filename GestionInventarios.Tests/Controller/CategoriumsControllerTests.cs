using AutoMapper;
using Moq;
using PrimerParcialLP2.Controllers;
using PrimerParcialLP2.DTO.Categoria;
using PrimerParcialLP2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PrimerParcialLP2.DTO;

public class CategoriumsControllerTests
{
    private readonly IMapper _mapper;
    private readonly CategoriumsController _controller;
    private readonly Mock<GestionInventariosContext> _mockContext;

    public CategoriumsControllerTests()
    {
        // Configurar AutoMapper
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = new Mapper(configuration);

        // Crear datos de ejemplo
        var categorias = new List<Categorium>
        {
            new Categorium { CategoriaId = 1, Nombre = "Categoría A" }
        };

        // Obtener el contexto mock
        _mockContext = MockDbContextHelper.GetMockContext(categorias);

        // Crear instancia del controlador
        _controller = new CategoriumsController(_mockContext.Object, _mapper);
    }

    [Fact]
    public async Task GetCategorium_ReturnsOkResult_WithCategoriaGetDTO()
    {
        var categoriaId = 1;
        var categoria = new Categorium { CategoriaId = categoriaId, Nombre = "Categoría A" };

        _mockContext.Setup(ctx => ctx.Categoria.FindAsync(categoriaId)).ReturnsAsync(categoria);

        var result = await _controller.GetCategorium(categoriaId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<CategoriaGetDTO>(okResult.Value);
        Assert.Equal(categoriaId, returnValue.CategoriaId);
    }

    [Fact]
    public async Task PutCategorium_ReturnsNoContentResult_WhenCategoriaExists()
    {
        var categoriaId = 1;
        var categoriaPutDTO = new CategoriaPutDTO { CategoriaId = categoriaId, Nombre = "Categoría B" };
        var categoria = new Categorium { CategoriaId = categoriaId, Nombre = "Categoría A" };

        _mockContext.Setup(ctx => ctx.Categoria.FindAsync(categoriaId)).ReturnsAsync(categoria);
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PutCategorium(categoriaId, categoriaPutDTO);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostCategorium_ReturnsOkObjectResult()
    {
        var categoriaInsertDTO = new CategoriaInsertDTO { Nombre = "Categoría C" };
        var categoria = _mapper.Map<Categorium>(categoriaInsertDTO);

        _mockContext.Setup(ctx => ctx.Categoria.Add(It.IsAny<Categorium>()));
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PostCategorium(categoriaInsertDTO);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);
        Assert.Equal(categoria.CategoriaId, returnValue);
    }
}
