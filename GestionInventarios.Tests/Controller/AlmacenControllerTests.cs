using AutoMapper;
using Moq;
using PrimerParcialLP2.Controllers;
using PrimerParcialLP2.DTO.Almacen;
using PrimerParcialLP2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PrimerParcialLP2.DTO;

public class AlmacensControllerTests
{
    private readonly IMapper _mapper;
    private readonly AlmacensController _controller;
    private readonly Mock<GestionInventariosContext> _mockContext;

    public AlmacensControllerTests()
    {
        // Configurar AutoMapper
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = new Mapper(configuration);

        // Crear datos de ejemplo
        var almacens = new List<Almacen>
        {
            new Almacen { AlmacenId = 1, Nombre = "Almacen A", Ubicacion = "Direccion A" }
        };

        // Obtener el contexto mock
        _mockContext = MockDbContextHelper.GetMockContext(almacens);

        // Crear instancia del controlador
        _controller = new AlmacensController(_mockContext.Object, _mapper);
    }

    [Fact]
    public async Task GetAlmacen_ReturnsOkResult_WithAlmacenGetDTO()
    {
        var almacenId = 1;
        var almacen = new Almacen { AlmacenId = almacenId, Nombre = "Almacen A", Ubicacion = "Direccion A" };

        _mockContext.Setup(ctx => ctx.Almacen.FindAsync(almacenId)).ReturnsAsync(almacen);

        var result = await _controller.GetAlmacen(almacenId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<AlmacenGetDTO>(okResult.Value);
        Assert.Equal(almacenId, returnValue.AlmacenId);
    }

    [Fact]
    public async Task PutAlmacen_ReturnsNoContentResult_WhenAlmacenExists()
    {
        var almacenId = 1;
        var almacenPutDTO = new AlmacenPutDTO { AlmacenId = almacenId, Nombre = "Almacen B", Ubicacion = "Direccion B" };
        var almacen = new Almacen { AlmacenId = almacenId, Nombre = "Almacen A", Ubicacion = "Direccion A" };

        _mockContext.Setup(ctx => ctx.Almacen.FindAsync(almacenId)).ReturnsAsync(almacen);
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PutAlmacen(almacenId, almacenPutDTO);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostAlmacen_ReturnsOkObjectResult()
    {
        var almacenInsertDTO = new AlmacenInsertDTO { Nombre = "Almacen C", Ubicacion = "Direccion C" };
        var almacen = _mapper.Map<Almacen>(almacenInsertDTO);

        _mockContext.Setup(ctx => ctx.Almacen.Add(It.IsAny<Almacen>()));
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PostAlmacen(almacenInsertDTO);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);
        Assert.Equal(almacen.AlmacenId, returnValue);
    }
}
