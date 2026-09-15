using Day07_http.Data.DataSources;

namespace Day07_http.Data.Mapper;

public static class ResponseMapper
{
    public static async Task<Response> ToResponse(this HttpResponseMessage response)
    {
        return new Response((int)response.StatusCode, response.Headers.ToDictionary(
            header => header.Key,
            header => string.Join(", ", header.Value)
        ), await response.Content.ReadAsStringAsync());
    }
}