# Pokémon Crystal–Style MonoGame Class Diagram

> Based on the MonoGame *Building 2D Games* tutorial series.
> Classes from the tutorial are marked **[Tutorial]**. Classes you'll need to add for a Pokémon-style RPG are marked **[RPG]**.

---

## Project Structure

```
PokemonCrystal/          ← Game project
GameLib/                 ← Reusable class library (Ch. 04)
  Core/
  Graphics/
  Input/
  Audio/
  Scenes/
  UI/
  RPG/                   ← Your Pokémon-specific systems
```

---

## Core (MonoGame Foundation)

```
Microsoft.Xna.Framework.Game     ← MonoGame base class
        ▲
        │ inherits
        │
    Game1                        [Tutorial] Ch. 03
    ─────────────────────────────────────────────
    - _spriteBatch : SpriteBatch
    - _sceneManager : SceneManager
    + Initialize() : void
    + LoadContent() : void
    + Update(GameTime) : void
    + Draw(GameTime) : void
```

---

## Graphics

```
    Sprite                       [Tutorial] Ch. 08
    ─────────────────────────────────────────────
    - Texture : Texture2D
    - SourceRectangle : Rectangle?
    + Position : Vector2
    + Rotation : float
    + Scale : Vector2
    + Color : Color
    + LayerDepth : float
    + Draw(SpriteBatch) : void

        ▲
        │ inherits
        │
    AnimatedSprite               [Tutorial] Ch. 09
    ─────────────────────────────────────────────
    - _animations : Dictionary<string, Animation>
    - _currentAnimation : Animation
    + Play(name: string) : void
    + Update(GameTime) : void
    + Draw(SpriteBatch) : void

    Animation                    [Tutorial] Ch. 09
    ─────────────────────────────────────────────
    - Frames : Rectangle[]
    - FrameDuration : float
    - IsLooping : bool
    - CurrentFrame : int
    + Update(GameTime) : void

    TextureAtlas                 [Tutorial] Ch. 07
    ─────────────────────────────────────────────
    - _texture : Texture2D
    + GetRegion(name: string) : Rectangle
    + CreateSprite(name: string) : Sprite
```

---

## Input

```
    KeyboardInfo                 [Tutorial] Ch. 11
    ─────────────────────────────────────────────
    - _previousState : KeyboardState
    - _currentState : KeyboardState
    + IsKeyDown(Keys) : bool
    + WasKeyJustPressed(Keys) : bool
    + WasKeyJustReleased(Keys) : bool
    + Update() : void

    GamePadInfo                  [Tutorial] Ch. 11
    ─────────────────────────────────────────────
    - _previousState : GamePadState
    - _currentState  : GamePadState
    + IsButtonDown(Buttons) : bool
    + WasButtonJustPressed(Buttons) : bool
    + Update() : void

    InputManager  (static)       [Tutorial] Ch. 11
    ─────────────────────────────────────────────
    + Keyboard : KeyboardInfo
    + GamePad : GamePadInfo
    + Update() : void
```

---

## Audio

```
    AudioController  (static)    [Tutorial] Ch. 15
    ─────────────────────────────────────────────
    - _soundEffects : Dictionary<string, SoundEffect>
    - _currentMusic : SoundEffectInstance
    + SFXVolume : float
    + MusicVolume : float
    + PlaySFX(name: string) : void
    + PlayMusic(name: string, loop: bool) : void
    + StopMusic() : void
```

---

## Scene Management

```
    «interface»
    IScene                       [Tutorial] Ch. 17
    ─────────────────────────────────────────────
    + Initialize() : void
    + LoadContent() : void
    + Update(GameTime) : void
    + Draw(SpriteBatch) : void
    + UnloadContent() : void

        ▲
        │ implements
        ├──────────────────────────────────────
        │                │                    │
    TitleScene       WorldScene          BattleScene    [RPG]
    (menus/title)    (overworld)         (turn-based)

    SceneManager                 [Tutorial] Ch. 17
    ─────────────────────────────────────────────
    - _currentScene : IScene
    + ChangeScene(scene: IScene) : void
    + Update(GameTime) : void
    + Draw(SpriteBatch) : void
```

---

## Tilemap & World (Pokémon Overworld)

