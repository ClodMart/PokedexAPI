namespace PokedexAPI.Classes
{
    [Serializable]
    public class Pokemon
    {
        public int id { get; set; } // The identifier for this resource.
        public string name { get; set; } // The name for this resource.
        public int base_experience { get; set; } // The base experience gained for defeating this Pokémon.
        public int height { get; set; } // The height of this Pokémon in decimetres.
        public bool is_default { get; set; } // Set for exactly one Pokémon used as the default for each species.
        public int order { get; set; } // Order for sorting.
        public int weight { get; set; } // The weight of this Pokémon in hectograms.
        public pokemon_ability[]? abilities { get; set; } // A list of abilities this Pokémon could potentially have.
        public named_api_resource[]? forms { get; set; } // A list of forms this Pokémon can take on.
        public version_game_index[]? game_indices { get; set; } // A list of game indices relevant to Pokémon item by generation.
        public pokemon_held_item[]? held_items { get; set; } // A list of items this Pokémon may be holding when encountered.
        public string? location_area_encounters { get; set; } // A link to a list of location areas, as well as encounter details.
        public pokemon_move[]? moves { get; set; } // A list of moves along with learn methods and level details.
        public pokemon_type_past[]? past_types { get; set; } // A list of details showing types this Pokémon had in previous generations.
        public pokemon_sprites? sprites { get; set; } // A set of sprites used to depict this Pokémon in the game.
        public pokemon_cries? cries { get; set; } // A set of cries used to depict this Pokémon in the game.
        public named_api_resource? species { get; set; } // The species this Pokémon belongs to.
        public pokemon_stat[]? stats { get; set; } // A list of base stat values for this Pokémon.
        public pokemon_type[]? types { get; set; } // A list of details showing types this Pokémon has.
    }

    [Serializable]
    public class pokemon_ability
    {
        public bool is_hidden { get; set; } // Whether or not this is a hidden ability.
        public int slot { get; set; } // The slot this ability occupies in this Pokémon species.
        public named_api_resource? ability { get; set; } // The ability the Pokémon may have.
    }

    [Serializable]
    public class pokemon_type
    {
        public int slot { get; set; } // The order the Pokémon's types are listed in.
        public named_api_resource? type { get; set; } // The type the referenced Pokémon has.
    }

    [Serializable]
    public class pokemon_form
    {
        public int slot { get; set; } // The order the Pokémon's forms are listed in.
        public named_api_resource? type { get; set; } // The type the referenced Form has.
    }

    [Serializable]
    public class pokemon_type_past
    {
        public named_api_resource? generation { get; set; } // The last generation in which the referenced Pokémon had the listed types.
        public pokemon_type[]? types { get; set; } // The types the referenced Pokémon had up to and including the listed generation.
    }

    [Serializable]
    public class pokemon_held_item
    {
        public named_api_resource? item { get; set; } // The item the referenced Pokémon holds.
        public pokemon_held_item_version[]? version_details { get; set; } // The details of the different versions in which the item is held.
    }

    [Serializable]
    public class pokemon_held_item_version
    {
        public named_api_resource? version { get; set; } // The version in which the item is held.
        public int rarity { get; set; } // How often the item is held.
    }

    [Serializable]
    public class pokemon_move
    {
        public named_api_resource? move { get; set; } // The move the Pokémon can learn.
        public pokemon_move_version[]? version_group_details { get; set; } // The details of the version in which the Pokémon can learn the move.
    }

    [Serializable]
    public class pokemon_move_version
    {
        public named_api_resource? move_learn_method { get; set; } // The method by which the move is learned.
        public named_api_resource? version_group { get; set; } // The version group in which the move is learned.
        public int level_learned_at { get; set; } // The minimum level to learn the move.
    }

    [Serializable]
    public class pokemon_stat
    {
        public named_api_resource? stat { get; set; } // The stat the Pokémon has.
        public int effort { get; set; } // The effort points (EV) the Pokémon has in the stat.
        public int base_stat { get; set; } // The base value of the stat.
    }

    [Serializable]
    public class pokemon_sprites
    {
        public string? front_default { get; set; } // The default depiction of this Pokémon from the front in battle.
        public string? front_shiny { get; set; } // The shiny depiction of this Pokémon from the front in battle.
        public string? front_female { get; set; } // The female depiction of this Pokémon from the front in battle.
        public string? front_shiny_female { get; set; } // The shiny female depiction of this Pokémon from the front in battle.
        public string? back_default { get; set; } // The default depiction of this Pokémon from the back in battle.
        public string? back_shiny { get; set; } // The shiny depiction of this Pokémon from the back in battle.
        public string? back_female { get; set; } // The female depiction of this Pokémon from the back in battle.
        public string? back_shiny_female { get; set; } // The shiny female depiction of this Pokémon from the back in battle.
    }

    [Serializable]
    public class pokemon_cries
    {
        public string? latest { get; set; } // The latest depiction of this Pokémon's cry.
        public string? legacy { get; set; } // The legacy depiction of this Pokémon's cry.
    }

    [Serializable]
    public class named_api_resource
    {
        public string? name { get; set; } // The name of the resource.
        public string? url { get; set; } // The URL of the resource.
    }

    [Serializable]
    public class version_game_index
    {
        public int game_index { get; set; } // The index of the game.
        public named_api_resource? version { get; set; } // The version of the game.
    }
}