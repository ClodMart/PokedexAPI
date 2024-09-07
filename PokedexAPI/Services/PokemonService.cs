using PokedexAPI.Classes;
using PokedexAPI.Services.Interfaces;
using PokedexAPI.Services.Utils;

namespace PokedexAPI.Services
{
    public class PokemonService : IPokemonService
    {
        private const string BaseUri = "https://pokeapi.co/api/v2/"; //Probably in production would use a configuration file to inject service base Url

        public async Task<Pokemon> GetPokemonInfo(string name)
        {
            string endpoint = "pokemon/"+name;

            Pokemon? Out = await RestApiWrapperService.Get<Pokemon>(BaseUri+endpoint);
            if(Out == null)
            {
                throw new Exception("NullRef");
            }
            return Out;
        }
    }
}
