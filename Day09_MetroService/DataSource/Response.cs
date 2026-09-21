namespace Day09_MetroService.DataSource;
public sealed record Response<T>(int StatusCode, T? Body);
