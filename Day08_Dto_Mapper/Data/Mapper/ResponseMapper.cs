using Day08_Dto_Mapper.Data.DataSources;

public static class ResponseMapper{
    public static async Task<Response2> ToResponse(this HttpResponseMessage response){
        return new Response2((int)response.StatusCode, response.Headers.ToDictionary(
            header => header.Key,
            header => string.Join(", ", header.Value)
        ), await response.Content.ReadAsStringAsync());
    }
}