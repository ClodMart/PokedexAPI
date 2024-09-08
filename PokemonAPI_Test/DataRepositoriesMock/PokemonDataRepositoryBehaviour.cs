using PokedexAPI.Classes;
using PokedexAPI.Classes.DTOs;
using PokedexAPI.Services.DataRepositories.Interfaces;
using PokedexAPI.Services.Interfaces;
using PokedexAPI.Services.Utils;

namespace PokemonAPI_Test.Services
{
    public class PokemonDataRepositoryBehaviour : IPokemonDataRepository
    {

        public async Task<Pokemon> GetPokemonInfo(string name)
        {
            return new Pokemon
            {
                name = name
            };
        }


        public async Task<PokemonSpecies> GetPokemonSecies(string name)
        {
            switch (name)
            {
                case "ditto":
                return new PokemonSpecies()
                {
                    is_legendary = false,
                    habitat = new()
                    {
                        name = "cave"
                    },
                    flavor_text_entries = [
                                   new() {
                    language = new PokedexAPI.Classes.Language()
                        {
                            name="en"
                        },
                    flavor_text = "test_flavour_text"}
                               ],
                };
                case "kyogre":
                    return new PokemonSpecies()
                    {
                        is_legendary = true,
                        habitat = new()
                        {
                            name = "cave"
                        },
                        flavor_text_entries = [
                                       new() {
                    language = new PokedexAPI.Classes.Language()
                        {
                            name="en"
                        },
                    flavor_text = "test_flavour_text"}
                                   ],
                    };
                default:
                    return new PokemonSpecies()
                    {
                        is_legendary = false,
                        habitat = new()
                        {
                            name = "grass"
                        },
                        flavor_text_entries = [
                   new() {
                    language = new PokedexAPI.Classes.Language()
                        {
                            name="en"
                        },
                    flavor_text = "test_flavour_text"}
               ],
                    };
            }
        }


        public virtual async Task<string> ShakespeareTranslate(string text)
        {
            return text + "shakspeare";
        }

        public virtual async Task<string> YodaTranslate(string text)
        {
            return text + "yoda";
        }
    }
}
