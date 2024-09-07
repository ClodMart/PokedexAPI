using Microsoft.AspNetCore.Mvc;
using PokedexAPI.Classes;
using PokedexAPI.Classes.DTOs;
using PokedexAPI.Services;
using PokedexAPI.Services.Interfaces;

namespace PokedexAPI.Controllers
{

    [ApiController]
    [Route("pokemon")]
    public class PokemonController(IPokemonService pokemonService) : ControllerBase
    {
        private readonly IPokemonService _pokemonService = pokemonService;

        [HttpGet]
        public async Task<ActionResult<PokemonDto>> GetPokemonInfo(string name)
        {
            PokemonDto result = await _pokemonService.GetPokemon(name);
            return Ok(result);
        }

        [HttpGet]
        [Route("translated")]
        public async Task<ActionResult<PokemonDto>> GetPokemonTranslated(string name)
        {
            PokemonDto result = await _pokemonService.GetPokemon(name, true);
            return Ok(result);
        }
    }
}
