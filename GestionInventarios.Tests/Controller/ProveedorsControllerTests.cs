using AutoMapper;
using Moq;
using PrimerParcialLP2.Controllers;
using PrimerParcialLP2.DTO.Proveedor;
using PrimerParcialLP2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PrimerParcialLP2.DTO;

public class ProveedorsControllerTests
{
    private readonly IMapper _mapper;
    private readonly ProveedorsController _controller;
    private readonly Mock<GestionInventariosContext> _mockContext;

    public ProveedorsControllerTests()
    {
        // Configurar AutoMapper
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = new Mapper(configuration);

        // Crear datos de ejemplo
        var proveedores = new List<Proveedor>
        {
            new Proveedor { ProveedorId = 1, Nombre = "Proveedor A", Direccion = "Direccion A" }
        };

        // Obtener el contexto mock
        _mockContext = MockDbContextHelper.GetMockContext(proveedores);

        // Crear instancia del controlador
        _controller = new ProveedorsController(_mockContext.Object, _mapper);
    }


    [Fact]
    public async Task GetProveedor_ReturnsOkResult_WithProveedorGetDTO()
    {
        var proveedorId = 1;
        var proveedor = new Proveedor { ProveedorId = proveedorId, Nombre = "Proveedor A", Direccion = "Direccion A" , Telefono = "12222222" };

        _mockContext.Setup(ctx => ctx.Proveedors.FindAsync(proveedorId)).ReturnsAsync(proveedor);

        var result = await _controller.GetProveedor(proveedorId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ProveedorGetDTO>(okResult.Value);
        Assert.Equal(proveedorId, returnValue.ProveedorId);
    }

    [Fact]
    public async Task PutProveedor_ReturnsNoContentResult_WhenProveedorExists()
    {
        var proveedorId = 1;
        var proveedorPutDTO = new ProveedorPutDTO { ProveedorId = proveedorId, Nombre = "Proveedor B", Direccion = "Direccion B", Telefono = "12222222" };
        var proveedor = new Proveedor { ProveedorId = proveedorId, Nombre = "Proveedor A", Direccion = "Direccion A", Telefono = "12222222" };

        _mockContext.Setup(ctx => ctx.Proveedors.FindAsync(proveedorId)).ReturnsAsync(proveedor);
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PutProveedor(proveedorId, proveedorPutDTO);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostProveedor_ReturnsOkObjectResult()
    {
        var proveedorInsertDTO = new ProveedorInsertDTO { Nombre = "Proveedor C", Direccion = "Direccion C", Telefono = "12222222" };
        var proveedor = _mapper.Map<Proveedor>(proveedorInsertDTO);

        _mockContext.Setup(ctx => ctx.Proveedors.Add(It.IsAny<Proveedor>()));
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PostProveedor(proveedorInsertDTO);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);
        Assert.Equal(proveedor.ProveedorId, returnValue);
    }
}
