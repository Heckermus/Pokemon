using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


public class Battle
{
    private Pokemon _you;
    private Pokemon _opp;
    private MouseState mouse = Mouse.GetState();
    //private Button _attack1b = new Button(("images/player"), )

    public Battle(Pokemon you, Pokemon opp) {
        _you = you;
        _opp = opp;
    }

    public void Draw(SpriteBatch sb, SpriteFont dg)
    {
        updateUI(sb, dg);
    }

    private void updateUI(SpriteBatch sb, SpriteFont dg)
    {
        sb.Begin(samplerState: SamplerState.PointClamp);
        sb.DrawString(dg, _you.getName(), new Vector2(100, 700), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        sb.DrawString(dg, _you.hp + "/" + _you.maxHP, new Vector2(400, 700), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        sb.DrawString(dg, _you.attack1.name + " dmg:" + _you.attack1.damage, new Vector2(100, 800), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        sb.DrawString(dg, _you.attack2.name + " dmg:" + _you.attack2.damage, new Vector2(100, 900), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        sb.DrawString(dg, _you.attack3.name + " dmg:" + _you.attack3.damage, new Vector2(100, 1000), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);

        sb.DrawString(dg, _opp.getName(), new Vector2(1500, 80), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        sb.DrawString(dg, _opp.hp + "/" + _opp.maxHP, new Vector2(1700, 80), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        sb.DrawString(dg, _opp.attack1.name + " dmg:" + _opp.attack1.damage, new Vector2(1500, 180), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        sb.DrawString(dg, _opp.attack2.name + " dmg:" + _opp.attack2.damage, new Vector2(1500, 280), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        sb.DrawString(dg, _opp.attack3.name + " dmg:" + _opp.attack3.damage, new Vector2(1500, 380), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        sb.End();
    }
}