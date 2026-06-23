using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using pokemon.Entity;

namespace pokemon.Scenes;

public class GameScene : Scene
{
    private Tilemap _tilemap;

    private Player _player;

    public override void Initialize()
    {
        base.Initialize();

        Core.ExitOnEscape = true;
    }

    public override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "images/atlas-definition.xml");

        _player = new Player(atlas);

        _tilemap = Tilemap.FromFile(Core.Content, "images/tilemap-definition.xml");
    }

    public override void Update(GameTime gameTime)
    {
        _player.Update(gameTime, Core.Input);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.Black);

        Core.SpriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: Core.ScaleMatrix
        );

        _tilemap.Draw(Core.SpriteBatch);
        _player.Draw(Core.SpriteBatch);

        Core.SpriteBatch.End();
    }
}
