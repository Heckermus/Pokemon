using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using pokemon.Entity;


namespace pokemon.Data;



public class BattleHandler
{
    private PokemonInstance p;
    private PokemonInstance opp;
    private SpriteFont font;
    private SpriteBatch spriteBatch;
    private Vector2 pos;
    private bool playersTurn;
    private Random rnd = new Random();
    private KeyboardState previousKeyboard;
    public bool battleOver { get; set; }
    public string lastMsg { get; set; }
    
    
    public BattleHandler(PokemonInstance p, PokemonInstance opp, SpriteBatch sb, SpriteFont font, Vector2 pos, bool playersTurn)
    {
        this.p = p;
        this.opp = opp;
        this.font = font;
        this.spriteBatch = sb;
        this.pos = pos;
        this.playersTurn = playersTurn;
        previousKeyboard = Keyboard.GetState();
        battleOver = false;
    }

    public void showStats()
    {
        easywrite(p.getName(), new Vector2(100, 100));
        easywrite(p.hp + "/" + p.maxHP, new Vector2(400, 100));
        easywrite(opp.getName(), new Vector2(1400, 100));
        easywrite(opp.hp + "/" + opp.maxHP, new Vector2(1700, 100));
    }

    
    public void Update(GameTime gameTime)
    {
        if (battleOver) return;
        easywrite("hello", pos);
        KeyboardState kState = Keyboard.GetState();
        if (playersTurn)
        {
            if (WasPressed(kState, Keys.D1)) attack(p, opp, p.attack1);
            else if (WasPressed(kState, Keys.D2)) attack(p, opp, p.attack2);
            else if (WasPressed(kState, Keys.D3)) attack(p, opp, p.attack3);
        } else if (!playersTurn)
        {
            int numb = rnd.Next(1, 4);

            switch  (numb)
            {
                case 1: attack(opp, p, opp.attack1); break;
                case 2: attack(opp, p, opp.attack2); break;
                case 3: attack(opp, p, opp.attack3); break;
            }
        }

        previousKeyboard = kState;
    }
    
    private bool WasPressed(KeyboardState current, Keys key)
        => current.IsKeyDown(key) && previousKeyboard.IsKeyUp(key);

    public void Draw()
    {
        showStats();
        if (!string.IsNullOrEmpty(lastMsg)) easywrite(lastMsg, new Vector2(pos.X + 1000, pos.Y + 700));
        if (battleOver)
        {
            return;
        }
        if (playersTurn)
        {
            easywrite("It's your turn!", new Vector2(pos.X + 100, pos.Y + 700));
            easywrite("Press 1: " + p.attack1.name + " dmg: " + p.attack1.damage, new Vector2(pos.X + 100, pos.Y + 800));
            easywrite("Press 2: " + p.attack2.name + " dmg: " + p.attack2.damage, new Vector2(pos.X + 100, pos.Y + 900));
            easywrite("Press 3: " + p.attack3.name + " dmg: " + p.attack3.damage, new Vector2(pos.X + 100, pos.Y + 1000));
        }
        else
        {
            easywrite("The opp attacks!", pos);
        }
    }

    private void easywrite(string s, Vector2 position)
    {

        spriteBatch.DrawString(
            font,
            s,
            new Vector2(position.X, position.Y),
            Color.White,
            0f,
            Vector2.Zero,
            2f,
            SpriteEffects.None,
            0f
        );

    }

    private void attack(PokemonInstance attacker, PokemonInstance defender, Attack a)
    {
        if (attacker.stamina < a.ap)
        {
            lastMsg = $"{attacker.getName()} has not enough stamina for that!";
        }
        
        defender.hp = defender.hp - a.damage;
        attacker.stamina = attacker.stamina - a.ap;

        lastMsg = $"{attacker.getName()} setzt {a.name} ein!";

        if (defender.hp <= 0)
        {
            battleOver = true;
            lastMsg = $"{defender.getName()} ist tot! {attacker.getName()} gewinnt!";
            return;
        }

        playersTurn = !playersTurn;
    }
    

}