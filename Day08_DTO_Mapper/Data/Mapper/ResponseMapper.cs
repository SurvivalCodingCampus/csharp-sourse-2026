using Day08_DTO_Mapper.Data.DataSources;
using Day08_DTO_Mapper.Data.DTOs;

namespace Day08_DTO_Mapper.Data.Mapper;

public static class ResponseMapper
{
    public static async Task<Response> ToResponse(this HttpResponseMessage response)
    {
        return new Response(
                (int)response.StatusCode,
                response.Headers.ToDictionary(
                    header => header.Key,
                    header => string.Join(", ", header.Value)),
                await response.Content.ReadAsStringAsync()
            );
    }
}