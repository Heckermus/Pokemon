using pokemon.Data;

namespace pokemon.Entity;

public class PokemonInstance
{
    public Pokemon _basePokemon { get; }
    public int hp { get; set; }
    public int stamina { get; set; }

    private string _nickname;

    public PokemonInstance(Pokemon _basePokemon)
    {
        this._basePokemon = _basePokemon;
        hp = _basePokemon.maxHP;
        stamina = _basePokemon.maxStamina;
    }

    public string getName()
    {
        if (_nickname != null)
            return _nickname;
        return _basePokemon.name;
    }
}
