using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using PokedexAPI.Classes;
using PokedexAPI.Services.DataRepositories.Interfaces;
using PokedexAPI.Services.Utils;
using System.Configuration;

namespace PokedexAPI.Services.DataRepositories
{
    public class PokemonDataRepository : IPokemonDataRepository
    {
        private readonly IOptions<ExternalUris> _externalUris;

        private string? BaseUri = "https://pokeapi.co/api/v2/";
        private string? TranslateBaseUri = "https://api.funtranslations.com/translate/";

        public PokemonDataRepository()
        {

        }
        //On creation I inject IOptions from appsettings.json
        public PokemonDataRepository(IOptions<ExternalUris> options)
        {
            BaseUri = options.Value.BasePokemonUri;
            TranslateBaseUri = options.Value.TranslateBaseUri;
        }

        public async Task<Pokemon> GetPokemonInfo(string name)
        {
            string endpoint = "pokemon/" + name;

            Pokemon? Out = await RestApiWrapperService.Get<Pokemon>(BaseUri + endpoint);
            return Out == null ? throw new Exception("NullRef") : Out;
        }


        public async Task<PokemonSpecies> GetPokemonSecies(string name)
        {
            string endpoint = "pokemon-species/" + name;

            PokemonSpecies? Out = await RestApiWrapperService.Get<PokemonSpecies>(BaseUri + endpoint);
            return Out == null ? throw new Exception("NullRef") : Out;
        }


        public virtual async Task<string> ShakespeareTranslate(string text)
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

        public virtual async Task<string> YodaTranslate(string text)
        {
            string endpoint = "yoda.json";
            Dictionary<string, string> dic = new Dictionary<string, string>
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
