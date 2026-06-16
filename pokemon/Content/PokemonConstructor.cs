using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

// Hilfsklassen für types.json
public class TypeEntry
{
    public string type { get; set; }
    public List<string> effective { get; set; }
    public List<string> weakness { get; set; }
    public List<string> resistant { get; set; }
    public List<string> immune { get; set; }
    public List<AttackEntry> attacks { get; set; }
}

public class AttackEntry
{
    public string name { get; set; }
    public AttackStats stats { get; set; }
}

public class AttackStats
{
    public int damage { get; set; }
    public int ap { get; set; }
    public bool special { get; set; }
}

// Hilfsklassen für pokemon.json
public class PokemonJson
{
    public List<PokemonEntry> pokemon { get; set; }
}

public class PokemonEntry
{
    public string name { get; set; }
    public int id { get; set; }
    public string type { get; set; }
    public StatsEntry stats { get; set; }
}

public class StatsEntry
{
    public int hp { get; set; }
    public double attack { get; set; }
    public double defense { get; set; }
    [JsonPropertyName("special-attack")]
    public double specialAttack { get; set; }
    [JsonPropertyName("special-defense")]
    public double specialDefense { get; set; }
    public int stamina { get; set; }
}

public class PokemonConstructor
{
    private PokemonJson pokemonData;
    private List<TypeEntry> typeData;

    public PokemonConstructor()
    {
        string pokejson = File.ReadAllText("Content/data/pokemon.json");
        pokemonData = JsonSerializer.Deserialize<PokemonJson>(pokejson);

        string typesjson = File.ReadAllText("Content/data/types.json");
        typeData = JsonSerializer.Deserialize<List<TypeEntry>>(typesjson);
    }

    public Pokemon create(int _id)
    {
        PokemonEntry entry = pokemonData.pokemon.FirstOrDefault(p => p.id == _id);
        if (entry == null) return null;

        TypeEntry typeEntry = typeData.FirstOrDefault(t => t.type == entry.type);
        if (typeEntry == null) return null;

        Attack attack1 = new Attack(typeEntry.attacks[0].name, typeEntry.attacks[0].stats.damage, typeEntry.attacks[0].stats.ap, typeEntry.attacks[0].stats.special);
        Attack attack2 = new Attack(typeEntry.attacks[1].name, typeEntry.attacks[1].stats.damage, typeEntry.attacks[1].stats.ap, typeEntry.attacks[1].stats.special);
        Attack attack3 = new Attack(typeEntry.attacks[2].name, typeEntry.attacks[2].stats.damage, typeEntry.attacks[2].stats.ap, typeEntry.attacks[2].stats.special);

        return new Pokemon(
            entry.name,
            entry.id,
            entry.type,
            entry.stats.hp,
            entry.stats.hp,
            entry.stats.attack,
            entry.stats.defense,
            entry.stats.specialAttack,
            entry.stats.specialDefense,
            entry.stats.stamina,
            entry.stats.stamina,
            attack1,
            attack2,
            attack3
        );
    }
}