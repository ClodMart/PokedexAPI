namespace PokedexAPI.Classes
{
    //Class used to load endpoint configuration from configs.json
    public class EndpointsOptions
    {
        public const string Endpoints = "Endpoints";

        public string PokemonApiBaseUri { get; set; }
        public string TranslateApiBaseUri { get; set; }
    }
}
