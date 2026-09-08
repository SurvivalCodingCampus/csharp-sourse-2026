using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Day05_DataSource;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day04_DataSource.Tests;

[TestClass]
[TestSubject(typeof(JsonFileDataSource))]
public class DataSourceTest
{
    private string _tempFilePath = string.Empty;
    private IDataSoure _dataSource = null!;

    [TestInitialize]
    public void Setup()
    {
        // 각 테스트마다 서로 영향을 주지 않도록 고유한 임시 파일 경로를 할당
        _tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.json");
        _dataSource = new JsonFileDataSource(_tempFilePath);
    }

    [TestCleanup]
    public void Cleanup()
    {
        // 테스트 완료 후 생성된 임시 파일 삭제
        if (File.Exists(_tempFilePath))
        {
            File.Delete(_tempFilePath);
        }
    }

    [TestMethod]
    public async Task GetPeopleAsync_파일이없을때_빈리스트반환()
    {
        // Act
        var result = await _dataSource.GetPeopleAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task SavePeopleAsync_저장후불러오기_데이터일치검증()
    {
        // Arrange
        var testData = new List<Person>
        {
            new Person { Name = "홍길동", Age = 25 },
            new Person { Name = "이민준", Age = 20 }
        };

        // Act: 저장 후 다시 읽기
        await _dataSource.SavePeopleAsync(testData);
        var loadedData = await _dataSource.GetPeopleAsync();

        // Assert
        Assert.AreEqual(2, loadedData.Count);
        Assert.AreEqual("홍길동", loadedData[0].Name);
        Assert.AreEqual(25, loadedData[0].Age);
        Assert.AreEqual("이민준", loadedData[1].Name);
        Assert.AreEqual(20, loadedData[1].Age);
    }

    [TestMethod]
    public async Task Program_필터링및삭제시나리오_검증()
    {
        // Arrange: Main 메서드 시나리오와 동일한 흐름 테스트
        var people = new List<Person>
        {
            new Person { Name = "홍길동", Age = 25 },
            new Person { Name = "이민준", Age = 20 }
        };
        await _dataSource.SavePeopleAsync(people);

        // Act 1: 데이터 로드 및 성인 필터링
        var loaded = await _dataSource.GetPeopleAsync();
        var adults = loaded.Where(p => p.Age >= 19).ToList();
        Assert.AreEqual(2, adults.Count);

        // Act 2: 미성년자 추가 후 저장
        loaded.Add(new Person { Name = "김민지", Age = 15 });
        await _dataSource.SavePeopleAsync(loaded);

        // Act 3: 특정 인물(이민준) 삭제 후 저장
        var reloaded = await _dataSource.GetPeopleAsync();
        var toDelete = reloaded.FirstOrDefault(p => p.Name == "이민준");
        Assert.IsNotNull(toDelete);
        
        reloaded.Remove(toDelete);
        await _dataSource.SavePeopleAsync(reloaded);

        // Final Assert: 최종 파일 상태 확인
        var finalData = await _dataSource.GetPeopleAsync();
        Assert.AreEqual(2, finalData.Count); // 홍길동, 김민지만 남아야 함
        Assert.IsFalse(finalData.Any(p => p.Name == "이민준"));
        Assert.IsTrue(finalData.Any(p => p.Name == "김민지"));
    }
}