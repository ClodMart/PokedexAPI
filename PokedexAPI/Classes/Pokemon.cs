// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using PokedexAPI.Classes;

public class EffectChange
{
    public VersionGroup version_group { get; set; }
    public List<EffectEntry> effect_entries { get; set; }
}

public class EffectEntry
{
    public string effect { get; set; }
    public string short_effect { get; set; }
    public Language language { get; set; }
}

public class Generation
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Language
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Name
{
    public string name { get; set; }
    public Language language { get; set; }
}

public class PokemonDetails
{
    public bool is_hidden { get; set; }
    public int slot { get; set; }
    public Pokemon pokemon { get; set; }
    public string name { get; set; }
    public string url { get; set; }
}

public class Pokemon
{
    public int id { get; set; }
    public string name { get; set; }
    public bool is_main_series { get; set; }
    public Generation generation { get; set; }
    public List<Name> names { get; set; }
    public List<EffectEntry> effect_entries { get; set; }
    public List<EffectChange> effect_changes { get; set; }
    public List<FlavorTextEntry> flavor_text_entries { get; set; }
    public List<PokemonDetails> pokemon { get; set; }
}

public class VersionGroup
{
    public string name { get; set; }
    public string url { get; set; }
}

