using Microsoft.AspNetCore.Mvc;
using PokedexAPI.Classes;
using PokedexAPI.Services;
using PokedexAPI.Services.Interfaces;

namespace PokedexAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PokemonController(IPokemonService pokemonService) : ControllerBase
    {
        private readonly IPokemonService _pokemonService = pokemonService;

        [HttpGet]
        public async Task<ActionResult<Pokemon>> GetPokemonInfo(string name)
        {
            Pokemon result = await _pokemonService.GetPokemonInfo(name);
            return Ok(result);
        }
    }
}
