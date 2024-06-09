using AutoMapper;
using Moq;
using PrimerParcialLP2.Controllers;
using PrimerParcialLP2.DTO.Inventario;
using PrimerParcialLP2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PrimerParcialLP2.DTO;

public class InventariosControllerTests
{
    private readonly IMapper _mapper;
    private readonly InventariosController _controller;
    private readonly Mock<GestionInventariosContext> _mockContext;

    public InventariosControllerTests()
    {
        // Configurar AutoMapper
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = new Mapper(configuration);

        // Crear datos de ejemplo
        var inventarios = new List<Inventario>
        {
            new Inventario { InventarioId = 1, ProductoId = 1, AlmacenId = 1, Cantidad = 10 }
        };

        // Obtener el contexto mock
        _mockContext = MockDbContextHelper.GetMockContext(inventarios);

        // Crear instancia del controlador
        _controller = new InventariosController(_mockContext.Object, _mapper);
    }

    [Fact]
    public async Task GetInventario_ReturnsOkResult_WithInventarioGetDTO()
    {
        var inventarioId = 1;
        var inventario = new Inventario { InventarioId = inventarioId, ProductoId = 1, AlmacenId = 1, Cantidad = 10 };

        _mockContext.Setup(ctx => ctx.Inventarios.FindAsync(inventarioId)).ReturnsAsync(inventario);

        var result = await _controller.GetInventario(inventarioId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<InventarioGetDTO>(okResult.Value);
        Assert.Equal(inventarioId, returnValue.InventarioId);
    }

    [Fact]
    public async Task PutInventario_ReturnsNoContentResult_WhenInventarioExists()
    {
        var inventarioId = 1;
        var inventarioPutDTO = new InventarioPutDTO { InventarioId = inventarioId, ProductoId = 1, AlmacenId = 1, Cantidad = 20 };
        var inventario = new Inventario { InventarioId = inventarioId, ProductoId = 1, AlmacenId = 1, Cantidad = 10 };

        _mockContext.Setup(ctx => ctx.Inventarios.FindAsync(inventarioId)).ReturnsAsync(inventario);
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PutInventario(inventarioId, inventarioPutDTO);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostInventario_ReturnsOkObjectResult()
    {
        var inventarioInsertDTO = new InventarioInsertDTO { ProductoId = 2, AlmacenId = 2, Cantidad = 15 };
        var inventario = _mapper.Map<Inventario>(inventarioInsertDTO);

        _mockContext.Setup(ctx => ctx.Inventarios.Add(It.IsAny<Inventario>()));
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PostInventario(inventarioInsertDTO);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);
        Assert.Equal(inventario.InventarioId, returnValue);
    }
}
