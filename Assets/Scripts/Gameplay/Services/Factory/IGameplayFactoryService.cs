using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Actors;
using EndlessHeresy.Gameplay.Actors.Enemies;
using EndlessHeresy.Gameplay.Actors.Hero;
using EndlessHeresy.Gameplay.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    public interface IGameplayFactoryService
    {
        public Task<HeroActor> CreateHeroAsync(Vector2 at);
        public Task<PunchingDummyActor> CreateDummyAsync(Vector2 at);
        public Task<ItemPickUpActor> CreateItemPickUpAsync(ItemType itemType);
        void Dispose(IActor actor);
    }
}