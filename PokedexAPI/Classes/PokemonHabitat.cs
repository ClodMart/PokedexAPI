
namespace PokedexAPI.Classes
{
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

    public class PokemonSpec
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class PokemonHabitat
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Name> names { get; set; }
        public List<PokemonSpec> pokemon_species { get; set; }
    }


}
