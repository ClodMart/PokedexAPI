namespace PokedexAPI.Classes.DTOs
{
    [Serializable]
    public class PokemonDto(Pokemon pokemon, PokemonSpecies species)
    {
        public string Name { get; set; } = pokemon.name;
        public string? Description { get; set; } = species.flavor_text_entries.FirstOrDefault(x => x.language.name == "en")?.flavor_text;
        public string Habitat { get; set; } = species.habitat.name;
        public bool Is_legendary { get; set; } = species.is_legendary;
    }
}
