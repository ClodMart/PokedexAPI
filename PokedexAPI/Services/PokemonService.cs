using PokedexAPI.Classes;
using PokedexAPI.Classes.DTOs;
using PokedexAPI.Services.Interfaces;
using PokedexAPI.Services.Utils;

namespace PokedexAPI.Services
{
    public class PokemonService : IPokemonService
    {
        //In production would use a configuration file to inject service base Url
        private const string BaseUri = "https://pokeapi.co/api/v2/"; 
        private const string TranslateBaseUri = "https://api.funtranslations.com/translate/";

        public async Task<PokemonDto> GetPokemon( string name, bool translate = false)
        {
            Pokemon pkm = await GetPokemonInfo(name);
            //PokemonHabitat hab = await GetPokemonHabitat(name);
            PokemonSpecies species = await GetPokemonSecies(name);
            string desc = species.flavor_text_entries.FirstOrDefault(x => x.language.name == "en")?.flavor_text;
            if (translate && desc != null)
            {
                if (species.habitat.name == "cave" || species.is_legendary)
                {
                    string translated = await YodaTranslate(species.flavor_text_entries.FirstOrDefault(x => x.language.name == "en")?.flavor_text ?? "");
                    desc = translated;
                }
                else
                {
                   string translated = await ShakespeareTranslate(species.flavor_text_entries.FirstOrDefault(x => x.language.name == "en")?.flavor_text ?? "");
                   desc = translated;
                }
            }
            return new PokemonDto(pkm, species);
        }


        private async Task<Pokemon> GetPokemonInfo(string name)
        {
            string endpoint = "pokemon/" + name;

            Pokemon? Out = await RestApiWrapperService.Get<Pokemon>(BaseUri + endpoint);
            return Out == null ? throw new Exception("NullRef") : Out;
        }


        private async Task<PokemonSpecies> GetPokemonSecies(string name)
        {
            string endpoint = "pokemon-species/" + name;

            PokemonSpecies? Out = await RestApiWrapperService.Get<PokemonSpecies>(BaseUri + endpoint);
            return Out == null ? throw new Exception("NullRef") : Out;
        }


        private async Task<string> ShakespeareTranslate(string text)
        {
            string endpoint = "shakespeare.json";
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "text", Uri.EscapeDataString(text) }
            };
            string Out;
            TranslateResult Translate = await RestApiWrapperService.PostRequest<TranslateResult>(TranslateBaseUri + endpoint, dic);
            if (Translate.success.total > 0)
            {
                Out = Translate.contents.translated;
            }
            else
            {
                Out = Translate.contents.text;
            }
            return Out;
        }

        private async Task<string> YodaTranslate(string text)
        {
            string endpoint = "yoda.json";
            Dictionary<string, string>  dic = new Dictionary<string, string>
            {
                { "text",  Uri.EscapeDataString(text) }
            };
            string Out;
            TranslateResult Translate = await RestApiWrapperService.PostRequest<TranslateResult>(TranslateBaseUri + endpoint, dic);
            if (Translate.success.total > 0) 
            {
                Out = Translate.contents.translated;
            }
            else 
            {
                Out = Translate.contents.text;
            }
            return Out;
        }
    }
}
