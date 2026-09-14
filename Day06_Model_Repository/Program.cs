using Day06_Model_Repository.DataSources;
using Day06_Model_Repository.Interfaces;
using Day06_Model_Repository.Models;
using Day06_Model_Repository.Tests;

namespace Day06_Model_Repository;

class Program
{
    static async Task Main(string[] args)
    {
        MockItemDataSource test = new MockItemDataSource();
        
        await test.TestCase1();
        await test.TestCase2();
        await test.TestCase3();
        await test.TestCase4();
        await test.TestCase5();

    }
}