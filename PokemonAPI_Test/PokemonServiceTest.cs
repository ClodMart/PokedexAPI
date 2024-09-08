using Microsoft.AspNetCore.Mvc;
using Moq;
using PokedexAPI.Classes.DTOs;
using PokedexAPI.Controllers;
using PokedexAPI.Services;
using PokedexAPI.Services.DataRepositories;
using PokedexAPI.Services.Interfaces;
using PokemonAPI_Test.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonAPI_Test
{
    public class PokemonServiceTest
    {
        [Theory]
        [InlineData("ditto")]
        [InlineData("kyogre")]
        public async Task GetPokemon_TranslatesBasedOnHabitatOrLegendary(string name)
        {
            //Arrange
            var mockPokemonData = new Mock<PokemonDataRepository>();
            var service = new PokemonService(mockPokemonData.Object);

            PokemonDto testpokemon = await new PokemonService(new PokemonDataRepositoryBehaviour()).GetPokemon(name, true);

            mockPokemonData.Setup(service => service.YodaTranslate(testpokemon.Description)).Returns(new PokemonDataRepositoryBehaviour().YodaTranslate(testpokemon.Description));
            mockPokemonData.Setup(service => service.ShakespeareTranslate(testpokemon.Description)).Returns(new PokemonDataRepositoryBehaviour().ShakespeareTranslate(testpokemon.Description));

            //Act
            PokemonDto pokemon = await service.GetPokemon(name, true);


            //Assert
            if (pokemon == null)
                Assert.False(true);

            if (pokemon.Is_legendary || pokemon.Habitat == "cave")
            {
                mockPokemonData.Verify(service => service.YodaTranslate(pokemon.Description), Times.Once);
            }
            else
            {
                mockPokemonData.Verify(service => service.ShakespeareTranslate(pokemon.Description), Times.Once);
            }
        }
    }
}
