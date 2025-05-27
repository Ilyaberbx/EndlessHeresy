using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Data.Static.Items;
using EndlessHeresy.Runtime.Data.Static.StatusEffects;

namespace EndlessHeresy.Runtime.Services.Gameplay.StaticData
{
    public interface IGameplayStaticDataService
    {
        HeroConfiguration HeroConfiguration { get; }
        PunchingDummyConfiguration PunchingDummyConfiguration { get; }
        FloatingMessagesConfiguration FloatingMessagesConfiguration { get; }
        ItemConfiguration GetItemConfiguration(ItemType identifier);
        AttributeItemData GetAttributeData(AttributeType identifier);
        StatusEffectConfiguration GetStatusEffectConfiguration(StatusEffectType identifier);
        DamageColorData GetDamageColorData(DamageType identifier);
    }
}