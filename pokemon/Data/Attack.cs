namespace pokemon.Data;

public class Attack
{
    public string name { get; }
    public int damage { get; }
    public int ap { get; }
    public bool special { get; }

    public Attack(string Name, int Damage, int Ap, bool Special)
    {
        name = Name;
        damage = Damage;
        ap = Ap;
        special = Special;
    }
}
