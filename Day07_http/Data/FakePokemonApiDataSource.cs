using System.Net;
using Day07_http.Data.Interfaces;

namespace Day07_http.Data;


public class FakePokemonApiDataSource : IPokemonApiDataSource
{
    private readonly HttpStatusCode _statusCode;
    private readonly string _content;

    public FakePokemonApiDataSource(HttpStatusCode statusCode, string content)
    {
        _statusCode = statusCode;
        _content = content;
    }

    public Task<Response> GetPokemonAsync(string pokemonName)
    {
        var response = new Response
        {
            StatusCode = (int)_statusCode,
            Body = _content
        };
        return Task.FromResult(response);
    }
}
