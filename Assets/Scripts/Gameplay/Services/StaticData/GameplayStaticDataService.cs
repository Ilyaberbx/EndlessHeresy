using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Global.Services.AssetsManagement;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public sealed class GameplayStaticDataService : IGameplayStaticDataService
    {
        private readonly IAssetsService _assetsService;
        private HeroConfiguration _heroConfiguration;
        public HeroConfiguration HeroConfiguration => _heroConfiguration;

        public GameplayStaticDataService(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }

        public async Task LoadHeroConfigurationAsync()
        {
            _heroConfiguration = await _assetsService.Load<HeroConfiguration>(GameplayStaticDataKeys.Hero);
        }
    }
}