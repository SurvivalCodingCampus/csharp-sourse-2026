using System;

namespace Day06_OOP_Model_N_Repository;



public class Item {
    public string Id { get; set; }
    public string Name { get; set; }
    public int Cnt { get; set; }

    public Item(string id, string name, int cnt) {
        Id = id;
        Name = name;
        Cnt = cnt;
    }
    
    //-------------------------------------------------------Alt+Ins =>자동으로 생성자, member 작성해줌
    //리스트 같은지 순서, 중복, 있는지 확인 => member setting
    protected bool Equals(Item other) {
        return Id == other.Id && Name == other.Name && Cnt == other.Cnt;
    }

    public override bool Equals(object? obj) {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Item)obj);
    }
    
    public override int GetHashCode() {
        return HashCode.Combine(Id, Name, Cnt);
    }
    //-----------------------------------------------------------------

}


