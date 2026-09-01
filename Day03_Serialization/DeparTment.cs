namespace Day03_Serialization;

class DeparTment
{
    public string Name { get; }
    public Emplovee leader { get; }

    public DeparTment(string name, Emplovee leader)
    {
        Name = name;
        this.leader = leader;
    }
}