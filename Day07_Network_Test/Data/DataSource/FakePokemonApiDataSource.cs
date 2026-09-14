using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Day07_Network.Data.DataSource;
using Day07_Network.Data.Interface;
using Newtonsoft.Json;

namespace Day07_Network_Test.Data.DataSource;

public class FakePokemonApiDataSource : IPokemonApiDataSource
{
    private readonly string _fakeJson = """
                                        {
                                          "id": 145,
                                          "name": "zapdos",
                                          "types": [
                                            {
                                              "slot": 1,
                                              "type": {
                                                "name": "electric",
                                                "url": "https://pokeapi.co/api/v2/type/13/"
                                              }
                                            },
                                            {
                                              "slot": 2,
                                              "type": {
                                                "name": "flying",
                                                "url": "https://pokeapi.co/api/v2/type/3/"
                                              }
                                            }
                                          ],
                                          "sprites": {
                                              "other": {
                                                "home": {
                                                  "front_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/home/shiny/145.png",
                                                  "front_female": null,
                                                  "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/home/145.png",
                                                  "front_shiny_female": null
                                                },
                                                "showdown": {
                                                  "back_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/back/shiny/145.gif",
                                                  "back_female": null,
                                                  "front_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/shiny/145.gif",
                                                  "back_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/back/145.gif",
                                                  "front_female": null,
                                                  "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/showdown/145.gif",
                                                  "back_shiny_female": null,
                                                  "front_shiny_female": null
                                                },
                                                "dream_world": {
                                                  "front_female": null,
                                                  "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/dream-world/145.svg"
                                                },
                                                "official-artwork": {
                                                  "front_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/shiny/145.png",
                                                  "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/145.png"
                                                }
                                               },
                                            "back_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/shiny/145.png",
                                            "back_female": null,
                                            "front_shiny": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/145.png",
                                            "back_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/145.png",
                                            "front_female": null,
                                            "front_default": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/145.png",
                                            "back_shiny_female": null,
                                            "front_shiny_female": null
                                          }
                                        }

                                        """;

    public Task<Response> GetPokemonAsync(string pokemonName)
    {
        var response = new Response(200,
            new Dictionary<string, string>(),
            _fakeJson);
        return Task.FromResult(response);
    }
}