```
    Tile                         [Tutorial] Ch. 13 + [RPG]
    ─────────────────────────────────────────────
    + TileId : int
    + IsWalkable : bool
    + TriggerType : TileEventType    ← Grass, Warp, Sign, etc.

    Tilemap                      [Tutorial] Ch. 13 + [RPG]
    ─────────────────────────────────────────────
    - _tiles : Tile[,]
    - _tileset : TextureAtlas
    + Columns : int
    + Rows : int
    + TileSize : int
    + GetTile(x, y) : Tile
    + Draw(SpriteBatch) : void

    MapWarp                      [RPG]
    ─────────────────────────────────────────────
    + SourceTile : Point
    + DestinationMap : string
    + DestinationTile : Point

    WorldMap                     [RPG]
    ─────────────────────────────────────────────
    - _tilemap : Tilemap
    - _warps : List<MapWarp>
    - _npcs : List<NpcEntity>
    + Name : string
    + LoadFromFile(path: string) : void
    + GetWarpAt(x, y) : MapWarp?
```

---

## Entities (Overworld)

```
    «abstract»
    Entity                       [RPG]
    ─────────────────────────────────────────────
    + Position : Vector2
    + Direction : Direction           ← Up/Down/Left/Right
    + Sprite : AnimatedSprite
    + Update(GameTime) : void
    + Draw(SpriteBatch) : void

        ▲
        │ inherits
        ├────────────────────────
        │                       │
    PlayerEntity           NpcEntity          [RPG]
    ─────────────────────  ─────────────────────────────
    - _speed : float       + DialogueKey : string
    + HandleInput() : void + Interact() : void
    + CheckCollision       + WalkRoute : Point[]
      (Tilemap) : bool
```

---

## RPG Data – Pokémon & Moves

```
    MoveData                     [RPG]  (loaded from JSON/XML)
    ─────────────────────────────────────────────
    + Id : int
    + Name : string
    + Type : PokemonType
    + Power : int
    + Accuracy : int
    + PP : int
    + Category : MoveCategory     ← Physical / Special / Status
    + Effect : StatusEffect?

    PokemonSpeciesData           [RPG]  (loaded from JSON/XML)
    ─────────────────────────────────────────────
    + SpeciesId : int
    + Name : string
    + Type1 : PokemonType
    + Type2 : PokemonType?
    + BaseStats : StatBlock
    + LearnSet : List<(Level, MoveData)>
    + CatchRate : int
    + ExpYield : int

    StatBlock                    [RPG]
    ─────────────────────────────────────────────
    + HP : int
    + Attack : int
    + Defense : int
    + SpAttack : int
    + SpDefense : int
    + Speed : int

    PokemonInstance              [RPG]  (a living Pokémon in the player's party)
    ─────────────────────────────────────────────
    - _species : PokemonSpeciesData
    + Nickname : string
    + Level : int
    + CurrentHP : int
    + MaxHP : int
    + Stats : StatBlock           ← computed from base + IVs/EVs
    + IVs : StatBlock
    + EVs : StatBlock
    + Moves : MoveInstance[4]
    + Status : StatusCondition    ← Burn, Sleep, Paralyzed, etc.
    + Experience : int
    + GainExp(amount: int) : void
    + LevelUp() : void

    MoveInstance                 [RPG]
    ─────────────────────────────────────────────
    + Data : MoveData
    + CurrentPP : int
```

---

## Battle System

```
    BattleScene                  [RPG]  implements IScene
    ─────────────────────────────────────────────
    - _playerParty : List<PokemonInstance>
    - _enemyParty  : List<PokemonInstance>
    - _activePokemon : (PokemonInstance, PokemonInstance)
    - _state : BattleState
    - _ui : BattleUI
    + StartBattle(enemy: Trainer?) : void
    + Update(GameTime) : void
    + Draw(SpriteBatch) : void

    BattleEngine                 [RPG]
    ─────────────────────────────────────────────
    + CalculateDamage(attacker, defender, move) : int
    + GetTypeEffectiveness(move, target) : float
    + TryCapture(pokemon, ball) : bool
    + RunTurn(playerAction, enemyAction) : IEnumerable<BattleEvent>

    «enum»
    BattleState                  [RPG]
    ─────────────────────────────────────────────
    SelectingAction
    SelectingMove
    SelectingTarget
    AnimatingMove
    EnemyTurn
    BattleOver

    BattleUI                     [RPG]
    ─────────────────────────────────────────────
    - _font : SpriteFont
    + DrawHUD(SpriteBatch, pokemon, side) : void
    + DrawMoveMenu(SpriteBatch, moves) : void
    + DrawDialogue(SpriteBatch, text) : void
    + DrawExpBar(SpriteBatch, pokemon) : void
```

