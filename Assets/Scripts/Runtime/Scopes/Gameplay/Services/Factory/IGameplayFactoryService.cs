using System.Threading.Tasks;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Actors.Enemies;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Scopes.Gameplay.Services.Factory
{
    public interface IGameplayFactoryService
    {
        public Task<HeroActor> CreateHeroAsync(Vector2 at);
        public Task<PunchingDummyActor> CreateDummyAsync(Vector2 at);
        public Task<ItemPickUpActor> CreateItemPickUpAsync(ItemType itemType);
        void Dispose(IActor actor);
    }
}