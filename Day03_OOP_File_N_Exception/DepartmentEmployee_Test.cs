using System.Reflection.Metadata;
using System.Text.Json;

namespace Day03_OOP_FileAndException;

public class DepartmentEmployee_Test
{
    static Employee e = new Employee("홍홍", 41);
    Department d = new Department("총무이름", e) ;

    private JsonSerializerOptions options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    // public string jsonString = JsonSerializer.Serialize(user, options);
} 
