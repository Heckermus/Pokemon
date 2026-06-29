using System;
using Microsoft.VisualBasic;
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
    public string additionalMsg { get; set; }
    private double messageTimer = 0;
    private const double MessageDur = 2.0;

    private PokemonInstance dead;
    private PokemonInstance winner;
    
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
        additionalMsg = "";
    }

    public void ShowStats()
    {
        Easywrite(p.getName(), new Vector2(100, 100));
        Easywrite(p.hp + "/" + p.maxHP, new Vector2(400, 100));
        Easywrite(opp.getName(), new Vector2(1400, 100));
        Easywrite(opp.hp + "/" + opp.maxHP, new Vector2(1700, 100));
    }

    
    public void Update(GameTime gameTime)
    {
        messageTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (battleOver)
        {
            if (messageTimer >= MessageDur)
            {
                lastMsg = $"{dead.getName()} is dead! {winner.getName()} wins!";
            }
            return;
        }
        
        KeyboardState kState = Keyboard.GetState();
        if (playersTurn)
        {
            if (WasPressed(kState, Keys.D1)) Attack(p, opp, p.attack1);
            else if (WasPressed(kState, Keys.D2)) Attack(p, opp, p.attack2);
            else if (WasPressed(kState, Keys.D3)) Attack(p, opp, p.attack3);
        } else if (!playersTurn && messageTimer >= MessageDur)
        {
            int numb = rnd.Next(1, 4);

            switch (numb)
            {
                case 1: Attack(opp, p, opp.attack1); break;
                case 2: Attack(opp, p, opp.attack2); break;
                case 3: Attack(opp, p, opp.attack3); break;
            }
        }

        previousKeyboard = kState;
    }
    
    private bool WasPressed(KeyboardState current, Keys key)
        => current.IsKeyDown(key) && previousKeyboard.IsKeyUp(key);

    public void Draw()
    {
        ShowStats();
        if (!string.IsNullOrEmpty(lastMsg)) Easywrite(lastMsg, new Vector2(pos.X + 1000, pos.Y + 700));
        if (battleOver)
        {
            return;
        }
        if (playersTurn)
        {
            Easywrite("It's your turn!", new Vector2(pos.X + 100, pos.Y + 700));
            Easywrite("Press 1: " + p.attack1.name + " dmg: " + DamageCalc(p, p.attack1), new Vector2(pos.X + 100, pos.Y + 800));
            Easywrite("Press 2: " + p.attack2.name + " dmg: " + DamageCalc(p, p.attack2), new Vector2(pos.X + 100, pos.Y + 900));
            Easywrite("Press 3: " + p.attack3.name + " dmg: " + DamageCalc(p, p.attack3), new Vector2(pos.X + 100, pos.Y + 1000));
        }
    }

    private void Easywrite(string s, Vector2 position)
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

    private void Attack(PokemonInstance attacker, PokemonInstance defender, Attack a)
    {
        
        defender.hp -= DamageCalc(attacker, defender, a);
        attacker.stamina = attacker.stamina - a.ap;

        SetMessage($"{attacker.getName()} uses {a.name}!"); 

        if (defender.hp <= 0)
        {
            defender.hp = 0;
            ShowStats();
            battleOver = true;
            dead = defender;
            winner = attacker;
            return;
        }

        playersTurn = !playersTurn;
    }
    
    private void SetMessage(string msg)
    {
        lastMsg = msg;
        if (!string.IsNullOrEmpty(additionalMsg)) lastMsg += "\n \n" + additionalMsg;
        messageTimer = 0;
        additionalMsg = "";
    }

    private int DamageCalc(PokemonInstance attacker, PokemonInstance defender, Attack a)
    {
        double damage = a.damage;
        
        if (a.special)
        {
            damage *= attacker.specialAttackFactor;
            damage /= defender.specialDefenseFactor;
        } else if (!a.special)
        {
            damage *= attacker.attackFactor;
            damage /= defender.defenseFactor;
        }
        double multiplier = 1.0;

        
        string attackType = a.type.ToString();
        string defenderType = defender.type.ToString();

        if (PokemonRegistry.getWeakness(defenderType).Contains(attackType))
        {
            multiplier = 2.0;
            additionalMsg = "Super effective!";
        }
        else if (PokemonRegistry.getResistant(defenderType).Contains(attackType))
        {
            multiplier = 0.5;
            additionalMsg = "Not very effective...";
        }
        else if (PokemonRegistry.getImmune(defenderType).Contains(attackType))
        {
            multiplier = 0.0;
            additionalMsg = $"It doesn't affect {defender.getName()}";
        }
        
        damage *= multiplier;
        
        additionalMsg += " (" + Math.Round(damage, MidpointRounding.AwayFromZero) + " dmg)";
        
        return (int) Math.Round(damage, MidpointRounding.AwayFromZero);
    }

    private int DamageCalc(PokemonInstance attacker, Attack a)
    {
        double damage = a.damage;
        
        if (a.special)
        {
            damage *= attacker.specialAttackFactor;
        } else if (!a.special)
        {
            damage *= attacker.attackFactor;
        }
        
        return (int) Math.Round(damage, MidpointRounding.AwayFromZero);
    }
}