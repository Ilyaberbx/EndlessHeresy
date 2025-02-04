using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Actors.CrescentKnife;
using EndlessHeresy.Gameplay.Actors.Hero;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Factory
{
    public interface IGameFactoryService
    {
        public Task<HeroActor> CreateHeroAsync(Vector2 at);
    }
}