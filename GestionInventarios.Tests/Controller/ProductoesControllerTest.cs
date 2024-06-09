using AutoMapper;
using Moq;
using PrimerParcialLP2.Controllers;
using PrimerParcialLP2.DTO.Producto;
using PrimerParcialLP2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PrimerParcialLP2.DTO;

public class ProductoesControllerTests
{
    private readonly IMapper _mapper;
    private readonly ProductoesController _controller;
    private readonly Mock<GestionInventariosContext> _mockContext;

    public ProductoesControllerTests()
    {
        // Configurar AutoMapper
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = new Mapper(configuration);

        // Crear datos de ejemplo
        var productos = new List<Producto>
        {
            new Producto { ProductoId = 1, Nombre = "Producto A", Descripcion = "Descripcion A", Precio = 10.0M }
        };

        // Obtener el contexto mock
        _mockContext = MockDbContextHelper.GetMockContext(productos);

        // Crear instancia del controlador
        _controller = new ProductoesController(_mockContext.Object, _mapper);
    }


    [Fact]
    public async Task GetProducto_ReturnsOkResult_WithProductoGetDTO()
    {
        var productoId = 1;
        var producto = new Producto { ProductoId = productoId, Nombre = "Producto A", Descripcion = "Descripcion A", Precio = 10.0M };

        _mockContext.Setup(ctx => ctx.Productos.FindAsync(productoId)).ReturnsAsync(producto);

        var result = await _controller.GetProducto(productoId);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ProductoGetDTO>(okResult.Value);
        Assert.Equal(productoId, returnValue.ProductoId);
    }

    [Fact]
    public async Task PutProducto_ReturnsNoContentResult_WhenProductoExists()
    {
        var productoId = 1;
        var productoPutDTO = new ProductoPutDTO { ProductoId = productoId, Nombre = "Producto B", Descripcion = "Descripcion B", Precio = 20.0M };
        var producto = new Producto { ProductoId = productoId, Nombre = "Producto A", Descripcion = "Descripcion A", Precio = 10.0M };

        _mockContext.Setup(ctx => ctx.Productos.FindAsync(productoId)).ReturnsAsync(producto);
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PutProducto(productoId, productoPutDTO);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PostProducto_ReturnsOkObjectResult()
    {
        var productoInsertDTO = new ProductoInsertDTO { Nombre = "Producto C", Descripcion = "Descripcion C", Precio = 30.0M };
        var producto = _mapper.Map<Producto>(productoInsertDTO);

        _mockContext.Setup(ctx => ctx.Productos.Add(It.IsAny<Producto>()));
        _mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);

        var result = await _controller.PostProducto(productoInsertDTO);

        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<int>(okObjectResult.Value);
        Assert.Equal(producto.ProductoId, returnValue);
    }
}
