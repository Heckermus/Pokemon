using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;

namespace pokemon.Entity;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}

public class Player
{
    private Dictionary<(Direction, bool isMoving), AnimatedSprite> _animations;
    private AnimatedSprite _currentAnimation;
    private Direction _facing = Direction.Down;
    private Vector2 _position;
    private const float SPEED = 1.0f;

    public Player(TextureAtlas atlas)
    {
        _animations = new Dictionary<(Direction, bool), AnimatedSprite>
        {
            [(Direction.Down, false)] = atlas.CreateAnimatedSprite("playerIdleDown"),
            [(Direction.Up, false)] = atlas.CreateAnimatedSprite("playerIdleUp"),
            [(Direction.Left, false)] = atlas.CreateAnimatedSprite("playerIdleLeft"),
            [(Direction.Right, false)] = atlas.CreateAnimatedSprite("playerIdleRight"),
            [(Direction.Down, true)] = atlas.CreateAnimatedSprite("playerWalkDown"),
            [(Direction.Up, true)] = atlas.CreateAnimatedSprite("playerWalkUp"),
            [(Direction.Left, true)] = atlas.CreateAnimatedSprite("playerWalkLeft"),
            [(Direction.Right, true)] = atlas.CreateAnimatedSprite("playerWalkRight"),
        };

        _currentAnimation = _animations[(_facing, false)];
    }

    public void Update(GameTime gameTime, InputManager input)
    {
        bool isMoving = false;
        Direction newFacing = _facing;

        if (input.Keyboard.IsKeyDown(Keys.W))
        {
            _position.Y -= SPEED;
            newFacing = Direction.Up;
            isMoving = true;
        }
        if (input.Keyboard.IsKeyDown(Keys.S))
        {
            _position.Y += SPEED;
            newFacing = Direction.Down;
            isMoving = true;
        }
        if (input.Keyboard.IsKeyDown(Keys.A))
        {
            _position.X -= SPEED;
            newFacing = Direction.Left;
            isMoving = true;
        }
        if (input.Keyboard.IsKeyDown(Keys.D))
        {
            _position.X += SPEED;
            newFacing = Direction.Right;
            isMoving = true;
        }

        var nextAnimation = _animations[(newFacing, isMoving)];
        if (nextAnimation != _currentAnimation)
        {
            _currentAnimation = nextAnimation;
        }

        _facing = newFacing;

        _currentAnimation.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _currentAnimation.Draw(spriteBatch, _position);
    }
}
