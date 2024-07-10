using AutoMapper;
using Moq;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Entrada;
using PrimerParcialLP2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PrimerParcialLP2;


public class EntradumsControllerTests
{
    private readonly IMapper _mapper;
    private readonly EntradumsController _controller;
    private readonly Mock<GestionInventariosContext> _mockContext;

    public EntradumsControllerTests()
    {
        // Configurar AutoMapper
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = new Mapper(configuration);

        // Crear datos de ejemplo
        var entradas = new List<Entradum>
        {
            new Entradum { EntradaId = 1, ProductoId = 1, Cantidad = 10, Fecha = DateTime.Now }
        };

        // Obtener el contexto mock
        _mockContext = MockDbContextHelper.GetMockContext(entradas);

        // Crear instancia del controlador
        _controller = new EntradumsController(_mockContext.Object, _mapper);
    }


    [Fact]
    public async Task GetEntradum_ReturnsOkResult_WithEntradaGetDTO()
    {
        var entradaId = 1;
        var entrada = new Entradum { EntradaId = entradaId, ProductoId = 1, Cantidad = 10, Fecha = DateTime.Now };

        _mockContext.Setup(ctx => ctx.Entrada.FindAsync(entradaId)).ReturnsAsync(entrada);

        var result = await _controller.GetEntradum(entradaId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<EntradaGetDTO>(okResult.Value);
        Assert.Equal(entradaId, returnValue.EntradaId);
    }

    [Fact]
    public async Task PutEntradum_ReturnsNoContentResult_WhenEntradaExists()
    {
        var entradaId = 1;
        var entradaPutDTO = new EntradaPutDTO { EntradaId = entradaId, ProductoId = 1, Cantidad = 20, Fecha = DateTime.Now };
        var entrada = new Entradum { EntradaId = entradaId, ProductoId = 1, Cantidad = 10, Fecha = DateTime.Now };

        _mockContext.Setup(ctx => ctx.Entrada.FindAsync(entradaId)).ReturnsAsync(entrada);
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PutEntradum(entradaId, entradaPutDTO);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostEntradum_ReturnsOkObjectResult()
    {
        var entradaInsertDTO = new EntradaInsertDTO { ProductoId = 2, Cantidad = 15, Fecha = DateTime.Now };
        var entrada = _mapper.Map<Entradum>(entradaInsertDTO);

        _mockContext.Setup(ctx => ctx.Entrada.Add(It.IsAny<Entradum>()));
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PostEntradum(entradaInsertDTO);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);
        Assert.Equal(entrada.EntradaId, returnValue);
    }

}
