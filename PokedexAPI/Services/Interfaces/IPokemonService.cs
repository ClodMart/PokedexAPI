using PokedexAPI.Classes;
using PokedexAPI.Classes.DTOs;

namespace PokedexAPI.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<PokemonDto> GetPokemon(string name, bool translate = false);
    }
}
