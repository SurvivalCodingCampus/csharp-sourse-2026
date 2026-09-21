using Day07_http.Data;

namespace Day07_http;

public class SubwayTests
{
    private readonly Subway _subway = new();

    [Test]
    public async Task GetArrivalsAsync_ValidStation_ReturnsSuccessResult()
    {
        var result = await _subway.GetArrivalsAsync("서울");

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Value, Is.Not.Null.And.Not.Empty);
        Assert.That(result.Value![0].TrainLineNm, Is.Not.Null.And.Not.Empty);
        Assert.That(result.Value![0].ArvlMsg2, Is.Not.Null.And.Not.Empty);

        foreach (var arrival in result.Value!)
        {
            TestContext.WriteLine($"[{arrival.UpdnLine}] {arrival.TrainLineNm} - {arrival.ArvlMsg2} ({arrival.ArvlMsg3})");
        }
    }

    [Test]
    public async Task GetArrivalsAsync_InvalidStation_ReturnsNotFoundFailure()
    {
        var result = await _subway.GetArrivalsAsync("존재하지않는역");

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.ErrorType, Is.EqualTo(ErrorType.NotFound));
        Assert.That(result.StatusCode, Is.EqualTo(404));
        Assert.That(result.Error, Is.Not.Null.And.Contains("존재하지않는역"));
        TestContext.WriteLine($"에러 메시지: {result.Error}");
    }

    [Test]
    public void PrintArrivalsAsync_InvalidStation_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () => await _subway.PrintArrivalsAsync("존재하지않는역"));
    }
}
