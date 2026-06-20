using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using pokemon.Entity;

namespace pokemon;

public class Game1 : Core
{
    private Tilemap _tilemap;

    private Player _player;

    public Game1()
        : base("Pokemon", 1280, 720, false, virtualWidth: 256, virtualHeight: 144) { }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");

        _tilemap = Tilemap.FromFile(Content, "images/tilemap-definition.xml");
        _player = new Player(atlas);

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _player.Update(gameTime, Input);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: ScaleMatrix);

        _tilemap.Draw(SpriteBatch);

        _player.Draw(SpriteBatch);

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
