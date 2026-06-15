using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace pokemon;

public class Game1 : Core
{
    private Texture2D _logo;
    private SpriteFont _dogica;


    public Game1()
        : base("Pokemon", 1920, 1080, true) { }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent() 
    {
        _logo = Content.Load<Texture2D>("images/player");
        TextureAtlas atlas = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");

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
        SpriteBatch.Draw(_logo, new Vector2 (1700, 100), Color.White);
        _dogica = Content.Load<SpriteFont>("dogica");
        

        SpriteBatch.End();
        

        base.Draw(gameTime);
    }
}
