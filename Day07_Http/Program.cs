using Day07_Http.Data.DataSources;
using Day07_Http.Data.Interfaces;
using Day07_Http.Repositories;
using Newtonsoft.Json;

namespace Day07_Http;

class Program
{
    static async Task Main(string[] args)
    {
        string pokemonName = "jigglypuff";
        
        HttpClient httpClient = new HttpClient();

        IPokemonApiDataSource dataSource =
            new PokemonApiDataSource(httpClient);

        IPokemonRepository repository =
            new PokemonRepository(dataSource);

        var pokemon =
            await repository.GetPokemonByNameAsync(pokemonName);

        if (pokemon == null)
        {
            return;
        }

        var saveData = new
        {
            Name = pokemon.Name,

            Types = pokemon.Types?
                .Select(type => type.Type?.Name)
                .ToList(),

            Height = pokemon.Height / 10.0,

            Weight = pokemon.Weight / 10.0,

            Attack = pokemon.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "attack")?
                .BaseStat,

            Defense = pokemon.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "defense")?
                .BaseStat,

            Speed = pokemon.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "speed")?
                .BaseStat,

            SpecialAttack = pokemon.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "special-attack")?
                .BaseStat,

            SpecialDefense = pokemon.Stats?
                .FirstOrDefault(stat =>
                    stat.Stat?.Name == "special-defense")?
                .BaseStat
        };

        var json =
            JsonConvert.SerializeObject(
                saveData,
                Formatting.Indented
            );

        await File.WriteAllTextAsync(
            $"{pokemonName}.json",
            json
        );

        Console.WriteLine(json);
        Console.WriteLine();
        Console.WriteLine($"{pokemonName}.json 저장 완료");
    }
}