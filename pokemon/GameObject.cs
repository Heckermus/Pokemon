using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObject
{
    public Texture2D _texture { get; set; }
    public Vector2 _position { get; set; }

    public GameObject(Texture2D Texture, Vector2 startPosition)
    {
        _texture = Texture;
        _position = startPosition;
    }

    public void ChangePosition(float deltaX, float deltaY)
    {
        _position = new Vector2(_position.X + deltaX, _position.Y + deltaY);
    }

    // NUR DEN SPRITEBATCH AUS MAIN UEBERGEBEN!
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, Color.White);
    }
}