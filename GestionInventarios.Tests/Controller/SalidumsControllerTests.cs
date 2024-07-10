using AutoMapper;
using Moq;
using PrimerParcialLP2.Controllers;
using GestionInventarios.Shared.DTOs.Salida;
using PrimerParcialLP2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PrimerParcialLP2;


public class SalidumsControllerTests
{
    private readonly IMapper _mapper;
    private readonly SalidumsController _controller;
    private readonly Mock<GestionInventariosContext> _mockContext;

    public SalidumsControllerTests()
    {
        // Configurar AutoMapper
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = new Mapper(configuration);

        // Crear datos de ejemplo
        var salidas = new List<Salidum>
        {
            new Salidum { SalidaId = 1, ProductoId = 1, Cantidad = 5, Fecha = DateTime.Now }
        };

        // Obtener el contexto mock
        _mockContext = MockDbContextHelper.GetMockContext(salidas);

        // Crear instancia del controlador
        _controller = new SalidumsController(_mockContext.Object, _mapper);
    }

    [Fact]
    public async Task GetSalidum_ReturnsOkResult_WithSalidaGetDTO()
    {
        var salidaId = 1;
        var salida = new Salidum { SalidaId = salidaId, ProductoId = 1, Cantidad = 5, Fecha = DateTime.Now };

        _mockContext.Setup(ctx => ctx.Salida.FindAsync(salidaId)).ReturnsAsync(salida);

        var result = await _controller.GetSalidum(salidaId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<SalidaGetDTO>(okResult.Value);
        Assert.Equal(salidaId, returnValue.SalidaId);
    }

    [Fact]
    public async Task PutSalidum_ReturnsNoContentResult_WhenSalidaExists()
    {
        var salidaId = 1;
        var salidaPutDTO = new SalidaPutDTO { SalidaId = salidaId, ProductoId = 1, Cantidad = 10, Fecha = DateTime.Now };
        var salida = new Salidum { SalidaId = salidaId, ProductoId = 1, Cantidad = 5, Fecha = DateTime.Now };

        _mockContext.Setup(ctx => ctx.Salida.FindAsync(salidaId)).ReturnsAsync(salida);
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PutSalidum(salidaId, salidaPutDTO);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostSalidum_ReturnsOkObjectResult()
    {
        var salidaInsertDTO = new SalidaInsertDTO { ProductoId = 2, Cantidad = 10, Fecha = DateTime.Now };
        var salida = _mapper.Map<Salidum>(salidaInsertDTO);

        _mockContext.Setup(ctx => ctx.Salida.Add(It.IsAny<Salidum>()));
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PostSalidum(salidaInsertDTO);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okResult.Value);
        Assert.Equal(salida.SalidaId, returnValue);
    }
}
