using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Actors.Enemies;
using EndlessHeresy.Gameplay.Actors.Hero;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    public interface IGameplayFactoryService
    {
        public Task<HeroActor> CreateHeroAsync(Vector2 at);
        public Task<PunchingDummyActor> CreateDummyAsync(Vector2 at);
    }
}