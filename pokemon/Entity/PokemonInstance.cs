using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using pokemon.Data;

namespace pokemon.Entity;

public class PokemonInstance
{
    //private AnimatedSprite _pokemon;
    private Pokemon _basePokemon;
    private Vector2 _position;

    private int hp;
    private int maxHP;
    private double attackFactor;
    private double defenseFactor;
    private double specialAttackFactor;
    private double specialDefenseFactor;
    private int stamina;

    private string _nickname;

    public PokemonInstance(Pokemon _basePokemon, TextureAtlas atlas)
    {
        this._basePokemon = _basePokemon;
        //_pokemon = atlas.CreateAnimatedSprite(_basePokemon.id);
    }

    public void Update(GameTime gameTime, InputManager input)
    {
        //_pokemon.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        //_pokemon.Draw(spriteBatch, _position);
    }

    public string getNickname()
    {
        if (_nickname != null) return _nickname;
        return _basePokemon.name;
    }
}
