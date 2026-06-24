using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;

namespace pokemon.Data;

// Hilfsklassen für types.json
public class TypeEntry
{
    public string type { get; set; }
    public List<string> effective { get; set; }
    public List<string> weakness { get; set; }
    public List<string> resistant { get; set; }
    public List<string> immune { get; set; }
}

// Hilfsklassen für pokemon.json
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

public class PokemonJson
{
    public List<PokemonEntry> pokemon { get; set; }
}

public class PokemonEntry
{
    public string name { get; set; }
    public string id { get; set; }
    public string type { get; set; }
    public StatsEntry stats { get; set; }
    public List<AttackEntry> attacks { get; set; }
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

public static class PokemonRegistry
{
    public static Dictionary<string, Pokemon> POKEMONS { get; } = new Dictionary<string, Pokemon>();
    private static PokemonJson pokemonData;
    private static List<TypeEntry> typeData;

    static PokemonRegistry()
    {
        string pokejson = File.ReadAllText("Content/data/pokemon.json");
        pokemonData = JsonSerializer.Deserialize<PokemonJson>(pokejson);

        string typesjson = File.ReadAllText("Content/data/types.json");
        typeData = JsonSerializer.Deserialize<List<TypeEntry>>(typesjson);

        foreach (PokemonEntry p in pokemonData.pokemon)
        {
            POKEMONS.Add(p.name, create(p.name));
        }
    }

    private static Pokemon create(string _name)
    {
        PokemonEntry entry = pokemonData.pokemon.FirstOrDefault(p => p.name == _name);
        if (entry == null)
            return null;

        TypeEntry typeEntry = typeData.FirstOrDefault(t => t.type == entry.type);
        if (typeEntry == null)
            return null;

        Attack attack1 = new Attack(
            entry.attacks[0].name,
            entry.attacks[0].stats.damage,
            entry.attacks[0].stats.ap,
            entry.attacks[0].stats.special
        );
        Attack attack2 = new Attack(
            entry.attacks[1].name,
            entry.attacks[1].stats.damage,
            entry.attacks[1].stats.ap,
            entry.attacks[1].stats.special
        );
        Attack attack3 = new Attack(
            entry.attacks[2].name,
            entry.attacks[2].stats.damage,
            entry.attacks[2].stats.ap,
            entry.attacks[2].stats.special
        );

        return new Pokemon(
            entry.name,
            Enum.Parse<Type>(entry.type),
            entry.id,
            entry.stats.hp,
            entry.stats.attack,
            entry.stats.defense,
            entry.stats.specialAttack,
            entry.stats.specialDefense,
            entry.stats.stamina,
            attack1,
            attack2,
            attack3
        );
    }

    public static List<string> getEffective(string type)
    {
        TypeEntry typeEntry = typeData.FirstOrDefault(t => t.type == type);
        if (typeEntry == null)
            return null;
        return typeEntry.effective;
    }

    public static List<string> getWeakness(string type)
    {
        TypeEntry typeEntry = typeData.FirstOrDefault(t => t.type == type);
        if (typeEntry == null)
            return null;
        return typeEntry.weakness;
    }

    public static List<string> getResistant(string type)
    {
        TypeEntry typeEntry = typeData.FirstOrDefault(t => t.type == type);
        if (typeEntry == null)
            return null;
        return typeEntry.resistant;
    }

    public static List<string> getImmune(string type)
    {
        TypeEntry typeEntry = typeData.FirstOrDefault(t => t.type == type);
        if (typeEntry == null)
            return null;
        return typeEntry.immune;
    }
}
