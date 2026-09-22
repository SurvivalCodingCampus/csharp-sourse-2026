using Day09_Result.Data.Common;
using Day09_Result.Data.Common.Errors;
using Day09_Result.Data.Models;
using Day09_Result.Data.Repositories;
using Day09_Result_Test.Data.DataSource;
using Xunit;

namespace Day09_Result_Test.Repositories;

public sealed class SubwayRepositoryTest
{
    [Fact]
    public async Task 역_도착_정보가_있으면_Success를_반환한다()
    {
        var successRepository = new SubwayRepository(new SuccessSubwayMockApiDataSource());

        Result<IReadOnlyList<Subway>, SubwayError> success =
            await successRepository.GetArrivalsByStationAsync("서울");

        var result = Assert.IsType<Result<IReadOnlyList<Subway>, SubwayError>.Success>(success);
        Assert.Single(result.Data);
    }

    [Fact]
    public async Task 존재하지_않는_역은_StationNotFound를_반환한다()
    {
        var notFoundRepository = new SubwayRepository(new NotFoundSubwayMockApiDataSource());

        Result<IReadOnlyList<Subway>, SubwayError> notFound =
            await notFoundRepository.GetArrivalsByStationAsync("없는역");

        var result = Assert.IsType<Result<IReadOnlyList<Subway>, SubwayError>.Failure>(notFound);
        Assert.Equal(SubwayError.StationNotFound, result.Error);
    }
}