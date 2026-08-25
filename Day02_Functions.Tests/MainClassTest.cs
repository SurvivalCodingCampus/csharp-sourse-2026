using System.Collections.Generic;
using System.Linq;
using Day02_Functions;
using NUnit.Framework;

namespace Day02_Functions.Tests;

[TestFixture]
public class TransactionTests
{
    private List<Transaction> CreateTestData()
    {
        return new List<Transaction>
        {
            new Transaction(new Trader("Brian", "Cambridge"), 2011, 300),
            new Transaction(new Trader("Raoul", "Cambridge"), 2012, 1000),
            new Transaction(new Trader("Raoul", "Cambridge"), 2011, 400),
            new Transaction(new Trader("Mario", "Milan"), 2012, 710),
            new Transaction(new Trader("Mario", "Milan"), 2012, 700),
            new Transaction(new Trader("Alan", "Cambridge"), 2012, 950),
        };
    }

    [Test]
    public void Test_2011년_거래_가격기준_이름asc()
    {
        var transactions = CreateTestData();

        var result = transactions
            .Where(t => t.Year == 2011)
            .OrderBy(t => t.Value)
            .Select(t => t.Trader.Name)
            .ToList();

        Assert.That(result, Is.EqualTo(new List<string> { "Brian", "Raoul" }));
    }

    [Test]
    public void Test_도시목록_distinct()
    {
        var transactions = CreateTestData();

        var result = transactions
            .Select(t => t.Trader.City)
            .Distinct()
            .ToList();

        Assert.That(result, Is.EqualTo(new List<string> { "Cambridge", "Milan" }));
    }

    [Test]
    public void Test_케임브리지_거래자_이름asc()
    {
        var transactions = CreateTestData();

        var result = transactions
            .Where(t => t.Trader.City == "Cambridge")
            .Select(t => t.Trader.Name)
            .Distinct()
            .OrderBy(name => name)
            .ToList();

        Assert.That(result, Is.EqualTo(new List<string> { "Alan", "Brian", "Raoul" }));
    }

    [Test]
    public void Test_전체_거래자_이름asc()
    {
        var transactions = CreateTestData();

        var result = transactions
            .Select(t => t.Trader.Name)
            .Distinct()
            .OrderBy(name => name)
            .ToList();

        Assert.That(result, Is.EqualTo(new List<string> { "Alan", "Brian", "Mario", "Raoul" }));
    }

    [Test]
    public void Test_밀라노_거래자_유무()
    {
        var transactions = CreateTestData();

        bool result = transactions.Any(t => t.Trader.City == "Milan");

        Assert.That(result, Is.True);
    }

    [Test]
    public void Test_케임브리지_거래자_트랜잭션값()
    {
        var transactions = CreateTestData();

        var result = transactions
            .Where(t => t.Trader.City == "Cambridge")
            .Select(t => t.Value)
            .ToList();

        Assert.That(result, Is.EqualTo(new List<int> { 300, 1000, 400, 950 }));
    }

    [Test]
    public void Test_최대_트랜잭션값()
    {
        var transactions = CreateTestData();

        int result = transactions.Max(t => t.Value);

        Assert.That(result, Is.EqualTo(1000));
    }

    [Test]
    public void Test_최소_트랜잭션값()
    {
        var transactions = CreateTestData();

        int result = transactions.Min(t => t.Value);

        Assert.That(result, Is.EqualTo(300));
    }
}