using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using pokemon.Entity;

namespace pokemon.Scenes;

public class Battle : Scene
{
    private PokemonInstance _yourPokemon;
    private PokemonInstance _opponentsPokemon;
    private MouseState mouse = Mouse.GetState();

    public Battle(PokemonInstance yourPokemon, PokemonInstance opponentsPokemon)
    {
        this._yourPokemon = yourPokemon;
        this._opponentsPokemon = opponentsPokemon;
    }

    public override void Initialize()
    {
        base.Initialize();

        Core.ExitOnEscape = true;
    }

    public override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "images/atlas-definition.xml");

        base.LoadContent();
    }

    public override void Update(GameTime gameTime) { }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.Black);

        Core.SpriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: Core.ScaleMatrix
        );

        Core.SpriteBatch.End();
    }

    /*private void updateUI(SpriteBatch sb, SpriteFont dg)
    {
        sb.Begin(samplerState: SamplerState.PointClamp);
        sb.DrawString(
            dg,
            _yourPokemon.getName(),
            new Vector2(100, 700),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );
        sb.DrawString(
            dg,
            _yourPokemon.hp + "/" + _yourPokemon.maxHP,
            new Vector2(400, 700),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );
        sb.DrawString(
            dg,
            _yourPokemon.attack1.name + " dmg:" + _yourPokemon.attack1.damage,
            new Vector2(100, 800),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );
        sb.DrawString(
            dg,
            _yourPokemon.attack2.name + " dmg:" + _yourPokemon.attack2.damage,
            new Vector2(100, 900),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );
        sb.DrawString(
            dg,
            _yourPokemon.attack3.name + " dmg:" + _yourPokemon.attack3.damage,
            new Vector2(100, 1000),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );

        sb.DrawString(
            dg,
            _opponentsPokemon.getName(),
            new Vector2(1500, 80),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );
        sb.DrawString(
            dg,
            _opponentsPokemon.hp + "/" + _opponentsPokemon.maxHP,
            new Vector2(1700, 80),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );
        sb.DrawString(
            dg,
            _opponentsPokemon.attack1.name + " dmg:" + _opponentsPokemon.attack1.damage,
            new Vector2(1500, 180),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );
        sb.DrawString(
            dg,
            _opponentsPokemon.attack2.name + " dmg:" + _opponentsPokemon.attack2.damage,
            new Vector2(1500, 280),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );
        sb.DrawString(
            dg,
            _opponentsPokemon.attack3.name + " dmg:" + _opponentsPokemon.attack3.damage,
            new Vector2(1500, 380),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );
        sb.End();
    }*/
}
