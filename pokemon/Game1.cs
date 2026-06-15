using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace pokemon;

public class Game1 : Core
{
    private TextureRegion _grass;
    private TextureRegion _stone;

    public Game1()
        : base("Pokemon", 1920, 1080, true) { }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Texture2D atlasTexture = Content.Load<Texture2D>("images/atlas");
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");

        _grass = atlas.GetRegion("grass");
        _stone = atlas.GetRegion("stone");

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        SpriteBatch.Begin();
        _grass.Draw(
            SpriteBatch,
            Vector2.Zero,
            Color.White,
            0.0f,
            Vector2.One,
            1.0f,
            SpriteEffects.None,
            0.0f
        );
        _stone.Draw(
            SpriteBatch,
            new Vector2(32, 32),
            Color.White,
            0.0f,
            Vector2.One,
            1.0f,
            SpriteEffects.None,
            0.0f
        );

        SpriteBatch.End();

        base.Draw(gameTime);
    }
}
