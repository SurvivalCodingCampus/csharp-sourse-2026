using Day09_Result_패턴.Data.DataSources;

namespace Day09_Result_패턴.Data.Mapper;

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