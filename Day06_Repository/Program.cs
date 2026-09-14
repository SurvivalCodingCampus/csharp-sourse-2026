using Day06_Repository.Data.DataSources;
using Day06_Repository.Data.Repositories;

namespace Day06_Repository;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        IInventoryRepository repository = new InventoryRepository(
            dataSource: null,
            maxStack: 30
        );
    }
}