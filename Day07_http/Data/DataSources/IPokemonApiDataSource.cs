namespace Day07_http.Data.DataSources;

public interface IPokemonApiDataSource<T>
{
    // C# 14. 네트워크 통신 p.41 참조
    // 데이터 소스: CRUD 기능을 정의하고 응답 객체를 반환
    Task<Response> GetAllAsync();
    Task<Response> GetPokemonAsync(string name);
    Task<Response> CreateAsync(T pokemon);
    Task<Response> UpdateAsync(T pokemon);
    Task<Response> DeleteAsync(string name);
}