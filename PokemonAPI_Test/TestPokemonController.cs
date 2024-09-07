
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using PokedexAPI.Controllers;
using PokedexAPI.Services.Interfaces;
using System.Net;

namespace PokemonAPI_Test
{
    public class TestPokemonController
    {
        [Theory]
        [InlineData("ditto", HttpStatusCode.OK)]
        public async Task GetPokemonInfo_OnSuccess_Return_SC200(string pokemonName, HttpStatusCode expected)
        {
            //Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            var controller = new PokemonController(mockPokemonService.Object);

            //Act
            var result = (ActionResult<Pokemon>)await controller.GetPokemonInfo(pokemonName);

            //Get StatusCode from IActionResult
            IConvertToActionResult convertToActionResult = result;
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;

            //Assert
            Assert.Equal((int)expected, actionResultWithStatusCode?.StatusCode);  
        }
    }
}