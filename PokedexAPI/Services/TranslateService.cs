using PokedexAPI.Services.Interfaces;
using PokedexAPI.Services.Utils;

namespace PokedexAPI.Services
{
    public class TranslateService : ITranslateService
    {
        private const string BaseUri = "https://api.funtranslations.com/translate/"; //Probably in production would use a configuration file to inject service base Url
        private readonly ApiService apiService = new(BaseUri);
    }
}
