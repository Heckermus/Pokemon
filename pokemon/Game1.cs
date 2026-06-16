using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace pokemon;

public class Game1 : Core
{


    private SpriteFont _dogica;
    private PokemonConstructor pc;
    private Pokemon diddy;
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
        pc = new PokemonConstructor();
        diddy = pc.create(1);
        Console.WriteLine(diddy.name);
        
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

        _spritebatch.Begin();
        _logo.Draw(_spritebatch);
        _spritebatch.DrawString(_dogica, diddy.getName(), new Vector2(100, 700), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        _spritebatch.DrawString(_dogica, diddy.hp + "/" + diddy.maxHP, new Vector2(400, 700), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        _spritebatch.DrawString(_dogica, diddy.attack1.name + " dmg:" + diddy.attack1.damage, new Vector2(100, 800), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        _spritebatch.DrawString(_dogica, diddy.attack2.name + " dmg:" + diddy.attack2.damage, new Vector2(100, 900), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        _spritebatch.DrawString(_dogica, diddy.attack3.name + " dmg:" + diddy.attack3.damage, new Vector2(100, 1000), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        _spritebatch.End();
        

        base.Draw(gameTime);
    }
}
