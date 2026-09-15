# Day 01 TIL

- 이름: 장종민
- 작성일: 2026-09-14

## 1. 오늘 막힌 부분 또는 내린 판단

PokeAPI에서 포켓몬 정보를 가져올 때 단순히 이름만 가져오는 것이 아니라  
속성, 키, 몸무게, 공격력, 방어력, 스피드, 특수공격, 특수방어까지 가져왔다.

## 2. 수정 전과 수정 후

### 수정 전

```csharp
Pokemon.cs
    
public class Pokemon
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("sprites")]
    public OtherSprites? Sprites { get; set; }
}

Program.cs
    
var pokemon =
    await repository.GetPokemonByNameAsync("pikachu");

Console.WriteLine($"이름: {pokemon?.Name}");

```

### 수정 후

```csharp
Pokemon.cs

public class Pokemon
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }

    [JsonProperty("weight")]
    public int Weight { get; set; }

    [JsonProperty("types")]
    public List<PokemonType>? Types { get; set; }

    [JsonProperty("stats")]
    public List<PokemonStat>? Stats { get; set; }
}

Program.cs

string pokemonName = "jigglypuff";

var pokemon =
    await repository.GetPokemonByNameAsync(pokemonName);

var saveData = new
{
    Name = pokemon?.Name,

    Types = pokemon?.Types?
        .Select(type => type.Type?.Name)
        .ToList(),

    Height = pokemon!.Height / 10.0,

    Weight = pokemon.Weight / 10.0,

    Attack = pokemon.Stats?
        .FirstOrDefault(stat =>
            stat.Stat?.Name == "attack")?
        .BaseStat,

    Defense = pokemon.Stats?
        .FirstOrDefault(stat =>
            stat.Stat?.Name == "defense")?
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
```

PokeAPI에서 반환되는 JSON 구조에 맞춰 `types`, `stats`, `height`, `weight` 데이터를 받을 수 있도록  
모델을 추가했다.

또한 포켓몬 이름을 `pokemonName` 변수로 관리하여 다른 포켓몬으로 변경할 때 한 곳만 수정하면 되도록 했다.

가져온 전체 데이터를 그대로 저장하지 않고 필요한 정보만 `saveData`에 담아 JSON 파일로 저장하도록 수정했다.

## 3. AI 사용 여부와 채택, 거절한 이유

- AI 사용 여부: 사용함
- 질문: PokeAPI에서 속성과 능력치 정보를 어떻게 가져오는지 질문함
- 제안받은 내용: API의 JSON 구조에서 속성은 `types`, 능력치는 `stats` 배열에 들어 있으므로  
해당 구조에 맞는 모델 클래스를 만들어 역직렬화하는 방법을 제안받음
- 채택 또는 거절한 내용: `types`와 `stats` 구조에 맞는 모델 클래스를 추가하는 방법을 채택함
- 판단한 이유: 실제 PokeAPI의 JSON 구조와 비교하여 속성과 능력치 데이터가   
해당 배열에 들어 있는 것을 확인했고, 필요한 정보를 정상적으로 가져올 수 있었음

## 4. 검증 결과

- 빌드: 성공
- 실행 결과: `{
           "Name": "jigglypuff",
           "Types": [
             "normal",
             "fairy"
           ],
           "Height": 0.5,
           "Weight": 5.5,
           "Attack": 45,
           "Defense": 20,
           "Speed": 20,
           "SpecialAttack": 45,
           "SpecialDefense": 25
         }`

- 추가로 확인한 내용: 없음

## 5. 아직 궁금한 점

테스트 코드에 반복해서 사용되는 매직 스트링과 매직 넘버를 줄이고,  
여러 포켓몬 테스트에서 코드를 효율적으로 재사용하는 방법이 궁금하다.

## 6. 다음에 적용할 것

테스트 코드에서 반복되는 포켓몬 이름과 능력치 값을 변수나 상수로 분리하여  
중복을 줄이고 코드를 더 쉽게 수정할 수 있도록 작성해보겠다.