---

## Player Progression

```
    Trainer                      [RPG]
    ─────────────────────────────────────────────
    + Name : string
    + Party : List<PokemonInstance>   (max 6)
    + PC : List<PokemonInstance>
    + Inventory : Inventory
    + Badges : bool[8]
    + Money : int
    + SeenPokédex  : HashSet<int>
    + CaughtPokédex : HashSet<int>

    Inventory                    [RPG]
    ─────────────────────────────────────────────
    - _items : Dictionary<ItemData, int>
    + AddItem(item, count) : void
    + UseItem(item, target) : bool
    + GetCount(item) : int

    ItemData                     [RPG]
    ─────────────────────────────────────────────
    + Id : int
    + Name : string
    + Description : string
    + Category : ItemCategory     ← Potion, Ball, KeyItem, TM, etc.
    + Effect : ItemEffect
```

---

## Dialogue & Events

```
    DialogueBox                  [RPG]  (built on SpriteFont Ch. 16 + Gum Ch. 20)
    ─────────────────────────────────────────────
    - _font : SpriteFont
    - _lines : string[]
    - _charIndex : int            ← typewriter effect
    + Show(text: string) : void
    + Update(GameTime) : void
    + Draw(SpriteBatch) : void
    + IsFinished : bool

    EventScript                  [RPG]
    ─────────────────────────────────────────────
    + Steps : List<IEventStep>
    + Execute(WorldScene) : IEnumerator

    «interface»
    IEventStep                   [RPG]
    ─────────────────────────────────────────────
    + Execute(context) : IEnumerator

    Implementations:
      DialogueStep  |  WarpStep  |  GivePokémonStep
      BattleStep    |  SetFlagStep  |  PlayMusicStep
```

---

## Save System

```
    SaveData                     [RPG]
    ─────────────────────────────────────────────
    + Player : Trainer
    + CurrentMap : string
    + PlayerPosition : Point
    + Flags : Dictionary<string, bool>   ← story flags
    + PlayTimeSeconds : double

    SaveManager  (static)        [RPG]
    ─────────────────────────────────────────────
    + Save(slot: int, data: SaveData) : void
    + Load(slot: int) : SaveData?
    + Delete(slot: int) : void
```

---

## Relationships Summary

```
Game1 ──uses──► SceneManager
SceneManager ──manages──► IScene
                              ├── TitleScene
                              ├── WorldScene ──has──► WorldMap
                              │                          └──► Tilemap
                              │               ──has──► PlayerEntity
                              │               ──has──► List<NpcEntity>
                              └── BattleScene ──has──► BattleEngine
                                              ──has──► BattleUI
                                              ──has──► Trainer (player)

Trainer ──has──► List<PokemonInstance>
                     └──► PokemonSpeciesData
                     └──► MoveInstance[]
                               └──► MoveData

PlayerEntity & NpcEntity ──use──► AnimatedSprite ──extends──► Sprite
InputManager ──feeds──► PlayerEntity
AudioController ──used by──► BattleScene, WorldScene
SaveManager ──serializes──► SaveData ──contains──► Trainer
```

---

## Tutorial Chapter Reference

| Chapter | What You Build | Used By |
|---------|---------------|---------|
| 03 | `Game1` skeleton | Everything |
| 04 | Class library project | All shared classes |
| 07 | `TextureAtlas` | Tilemap, Sprites |
| 08 | `Sprite` | All visible entities |
| 09 | `AnimatedSprite`, `Animation` | Player, NPCs, Pokémon sprites |
| 11 | `InputManager`, `KeyboardInfo`, `GamePadInfo` | `PlayerEntity` |
| 12 | Collision detection helpers | `PlayerEntity` ↔ `Tilemap` |
| 13 | `Tilemap`, `Tile` | `WorldMap`, `WorldScene` |
| 14–15 | `AudioController` | Music/SFX throughout |
| 16 | `SpriteFont` | `DialogueBox`, `BattleUI` |
| 17 | `SceneManager`, `IScene` | All scenes |
| 20–21 | Gum UI | Menus, `BattleUI` |