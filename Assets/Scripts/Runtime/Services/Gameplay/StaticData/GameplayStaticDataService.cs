using System.Linq;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Data.Static.StatusEffects;
using EndlessHeresy.Runtime.Services.AssetsManagement;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Services.Gameplay.StaticData
{
    public sealed class GameplayStaticDataService : IGameplayStaticDataService, IInitializable
    {
        private readonly IAssetsService _assetsService;
        private HeroConfiguration _heroConfiguration;
        private PunchingDummyConfiguration _punchingDummyConfiguration;
        private FloatingMessagesConfiguration _floatingMessagesConfiguration;
        private StatusEffectConfiguration[] _statusEffectsConfiguration;
        private AttributesConfiguration _attributesConfiguration;
        private DamageColorsConfiguration _damageColorsConfiguration;
        public HeroConfiguration HeroConfiguration => _heroConfiguration;
        public PunchingDummyConfiguration PunchingDummyConfiguration => _punchingDummyConfiguration;
        public FloatingMessagesConfiguration FloatingMessagesConfiguration => _floatingMessagesConfiguration;

        public GameplayStaticDataService(IAssetsService assetsService)
        {
            _assetsService = assetsService;
        }

        public void Initialize()
        {
            Task.WhenAll(
                    LoadHeroConfigurationAsync(),
                    LoadDummyConfigurationAsync(),
                    LoadMessagesConfigurationAsync(),
                    LoadStatusEffectsConfigurationAsync(),
                    LoadAttributesConfigurationAsync(),
                    LoadDamageColorsConfigurationAsync(),
                    LoadAttributesConfigurationAsync())
                .Forget();
        }

        public AttributeItemData GetAttributeData(AttributeType identifier)
        {
            return _attributesConfiguration.Data.FirstOrDefault(temp => temp.Identifier == identifier);
        }

        public StatusEffectConfiguration GetStatusEffectConfiguration(StatusEffectType identifier)
        {
            return _statusEffectsConfiguration.FirstOrDefault(temp => temp.Identifier == identifier);
        }

        public DamageColorData GetDamageColorData(DamageType identifier)
        {
            return _damageColorsConfiguration.Data.FirstOrDefault(temp => temp.Identifier == identifier);
        }

        private async Task LoadAttributesConfigurationAsync()
        {
            _attributesConfiguration =
                await _assetsService.Load<AttributesConfiguration>(GameplayStaticDataKeys.Attributes);
        }

        private async Task LoadDamageColorsConfigurationAsync()
        {
            _damageColorsConfiguration =
                await _assetsService.Load<DamageColorsConfiguration>(GameplayStaticDataKeys.DamageColors);
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

        public async Task LoadStatusEffectsConfigurationAsync()
        {
            _statusEffectsConfiguration =
                await _assetsService.LoadAll<StatusEffectConfiguration>(GameplayStaticDataKeys.StatusEffects);
        }
    }
}