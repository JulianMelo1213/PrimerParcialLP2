using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using PrimerParcialLP2.Models;

public static class MockDbContextHelper
{
    public static Mock<GestionInventariosContext> GetMockContext(List<Ajuste> ajustes)
    {
        var data = ajustes.AsQueryable();

        var mockSet = new Mock<DbSet<Ajuste>>();
        mockSet.As<IQueryable<Ajuste>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Ajuste>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Ajuste>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Ajuste>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return data.SingleOrDefault(d => d.AjusteId == id);
        });

        var mockContext = new Mock<GestionInventariosContext>();
        mockContext.Setup(c => c.Ajustes).Returns(mockSet.Object);
        mockContext.Setup(m => m.Set<Ajuste>()).Returns(mockSet.Object);
        mockContext.Setup(m => m.Entry(It.IsAny<Ajuste>())).Callback((Ajuste a) =>
        {
            mockContext.Object.Entry(a).State = EntityState.Modified;
        });

        return mockContext;
    }

    public static Mock<GestionInventariosContext> GetMockContext(List<Almacen> almacens)
    {
        var data = almacens.AsQueryable();

        var mockSet = new Mock<DbSet<Almacen>>();
        mockSet.As<IQueryable<Almacen>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Almacen>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Almacen>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Almacen>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return data.SingleOrDefault(d => d.AlmacenId == id);
        });

        var mockContext = new Mock<GestionInventariosContext>();
        mockContext.Setup(c => c.Almacen).Returns(mockSet.Object);
        mockContext.Setup(m => m.Set<Almacen>()).Returns(mockSet.Object);
        mockContext.Setup(m => m.Entry(It.IsAny<Almacen>())).Callback((Almacen a) =>
        {
            mockContext.Object.Entry(a).State = EntityState.Modified;
        });

        return mockContext;
    }

    public static Mock<GestionInventariosContext> GetMockContext(List<Categorium> categorias)
    {
        var data = categorias.AsQueryable();

        var mockSet = new Mock<DbSet<Categorium>>();
        mockSet.As<IQueryable<Categorium>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Categorium>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Categorium>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Categorium>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return data.SingleOrDefault(d => d.CategoriaId == id);
        });

        var mockContext = new Mock<GestionInventariosContext>();
        mockContext.Setup(c => c.Categoria).Returns(mockSet.Object);
        mockContext.Setup(m => m.Set<Categorium>()).Returns(mockSet.Object);
        mockContext.Setup(m => m.Entry(It.IsAny<Categorium>())).Callback((Categorium a) =>
        {
            mockContext.Object.Entry(a).State = EntityState.Modified;
        });

        return mockContext;
    }

    public static Mock<GestionInventariosContext> GetMockContext(List<Entradum> entradas)
    {
        var data = entradas.AsQueryable();

        var mockSet = new Mock<DbSet<Entradum>>();
        mockSet.As<IQueryable<Entradum>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Entradum>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Entradum>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Entradum>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return data.SingleOrDefault(d => d.EntradaId == id);
        });

        var mockContext = new Mock<GestionInventariosContext>();
        mockContext.Setup(c => c.Entrada).Returns(mockSet.Object);
        mockContext.Setup(m => m.Set<Entradum>()).Returns(mockSet.Object);
        mockContext.Setup(m => m.Entry(It.IsAny<Entradum>())).Callback((Entradum e) =>
        {
            mockContext.Object.Entry(e).State = EntityState.Modified;
        });

        return mockContext;
    }

    public static Mock<GestionInventariosContext> GetMockContext(List<Inventario> inventarios)
    {
        var data = inventarios.AsQueryable();

        var mockSet = new Mock<DbSet<Inventario>>();
        mockSet.As<IQueryable<Inventario>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Inventario>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Inventario>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Inventario>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return data.SingleOrDefault(d => d.InventarioId == id);
        });

        var mockContext = new Mock<GestionInventariosContext>();
        mockContext.Setup(c => c.Inventarios).Returns(mockSet.Object);
        mockContext.Setup(m => m.Set<Inventario>()).Returns(mockSet.Object);
        mockContext.Setup(m => m.Entry(It.IsAny<Inventario>())).Callback((Inventario e) =>
        {
            mockContext.Object.Entry(e).State = EntityState.Modified;
        });

        return mockContext;
    }
    public static Mock<GestionInventariosContext> GetMockContext(List<Producto> productos)
    {
        var data = productos.AsQueryable();

        var mockSet = new Mock<DbSet<Producto>>();
        mockSet.As<IQueryable<Producto>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Producto>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Producto>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Producto>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return data.SingleOrDefault(d => d.ProductoId == id);
        });

        var mockContext = new Mock<GestionInventariosContext>();
        mockContext.Setup(c => c.Productos).Returns(mockSet.Object);
        mockContext.Setup(m => m.Set<Producto>()).Returns(mockSet.Object);
        mockContext.Setup(m => m.Entry(It.IsAny<Producto>())).Callback((Producto p) =>
        {
            mockContext.Object.Entry(p).State = EntityState.Modified;
        });

        return mockContext;
    }

    public static Mock<GestionInventariosContext> GetMockContext(List<Proveedor> proveedores)
    {
        var data = proveedores.AsQueryable();

        var mockSet = new Mock<DbSet<Proveedor>>();
        mockSet.As<IQueryable<Proveedor>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Proveedor>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Proveedor>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Proveedor>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return data.SingleOrDefault(d => d.ProveedorId == id);
        });

        var mockContext = new Mock<GestionInventariosContext>();
        mockContext.Setup(c => c.Proveedors).Returns(mockSet.Object);
        mockContext.Setup(m => m.Set<Proveedor>()).Returns(mockSet.Object);
        mockContext.Setup(m => m.Entry(It.IsAny<Proveedor>())).Callback((Proveedor p) =>
        {
            mockContext.Object.Entry(p).State = EntityState.Modified;
        });

        return mockContext;
    }

    public static Mock<GestionInventariosContext> GetMockContext(List<Salidum> salidas)
    {
        var data = salidas.AsQueryable();

        var mockSet = new Mock<DbSet<Salidum>>();
        mockSet.As<IQueryable<Salidum>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Salidum>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Salidum>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Salidum>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return data.SingleOrDefault(d => d.SalidaId == id);
        });

        var mockContext = new Mock<GestionInventariosContext>();
        mockContext.Setup(c => c.Salida).Returns(mockSet.Object);
        mockContext.Setup(m => m.Set<Salidum>()).Returns(mockSet.Object);
        mockContext.Setup(m => m.Entry(It.IsAny<Salidum>())).Callback((Salidum s) =>
        {
            mockContext.Object.Entry(s).State = EntityState.Modified;
        });

        return mockContext;
    }
}
