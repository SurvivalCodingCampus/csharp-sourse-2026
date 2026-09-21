namespace Day07_http.Data.Mappers;

public static class ResponseMapper
{
    public static async Task<Response> ToResponseAsync(this HttpResponseMessage httpResponse)
    {
        var body = await httpResponse.Content.ReadAsStringAsync();

        return new Response
        {
            StatusCode = (int)httpResponse.StatusCode,
            Body = body
        };
    }
}
