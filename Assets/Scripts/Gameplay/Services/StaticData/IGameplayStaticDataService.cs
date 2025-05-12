using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Data.Static.Items;
using EndlessHeresy.Gameplay.Data.Static.StatusEffects;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public interface IGameplayStaticDataService
    {
        HeroConfiguration HeroConfiguration { get; }
        PunchingDummyConfiguration PunchingDummyConfiguration { get; }
        FloatingMessagesConfiguration FloatingMessagesConfiguration { get; }
        ItemConfiguration GetItemConfiguration(ItemType identifier);
        AttributeConfigurationData GetAttributeData(AttributeType identifier);
        StatusEffectConfiguration GetStatusEffectConfiguration(StatusEffectType identifier);
        DamageColorData GetDamageColorData(DamageType identifier);
    }
}