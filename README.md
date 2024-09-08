# PokedexAPI
## Project structure
The program is a .NET api based on .NET 8 you can use Visual studio or other ide to open the solution PokedexAPI.sln or you can use the command line.
The solution contains a WebApi PokedexAPI and a xUnit project to test it; the test project is done using moq library to mock services in test cases.

## Changes for production level application
1) I would setup a shared library between the two projects containing the folder "PokemonAPI_Test\DataRepositoriesMock\" and "PokedexAPI\Services\DataRepositories\" in order to keep the data repositories and mock in a single place.
2) A better global error handling class to catch and manage known errors.


