using EndlessHeresy.Gameplay.Data.Static;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public interface IGameplayStaticDataService
    {
        HeroConfiguration HeroConfiguration { get; }
        PunchingDummyConfiguration PunchingDummyConfiguration { get; }
    }
}