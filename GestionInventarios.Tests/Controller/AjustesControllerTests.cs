using AutoMapper;
using Moq;
using PrimerParcialLP2.Controllers;
using PrimerParcialLP2.DTO.Ajuste;
using PrimerParcialLP2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PrimerParcialLP2.DTO;

public class AjustesControllerTests
{
    private readonly IMapper _mapper;
    private readonly AjustesController _controller;
    private readonly Mock<GestionInventariosContext> _mockContext;

    public AjustesControllerTests()
    {
        // Configurar AutoMapper
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = new Mapper(configuration);

        // Crear datos de ejemplo
        var ajustes = new List<Ajuste>
        {
            new Ajuste { AjusteId = 1, ProductoId = 1, AlmacenId = 1, Cantidad = 10, Fecha = DateTime.Now, Tipo = "Incremento" }
        };

        // Obtener el contexto mock
        _mockContext = MockDbContextHelper.GetMockContext(ajustes);

        // Crear instancia del controlador
        _controller = new AjustesController(_mockContext.Object, _mapper);
    }

    [Fact]
    public async Task GetAjuste_ReturnsOkResult_WithAjusteGetDTO()
    {
        var ajusteId = 1;
        var ajuste = new Ajuste { AjusteId = ajusteId, ProductoId = 1, AlmacenId = 1, Cantidad = 10, Fecha = DateTime.Now, Tipo = "Incremento" };

        _mockContext.Setup(ctx => ctx.Ajustes.FindAsync(ajusteId)).ReturnsAsync(ajuste);

        var result = await _controller.GetAjuste(ajusteId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<AjusteGetDTO>(okResult.Value);
        Assert.Equal(ajusteId, returnValue.AjusteId);
    }

    [Fact]
    public async Task PutAjuste_ReturnsNoContentResult_WhenAjusteExists()
    {
        var ajusteId = 1;
        var ajustePutDTO = new AjustePutDTO { AjusteId = ajusteId, ProductoId = 1, AlmacenId = 1, Cantidad = 10, Fecha = DateTime.Now, Tipo = "Decremento" };
        var ajuste = new Ajuste { AjusteId = ajusteId, ProductoId = 1, AlmacenId = 1, Cantidad = 10, Fecha = DateTime.Now, Tipo = "Incremento" };

        _mockContext.Setup(ctx => ctx.Ajustes.FindAsync(ajusteId)).ReturnsAsync(ajuste);
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PutAjuste(ajusteId, ajustePutDTO);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostAjuste_ReturnsOkObjectResult()
    {
        var ajusteInsertDTO = new AjusteInsertDTO { ProductoId = 1, AlmacenId = 1, Cantidad = 10, Fecha = DateTime.Now, Tipo = "Incremento" };
        var ajuste = _mapper.Map<Ajuste>(ajusteInsertDTO);

        _mockContext.Setup(ctx => ctx.Ajustes.Add(It.IsAny<Ajuste>()));
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PostAjuste(ajusteInsertDTO);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);
        Assert.Equal(ajuste.AjusteId, returnValue);
    }
}
