using System.Linq;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static;
using EndlessHeresy.Gameplay.Data.Static.Items;
using EndlessHeresy.Gameplay.Data.Static.StatusEffects;
using EndlessHeresy.Global.Services.AssetsManagement;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public sealed class GameplayStaticDataService : IGameplayStaticDataService, IInitializable
    {
        private readonly IAssetsService _assetsService;
        private HeroConfiguration _heroConfiguration;
        private PunchingDummyConfiguration _punchingDummyConfiguration;
        private FloatingMessagesConfiguration _floatingMessagesConfiguration;
        private ItemConfiguration[] _itemsConfigurations;
        private StatusEffectConfiguration[] _statusEffectsConfiguration;
        public HeroConfiguration HeroConfiguration => _heroConfiguration;
        public PunchingDummyConfiguration PunchingDummyConfiguration => _punchingDummyConfiguration;
        public FloatingMessagesConfiguration FloatingMessagesConfiguration => _floatingMessagesConfiguration;

        public GameplayStaticDataService(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }

        public void Initialize()
        {
            var initializationTask = Task.WhenAll(LoadHeroConfigurationAsync(),
                LoadDummyConfigurationAsync(),
                LoadMessagesConfigurationAsync(),
                LoadItemsConfigurationAsync(),
                LoadStatusEffectConfigurations());

            initializationTask.Forget();
        }

        public ItemConfiguration GetItemConfiguration(ItemType identifier)
        {
            return _itemsConfigurations.FirstOrDefault(temp => temp.Identifier == identifier);
        }

        public StatusEffectConfiguration GetStatusEffectConfiguration(StatusEffectType identifier)
        {
            return _statusEffectsConfiguration.FirstOrDefault(temp => temp.Identifier == identifier);
        }

        private async Task LoadItemsConfigurationAsync()
        {
            _itemsConfigurations = await _assetsService.LoadAll<ItemConfiguration>(GameplayStaticDataKeys.Items);
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

        public async Task LoadStatusEffectConfigurations()
        {
            _statusEffectsConfiguration =
                await _assetsService.LoadAll<StatusEffectConfiguration>(GameplayStaticDataKeys.StatusEffects);
        }
    }
}