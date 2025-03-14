using System;
using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Actors.Enemies;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Global.Services.AssetsManagement;
using VContainer.Unity;
using DebugUtility = Better.Commons.Runtime.Utility.DebugUtility;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public sealed class GameplayStaticDataService : IGameplayStaticDataService, IInitializable
    {
        private readonly IAssetsService _assetsService;
        private HeroConfiguration _heroConfiguration;
        private PunchingDummyConfiguration _punchingDummyConfiguration;
        public HeroConfiguration HeroConfiguration => _heroConfiguration;
        public PunchingDummyConfiguration PunchingDummyConfiguration => _punchingDummyConfiguration;

        public GameplayStaticDataService(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }

        public async void Initialize()
        {
            try
            {
                await LoadHeroConfigurationAsync();
                await LoadDummyConfigurationAsync();
            }
            catch (Exception e)
            {
                DebugUtility.LogException(e);
            }
        }

        private async Task LoadDummyConfigurationAsync()
        {
            _punchingDummyConfiguration = await _assetsService.Load<PunchingDummyConfiguration>(GameplayStaticDataKeys.PunchingDummy);
        }

        private async Task LoadHeroConfigurationAsync()
        {
            _heroConfiguration = await _assetsService.Load<HeroConfiguration>(GameplayStaticDataKeys.Hero);
        }
    }
}