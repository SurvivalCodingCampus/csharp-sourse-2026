using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day06_Model_Repository;
using Moq;
using NUnit.Framework;

namespace Day06_Model_Repository_Test;

[TestFixture]
[TestOf(typeof(InventoryRepository))]
public class InventoryRepositoryTest
{
    [Test]
    public async Task GetItemsAsync_WhenSwordAndShieldExist_ReturnsBothItems()
    {
        // Given
        var initialItems = new List<Item>
        {
            new(1, "Sword", 1),
            new(2, "Shield", 1)
        };

        var dataSourceMock = MockItemDataSource.Create(initialItems);
        var repository = new InventoryRepository(
            dataSourceMock.Object,
            maxSlot: 10,
            maxStack: 99);

        // When
        var items = await repository.GetItemsAsync();

        // Then
        Assert.Multiple(() =>
        {
            Assert.That(items, Has.Count.EqualTo(2));
            Assert.That(
                items.Select(item => item.Name),
                Is.EquivalentTo(new[] { "Sword", "Shield" }));
        });

        dataSourceMock.Verify(
            dataSource => dataSource.LoadAllItemsAsync(),
            Times.Once);
        dataSourceMock.Verify(
            dataSource => dataSource.SaveAllItemsAsync(It.IsAny<List<Item>>()),
            Times.Never);
    }

    [Test]
    public async Task AddItemAsync_WhenNewItemCanBeAdded_AddsAndSavesItem()
    {
        // Given
        var initialItems = new List<Item>
        {
            new(1, "Sword", 1),
            new(2, "Shield", 1)
        };

        var dataSourceMock = MockItemDataSource.Create(initialItems);
        var repository = new InventoryRepository(
            dataSourceMock.Object,
            maxSlot: 3,
            maxStack: 99);

        var potion = new Item(3, "Potion", 1);

        // When
        var result = await repository.AddItemAsync(potion);

        // Then
        Assert.That(result, Is.True);

        dataSourceMock.Verify(
            dataSource => dataSource.LoadAllItemsAsync(),
            Times.Once);
        dataSourceMock.Verify(
            dataSource => dataSource.SaveAllItemsAsync(
                It.Is<List<Item>>(savedItems =>
                    savedItems.Count == 3 &&
                    savedItems.Any(item =>
                        item.Id == 3 &&
                        item.Name == "Potion" &&
                        item.Count == 1))),
            Times.Once);
    }

    [Test]
    public async Task AddItemAsync_WhenItemAlreadyExists_IncreasesAndSavesCount()
    {
        // Given
        var initialItems = new List<Item>
        {
            new(1, "Sword", 1)
        };

        var dataSourceMock = MockItemDataSource.Create(initialItems);
        var repository = new InventoryRepository(
            dataSourceMock.Object,
            maxSlot: 2,
            maxStack: 20);

        var additionalSword = new Item(1, "Sword", 1);

        // When
        var result = await repository.AddItemAsync(additionalSword);

        // Then
        Assert.That(result, Is.True);

        dataSourceMock.Verify(
            dataSource => dataSource.LoadAllItemsAsync(),
            Times.Once);
        dataSourceMock.Verify(
            dataSource => dataSource.SaveAllItemsAsync(
                It.Is<List<Item>>(savedItems =>
                    savedItems.Count == 1 &&
                    savedItems.Single(item => item.Id == 1).Count == 2)),
            Times.Once);
    }

    [Test]
    public async Task AddItemAsync_WhenInventoryIsFull_ReturnsFalseWithoutSaving()
    {
        // Given
        var initialItems = new List<Item>
        {
            new(1, "Sword", 1),
            new(2, "Shield", 1)
        };

        var dataSourceMock = MockItemDataSource.Create(initialItems);
        var repository = new InventoryRepository(
            dataSourceMock.Object,
            maxSlot: 2,
            maxStack: 99);

        var potion = new Item(3, "Potion", 1);

        // When
        var result = await repository.AddItemAsync(potion);

        // Then
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(initialItems, Has.Count.EqualTo(2));
            Assert.That(initialItems.Any(item => item.Id == 3), Is.False);
        });

        dataSourceMock.Verify(
            dataSource => dataSource.LoadAllItemsAsync(),
            Times.Once);
        dataSourceMock.Verify(
            dataSource => dataSource.SaveAllItemsAsync(It.IsAny<List<Item>>()),
            Times.Never);
    }

    [Test]
    public async Task AddItemAsync_WhenMaxStackWouldBeExceeded_ReturnsFalseWithoutSaving()
    {
        // Given
        var initialItems = new List<Item>
        {
            new(3, "Potion", 99)
        };

        var dataSourceMock = MockItemDataSource.Create(initialItems);
        var repository = new InventoryRepository(
            dataSourceMock.Object,
            maxSlot: 10,
            maxStack: 99);

        var additionalPotion = new Item(3, "Potion", 1);

        // When
        var result = await repository.AddItemAsync(additionalPotion);

        // Then
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.False);
            Assert.That(initialItems.Single(item => item.Id == 3).Count, Is.EqualTo(99));
        });

        dataSourceMock.Verify(
            dataSource => dataSource.LoadAllItemsAsync(),
            Times.Once);
        dataSourceMock.Verify(
            dataSource => dataSource.SaveAllItemsAsync(It.IsAny<List<Item>>()),
            Times.Never);
    }
}
