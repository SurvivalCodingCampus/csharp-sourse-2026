namespace Day05_DataSource.Model;

public class Person {
    public string Name { get; set; }
    public int Age { get; set; }
    
//class이름과 똑같아야 하는 생성자
    public Person(string name, int age) { 
        Name = name;
        Age = age;
    }
//2번을 위한 빈 생성자
    public Person() {
        
    }

    // list 정렬
    protected bool Equals(Person other) {
        return Name == other.Name && Age == other.Age;
    }

    public override bool Equals(object? obj) {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Person)obj);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Name, Age);
    }
    // list 정렬
}