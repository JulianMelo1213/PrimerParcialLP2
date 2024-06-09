using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

public static class DbSetMock
{
    public static Mock<DbSet<T>> GetMockDbSet<T>(IEnumerable<T> data) where T : class
    {
        var queryable = data.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

        mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) =>
        {
            var id = (int)ids[0];
            return queryable.SingleOrDefault(d => EF.Property<int>(d, "AjusteId") == id);
        });

        mockSet.Setup(d => d.Update(It.IsAny<T>())).Callback<T>((s) => {
            queryable = queryable.Append(s).AsQueryable();
        });

        return mockSet;
    }
}
