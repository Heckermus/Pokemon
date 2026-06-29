using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using pokemon.Data;
using pokemon.Entity;

namespace pokemon.Scenes;

public class Battle : Scene
{
    private SpriteFont font;
    private PokemonInstance _yourPokemon;
    private PokemonInstance _opponentsPokemon;
    private bool playersturn = true;
    private BattleHandler bh;

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
        font = Content.Load<SpriteFont>("fonts/6x8");
        
        bh = new BattleHandler(
            _yourPokemon, 
            _opponentsPokemon, 
            Core.SpriteBatch, 
            font, 
            new Vector2(0, 0), // Startposition für den Text
            true
        );
        
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        Core.SpriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: Core.ScaleMatrix
        );
        bh.Update(gameTime);

        Core.SpriteBatch.End();
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.LightSkyBlue);
        
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        bh.Draw();
        Core.SpriteBatch.End();
        
        

        
    } 
}
