using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;

namespace pokemon.Entity;

public class PokemonVisual
{
    private Sprite _pokemon;
    private Vector2 _position;
    private double timer;

    public PokemonVisual(TextureAtlas atlas, Vector2 pos)
    {
        _pokemon = atlas.CreateSprite("pokemon");
        _position = pos;
    }

    public void Update(GameTime gameTime)
    {
        timer += gameTime.ElapsedGameTime.TotalSeconds;

        if (timer >= 0.5)
        {
            _pokemon.Color = Color.White;
            timer = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _pokemon.Draw(spriteBatch, _position);
    }

    public void TakeDamageVisual()
    {
        _pokemon.Color = Color.Black;
    }
}
