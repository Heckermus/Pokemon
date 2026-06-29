namespace pokemon.Data;

public class Attack
{
    public string name { get; }
    public Type type;
    public int damage { get; }
    public int ap { get; }
    public bool special { get; }

    public Attack(string Name, Type Type, int Damage, int Ap, bool Special)
    {
        name = Name;
        type = Type;
        damage = Damage;
        ap = Ap;
        special = Special;
    }
}
