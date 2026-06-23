namespace pokemon.Data;

public class Pokemon
{
    public string name { get; }
    public Type type { get; }
    public int maxHP { get; }
    public double attackMult { get; }
    public double defenseMult { get; }
    public double specialAttackMult { get; }
    public double specialDefenseMult { get; }
    public int maxStamina { get; }
    public Attack attack1 { get; } //FIXME: type specific scheiß auch in pokemon speichern sonst machen wir uns das leben schwer
    public Attack attack2 { get; }
    public Attack attack3 { get; }

    public Pokemon(
        string name,
        Type type,
        int maxHP,
        double attackMult,
        double defenseMult,
        double specialAttackMult,
        double specialDefenseMult,
        int maxStamina,
        Attack attack1,
        Attack attack2,
        Attack attack3
    )
    {
        this.name = name;
        this.type = type;
        this.maxHP = maxHP;
        this.attackMult = attackMult;
        this.defenseMult = defenseMult;
        this.specialAttackMult = specialAttackMult;
        this.specialDefenseMult = specialDefenseMult;
        this.maxStamina = maxStamina;
        this.attack1 = attack1;
        this.attack2 = attack2;
        this.attack3 = attack3;
    }
}
