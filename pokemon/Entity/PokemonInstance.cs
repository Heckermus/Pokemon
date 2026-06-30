using pokemon.Data;

namespace pokemon.Entity;

public class PokemonInstance
{
    public Pokemon _basePokemon { get; }
    public int hp { get; set; }
    public int maxHP { get; set; }
    public double attackFactor { get; set; }
    public double defenseFactor { get; set; }
    public double specialAttackFactor { get; set; }
    public double specialDefenseFactor { get; set; }
    public int maxStamina { get; set; }
    public int stamina { get; set; }
    public Attack attack1 { get; set; }
    public Attack attack2 { get; set; }
    public Attack attack3 { get; set; }
    private string _nickname;

    public PokemonInstance(Pokemon _basePokemon)
    {
        this._basePokemon = _basePokemon;
        this.hp = _basePokemon.maxHP;
        this.maxHP = _basePokemon.maxHP;
        this.defenseFactor = _basePokemon.defenseMult;
        this.attackFactor = _basePokemon.attackMult;
        this.specialAttackFactor = _basePokemon.specialAttackMult;
        this.specialDefenseFactor = _basePokemon.specialDefenseMult;
        this.maxStamina = _basePokemon.maxStamina;
        this.stamina = _basePokemon.maxStamina;
        this.attack1 = _basePokemon.attack1;
        this.attack2 = _basePokemon.attack2;
        this.attack3 = _basePokemon.attack3;

        //_pokemon = atlas.CreateAnimatedSprite(_basePokemon.id);
    }

    public string getName()
    {
        if (_nickname != null)
            return _nickname;
        return _basePokemon.name;
    }
}
