using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using pokemon.Data;
using pokemon.Entity;

namespace pokemon.Scenes;

public class Battle : Scene
{
    private SpriteFont _font;
    private SpriteFont _fontBold;
    private Random _random = new Random();

    private PokemonInstance _playerPokemon;
    private PokemonInstance _enemyokemon;

    private PokemonVisual _playerVisual;
    private PokemonVisual _enemyVisual;

    private bool _playersTurn;
    private bool _battleOver = false;
    public string lastMsg { get; set; }
    public string additionalMsg { get; set; }
    private double messageTimer = 0;
    private const double MESSAGE_DURATION = 2.0;

    public Battle(PokemonInstance yourPokemon, PokemonInstance opponentsPokemon, bool playersTurn)
    {
        _playerPokemon = yourPokemon;
        _enemyokemon = opponentsPokemon;
        _playersTurn = playersTurn;
    }

    public override void Initialize()
    {
        base.Initialize();

        Core.ExitOnEscape = true;
    }

    public override void LoadContent()
    {
        TextureAtlas atlas = TextureAtlas.FromFile(Core.Content, "images/atlas-definition.xml");

        _playerVisual = new PokemonVisual(atlas, new Vector2(32, 72));
        _enemyVisual = new PokemonVisual(atlas, new Vector2(192, 72));

        _font = Content.Load<SpriteFont>("fonts/6x8");
        _fontBold = Content.Load<SpriteFont>("fonts/8x8");
    }

    public override void Update(GameTime gameTime)
    {
        messageTimer += gameTime.ElapsedGameTime.TotalSeconds;

        _playerVisual.Update(gameTime);
        _enemyVisual.Update(gameTime);

        if (_battleOver && messageTimer >= MESSAGE_DURATION * 2)
        {
            Core.ChangeScene(new GameScene());
        }
        if (!_battleOver)
        {
            if (_playersTurn)
            {
                if (Core.Input.Keyboard.WasKeyJustPressed(Keys.D1))
                {
                    Attack(_playerPokemon, _enemyokemon, _playerPokemon._basePokemon.attack1);
                    _enemyVisual.TakeDamageVisual();
                }
                else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.D2))
                {
                    Attack(_playerPokemon, _enemyokemon, _playerPokemon._basePokemon.attack2);
                    _enemyVisual.TakeDamageVisual();
                }
                else if (Core.Input.Keyboard.WasKeyJustPressed(Keys.D3))
                {
                    Attack(_playerPokemon, _enemyokemon, _playerPokemon._basePokemon.attack3);
                    _enemyVisual.TakeDamageVisual();
                }
            }
            else if (!_playersTurn && messageTimer >= MESSAGE_DURATION)
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        Attack(_enemyokemon, _playerPokemon, _enemyokemon._basePokemon.attack1);
                        _playerVisual.TakeDamageVisual();
                        break;
                    case 2:
                        Attack(_enemyokemon, _playerPokemon, _enemyokemon._basePokemon.attack2);
                        _playerVisual.TakeDamageVisual();
                        break;
                    case 3:
                        Attack(_enemyokemon, _playerPokemon, _enemyokemon._basePokemon.attack3);
                        _playerVisual.TakeDamageVisual();
                        break;
                }
            }
        }
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.Black);

        Core.SpriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: Core.ScaleMatrix
        );

        _playerVisual.Draw(Core.SpriteBatch);
        _enemyVisual.Draw(Core.SpriteBatch);

        Write(_playerPokemon.getName(), 16, 16, bold: true);
        Write(_playerPokemon.hp + "/" + _playerPokemon._basePokemon.maxHP, 16, 24);
        Write(_enemyokemon.getName(), 192, 16, bold: true);
        Write(_enemyokemon.hp + "/" + _enemyokemon._basePokemon.maxHP, 192, 24);

        if (!string.IsNullOrEmpty(lastMsg))
            Write(lastMsg, 112, 128);

        if (_playersTurn)
        {
            Write("It's your turn!", 16, 108, bold: true, scale: 0.5f);
            Write(
                "Press 1: "
                    + _playerPokemon._basePokemon.attack1.name
                    + " dmg: "
                    + DamageCalc(_playerPokemon, _playerPokemon._basePokemon.attack1),
                16,
                117,
                scale: 0.5f
            );
            Write(
                "Press 2: "
                    + _playerPokemon._basePokemon.attack2.name
                    + " dmg: "
                    + DamageCalc(_playerPokemon, _playerPokemon._basePokemon.attack2),
                16,
                126,
                scale: 0.5f
            );
            Write(
                "Press 3: "
                    + _playerPokemon._basePokemon.attack3.name
                    + " dmg: "
                    + DamageCalc(_playerPokemon, _playerPokemon._basePokemon.attack3),
                16,
                135,
                scale: 0.5f
            );
        }

        Core.SpriteBatch.End();
    }

    private int DamageCalc(PokemonInstance attacker, PokemonInstance defender, Attack a)
    {
        double damage = a.damage;

        if (a.special)
        {
            damage *= attacker._basePokemon.specialAttackMult;
            damage /= defender._basePokemon.specialDefenseMult;
        }
        else if (!a.special)
        {
            damage *= attacker._basePokemon.attackMult;
            damage /= defender._basePokemon.defenseMult;
        }
        double multiplier = 1.0;

        string attackType = a.type.ToString();
        string defenderType = defender._basePokemon.type.ToString();

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

        return (int)Math.Round(damage, MidpointRounding.AwayFromZero);
    }

    private int DamageCalc(PokemonInstance attacker, Attack a)
    {
        double damage = a.damage;

        if (a.special)
        {
            damage *= attacker._basePokemon.specialAttackMult;
        }
        else if (!a.special)
        {
            damage *= attacker._basePokemon.attackMult;
        }

        return (int)Math.Round(damage, MidpointRounding.AwayFromZero);
    }

    private void Attack(PokemonInstance attacker, PokemonInstance defender, Attack a)
    {
        defender.hp -= DamageCalc(attacker, defender, a);
        attacker.stamina = attacker.stamina - a.ap;

        SetMessage($"{attacker.getName()} uses {a.name}!");

        if (defender.hp <= 0)
        {
            defender.hp = 0;
            EndBattle(attacker, defender);
        }

        _playersTurn = !_playersTurn;
    }

    private void EndBattle(PokemonInstance winner, PokemonInstance loser)
    {
        lastMsg = $"{loser.getName()} is dead! {winner.getName()} wins!";
        _battleOver = true;
    }

    private void SetMessage(string msg)
    {
        lastMsg = msg;
        if (!string.IsNullOrEmpty(additionalMsg))
            lastMsg += "\n \n" + additionalMsg;
        messageTimer = 0;
        additionalMsg = "";
    }

    private void Write(string s, int x, int y, bool bold = false, float scale = 1f)
    {
        if (!bold)
        {
            Core.SpriteBatch.DrawString(
                _font,
                s,
                new Microsoft.Xna.Framework.Vector2(x, y),
                Color.White,
                0f,
                Microsoft.Xna.Framework.Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );
        }
        else
        {
            Core.SpriteBatch.DrawString(
                _fontBold,
                s,
                new Microsoft.Xna.Framework.Vector2(x, y),
                Color.White,
                0f,
                Microsoft.Xna.Framework.Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
