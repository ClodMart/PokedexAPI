using PokedexAPI.Classes;

namespace PokedexAPI.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<Pokemon> GetPokemonInfo(string name);
    }
}
