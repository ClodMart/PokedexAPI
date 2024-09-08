using PokedexAPI.Classes;
using PokedexAPI.Classes.DTOs;
using PokedexAPI.Services.DataRepositories.Interfaces;
using PokedexAPI.Services.Interfaces;
using PokedexAPI.Services.Utils;

namespace PokedexAPI.Services
{
    public class PokemonService : IPokemonService
    {
        private IPokemonDataRepository _dataRepository;
        public PokemonService(IPokemonDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }


        //Ideally following SOLID principles this should be divided in two separate methods one to get pokemon data and one to translate description but
        //since the DTO is the same having only one method will simplify the controller
        public async Task<PokemonDto> GetPokemon(string name, bool translate = false)
        {
            Pokemon pkm = await _dataRepository.GetPokemonInfo(name);
            PokemonSpecies species = await _dataRepository.GetPokemonSecies(name);
            string desc = species.flavor_text_entries.FirstOrDefault(x => x.language.name == "en")?.flavor_text;
            if (translate && desc != null)
            {
                if (species.habitat.name == "cave" || species.is_legendary)
                {
                    string translated = await _dataRepository.YodaTranslate(species.flavor_text_entries.FirstOrDefault(x => x.language.name == "en")?.flavor_text ?? "");
                    desc = translated;
                }
                else
                {
                    string translated = await _dataRepository.ShakespeareTranslate(species.flavor_text_entries.FirstOrDefault(x => x.language.name == "en")?.flavor_text ?? "");
                    desc = translated;
                }
            }
            return new PokemonDto(pkm, species);
        }
    }
}
