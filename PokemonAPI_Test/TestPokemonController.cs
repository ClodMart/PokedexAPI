
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using PokedexAPI.Classes.DTOs;
using PokedexAPI.Controllers;
using PokedexAPI.Services.Interfaces;
using System.Net;

namespace PokemonAPI_Test
{
    public class TestPokemonController
    {
        [Fact]
        public async Task GetPokemonInfo_OnSuccess_InvokesPokemonServiceOnce()
        {
            //Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            mockPokemonService.Setup(service => service.GetPokemon("ditto", false));

            var controller = new PokemonController(mockPokemonService.Object);

            //Act
            var result = await controller.GetPokemonInfo("ditto");

            //Assert
            mockPokemonService.Verify(service => service.GetPokemon("ditto", false), Times.Once);
        }

        [Theory]
        [InlineData("ditto", HttpStatusCode.OK)]
        public async Task GetPokemonInfo_OnSuccess_Return_SC200(string pokemonName, HttpStatusCode expected)
        {
            //Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            var controller = new PokemonController(mockPokemonService.Object);

            //Act
            var result = (ActionResult<PokemonDto>)await controller.GetPokemonInfo(pokemonName);

            //Get StatusCode from IActionResult
            IConvertToActionResult convertToActionResult = result;
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;

            //Assert
            Assert.Equal((int)expected, actionResultWithStatusCode?.StatusCode);  
        }


        [Fact]
        public async Task GetPokemonTranslated_OnSuccess_InvokesPokemonServiceOnce()
        {
            //Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            mockPokemonService.Setup(service => service.GetPokemon("ditto", true));

            var controller = new PokemonController(mockPokemonService.Object);

            //Act
            var result = await controller.GetPokemonTranslated("ditto");

            //Assert
            mockPokemonService.Verify(service => service.GetPokemon("ditto", true), Times.Once);
        }

        [Theory]
        [InlineData("ditto", HttpStatusCode.OK)]
        public async Task GetPokemonTranslated_OnSuccess_Return_SC200(string pokemonName, HttpStatusCode expected)
        {
            //Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            var controller = new PokemonController(mockPokemonService.Object);

            //Act
            var result = (ActionResult<PokemonDto>)await controller.GetPokemonTranslated(pokemonName);

            //Get StatusCode from IActionResult
            IConvertToActionResult convertToActionResult = result;
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;

            //Assert
            Assert.Equal((int)expected, actionResultWithStatusCode?.StatusCode);
        }
    }
}