
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using PokedexAPI.Classes.DTOs;
using PokedexAPI.Controllers;
using PokedexAPI.Services.Interfaces;
using PokemonAPI_Test.Services;
using System.Net;

namespace PokemonAPI_Test
{
    public class TestPokemonController
    {
        [Theory]
        [InlineData("ditto")]
        [InlineData("kyogre")]
        public async Task GetPokemonInfo_OnSuccess_InvokesPokemonServiceOnce(string name)
        {
            //Arrange
            var mockPokemonService = new Mock<IPokemonService>();

            var controller = new PokemonController(mockPokemonService.Object);
            //mockPokemonService.Setup(service => service.GetPokemon(name, false));

            //Act
            await controller.GetPokemonInfo(name);
      
            //Assert
            mockPokemonService.Verify(service => service.GetPokemon(name, false), Times.Once);
        }

        [Theory]
        [InlineData("ditto", HttpStatusCode.OK)]
        [InlineData("kyogre", HttpStatusCode.OK)]
        public async Task GetPokemonInfo_OnSuccess_Return_SC200(string pokemonName, HttpStatusCode expected)
        {
            //Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            var controller = new PokemonController(mockPokemonService.Object);

            //Act
            var result = await controller.GetPokemonInfo(pokemonName);

            //Get StatusCode from IActionResult
            IConvertToActionResult convertToActionResult = result;
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;

            //Assert
            Assert.Equal((int)expected, actionResultWithStatusCode?.StatusCode);  
        }


        [Theory]
        [InlineData("ditto")]
        [InlineData("kyogre")]
        public async Task GetPokemonTranslated_OnSuccess_InvokesPokemonServiceOnceAndTranslatesBasedOnHabitatOrLegendary(string name)
        {
            //Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            var controller = new PokemonController(mockPokemonService.Object);

            //Act
            var result = await controller.GetPokemonTranslated(name);
            //Assert
            mockPokemonService.Verify(service => service.GetPokemon(name, true), Times.Once);
        }

        [Theory]
        [InlineData("ditto", HttpStatusCode.OK)]
        [InlineData("kyogre", HttpStatusCode.OK)]
        public async Task GetPokemonTranslated_OnSuccess_Return_SC200(string pokemonName, HttpStatusCode expected)
        {
            //Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            var controller = new PokemonController(mockPokemonService.Object);

            //Act
            var result = await controller.GetPokemonTranslated(pokemonName);

            //Get StatusCode from IActionResult
            IConvertToActionResult convertToActionResult = result;
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;

            //Assert
            Assert.Equal((int)expected, actionResultWithStatusCode?.StatusCode);
        }
    }
}