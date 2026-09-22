using Day09_Result_Pattern.Data.Common;
using Day09_Result_Pattern.Data.Common.Errors;
using Day09_Result_Pattern.Data.Models;
using Day09_Result_Pattern.Data.Repositories;
using Day09_Result_Pattern_Test.Mocks;

namespace Day09_Result_Pattern_Test;

public class SubwayTests
{
    [Test]
    public async Task 역이_있으면_Subway정보를_반환()
    {
        var dataSource =
            new MockSubwaySuccessDataSource();

        var repository =
            new SubwayRepository(
                dataSource
            );
        
        var result =
            await repository.GetArrivalsAsync(
                "서울"
            );
        
        Assert.That(
            result,
            Is.TypeOf<Result<List<Subway>, SubwayError>.Success>()
        );

        var successResult =
            (Result<List<Subway>, SubwayError>.Success)result;

        Assert.That(
            successResult.data,
            Is.Not.Empty
        );

        var subway =
            successResult.data[0];

        Assert.Multiple(() =>
        {
            Assert.That(
                subway.StationName,
                Is.EqualTo("서울")
            );

            Assert.That(
                subway.Direction,
                Is.EqualTo("상행")
            );

            Assert.That(
                subway.TrainLineName,
                Is.EqualTo("광운대행")
            );

            Assert.That(
                subway.TerminalStation,
                Is.EqualTo("광운대")
            );

            Assert.That(
                subway.ArrivalMessage,
                Is.EqualTo("3분 후")
            );

            Assert.That(
                subway.CurrentLocation,
                Is.EqualTo("남영")
            );

            Assert.That(
                subway.ArrivalSeconds,
                Is.EqualTo(180)
            );

            Assert.That(
                subway.ArrivalCode,
                Is.EqualTo("99")
            );
        });
    }

    [Test]
    public async Task 역이_없으면_StationNotFound를_반환()
    {
        // Arrange
        var dataSource =
            new MockSubwayNotFoundDataSource();

        var repository =
            new SubwayRepository(
                dataSource
            );

        // Act
        var result =
            await repository.GetArrivalsAsync(
                "없는역"
            );

        // Assert
        Assert.That(
            result,
            Is.TypeOf<Result<List<Subway>, SubwayError>.Error>()
        );

        var errorResult =
            (Result<List<Subway>, SubwayError>.Error)result;

        Assert.That(
            errorResult.error,
            Is.EqualTo(SubwayError.StationNotFound)
        );
    }
}