using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Day06_Model_Repository;
using Moq;

namespace Day06_Model_Repository_Test;

public static class MockItemDataSource
{
    public static Mock<IItemDataSource> Create(List<Item> initialItems)
    {
        ArgumentNullException.ThrowIfNull(initialItems);

        var mock = new Mock<IItemDataSource>(MockBehavior.Strict);

        mock.Setup(dataSource => dataSource.LoadAllItemsAsync())
            .ReturnsAsync(initialItems);

        mock.Setup(dataSource =>
                dataSource.SaveAllItemsAsync(It.IsAny<List<Item>>()))
            .Returns(Task.CompletedTask);

        return mock;
    }
}
