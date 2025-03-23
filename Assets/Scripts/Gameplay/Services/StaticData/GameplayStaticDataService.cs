using System;
using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static;
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
        private FloatingMessagesConfiguration _floatingMessagesConfiguration;
        public HeroConfiguration HeroConfiguration => _heroConfiguration;
        public PunchingDummyConfiguration PunchingDummyConfiguration => _punchingDummyConfiguration;
        public FloatingMessagesConfiguration FloatingMessagesConfiguration => _floatingMessagesConfiguration;

        public ItemConfiguration GetItemConfiguration(ItemType itemType)
        {
            throw new NotImplementedException();
        }

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
                await LoadMessagesConfigurationAsync();
            }
            catch (Exception e)
            {
                DebugUtility.LogException(e);
            }
        }

        private async Task LoadMessagesConfigurationAsync()
        {
            _floatingMessagesConfiguration =
                await _assetsService.Load<FloatingMessagesConfiguration>(GameplayStaticDataKeys.FloatingMessages);
        }

        private async Task LoadDummyConfigurationAsync()
        {
            _punchingDummyConfiguration =
                await _assetsService.Load<PunchingDummyConfiguration>(GameplayStaticDataKeys.PunchingDummy);
        }

        private async Task LoadHeroConfigurationAsync()
        {
            _heroConfiguration = await _assetsService.Load<HeroConfiguration>(GameplayStaticDataKeys.Hero);
        }
    }
}