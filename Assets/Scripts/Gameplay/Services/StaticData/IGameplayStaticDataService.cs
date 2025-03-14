using EndlessHeresy.Gameplay.Actors.Enemies;
using EndlessHeresy.Gameplay.Actors.Hero;

namespace EndlessHeresy.Gameplay.Services.StaticData
{
    public interface IGameplayStaticDataService
    {
        HeroConfiguration HeroConfiguration { get; }
        PunchingDummyConfiguration PunchingDummyConfiguration { get; }
    }
}