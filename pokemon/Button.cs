using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class Button
{
    public Rectangle Bounds { get; set; }
    public Texture2D Texture { get; set; }
    public string Text { get; set; }

    public Button(Texture2D texture, Rectangle bounds, string text)
    {
        Texture = texture;
        Bounds = bounds;
        Text = text;
    }

    public bool IsClicked(MouseState mouse)
    {
        return Bounds.Contains(mouse.Position) 
            && mouse.LeftButton == ButtonState.Pressed;
    }

    public void Draw(SpriteBatch spriteBatch, SpriteFont font)
    {
        spriteBatch.Draw(Texture, Bounds, Color.White);
        spriteBatch.DrawString(font, Text, new Vector2(Bounds.X + 10, Bounds.Y + 10), Color.Black);
    }
}