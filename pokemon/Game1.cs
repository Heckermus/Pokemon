using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace pokemon;

public class Game1 : Core
{


    private SpriteFont _dogica;


    private GameObject _logo;
    private SpriteBatch _spritebatch;
    private GraphicsDevice _graphicsDevice;

    public Game1()
        : base("Pokemon", 1920, 1080, true) { }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();   
    }

    protected override void LoadContent() 
    {
        base.LoadContent();

        _spritebatch = new SpriteBatch(GraphicsDevice);

        
        
        _dogica = Content.Load<SpriteFont>("fonts/dogica");
        _logo = new GameObject(Content.Load<Texture2D>("images/player"), new Vector2 (0, 0));

        
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        _logo.ChangePosition(1, 0);
        base.Update(gameTime);
    
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spritebatch.Begin();
        _logo.Draw(_spritebatch);
        _spritebatch.DrawString(_dogica, "Der Fo", new Vector2 (10, 900), Color.White);
        _spritebatch.End();
        

        base.Draw(gameTime);
    }
}
