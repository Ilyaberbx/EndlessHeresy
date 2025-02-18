using System;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay.Services.Tick
{
    public interface IGameUpdateService : ITickable, IFixedTickable
    {
        event Action<float> OnUpdate;
        event Action<float> OnFixedUpdate;
    }
}