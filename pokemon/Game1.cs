using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace pokemon;

public class Game1 : Core
{


    private SpriteFont _dogica;
    private Pokemon diddy;
    private GameObject _logo;
    private SpriteBatch _spritebatch;
    private GraphicsDevice _graphicsDevice;
    private Battle b;

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
        diddy = PokemonConstructor.create(2);
        Console.WriteLine(diddy.name);
        
        _dogica = Content.Load<SpriteFont>("fonts/dogica");
        _logo = new GameObject(Content.Load<Texture2D>("images/player"), new Vector2 (0, 0));
        b = new Battle(diddy, PokemonConstructor.create(4));
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
        )
            Exit();

        if (
            GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Space)
        )
            _logo.ChangePosition(1, 1);
            
        _logo.ChangePosition(1, 0);
        base.Update(gameTime);
    
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        b.Draw(_spritebatch, _dogica);
        Console.WriteLine(PokemonConstructor.getEffective("Psycho")[1]);
        

        base.Draw(gameTime);
    }
}
