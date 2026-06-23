using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics.Tracing;

public class Pokemon
{
    public string name {get;}
    public string nickname {get; set;}
    public int id {get;}
    public string type {get;}
    public int maxHP {get; set;}
    public int hp {get; set;}
    public double attackMult {get; set;}
    public double defenseMult {get; set;}
    public double specialAttackMult {get; set;}
    public double specialDefenseMult {get; set;}
    public int maxStamina {get; set;}
    public int stamina {get; set;}
    public Attack attack1 {get; set;}
    public Attack attack2 {get; set;}
    public Attack attack3 {get; set;}

    public Pokemon(string Name, int Id, string Type, int MaxHP, int Hp, double AttackMult, double DefenseMult, double SpecialAttackMult, double SpecialDefenseMult, int Stamina, int MaxStamina, Attack Attack1, Attack Attack2, Attack Attack3)
    {
        name = Name;
        id = Id;
        type = Type;
        maxHP = MaxHP;
        hp = Hp;
        attackMult = AttackMult;
        defenseMult = DefenseMult;
        specialAttackMult = SpecialAttackMult;
        specialDefenseMult = SpecialDefenseMult;
        stamina = Stamina;
        maxStamina = MaxStamina;
        attack1 = Attack1;
        attack2 = Attack2;
        attack3 = Attack3;
    }

    public string getName()
    {
        if (nickname != null)
        {
            return nickname;
        }
        return name;
    }
}