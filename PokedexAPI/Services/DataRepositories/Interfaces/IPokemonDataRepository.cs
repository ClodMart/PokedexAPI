using PokedexAPI.Classes;

namespace PokedexAPI.Services.DataRepositories.Interfaces
{
    public interface IPokemonDataRepository
    {
        Task<Pokemon> GetPokemonInfo(string name);
        Task<PokemonSpecies> GetPokemonSecies(string name);
        Task<string> ShakespeareTranslate(string text);
        Task<string> YodaTranslate(string text);
    }
}
