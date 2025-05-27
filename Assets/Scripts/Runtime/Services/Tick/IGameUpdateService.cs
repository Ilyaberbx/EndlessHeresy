using System;
using VContainer.Unity;

namespace EndlessHeresy.Runtime.Services.Tick
{
    public interface IGameUpdateService : ITickable, IFixedTickable
    {
        event Action<float> OnUpdate;
        event Action<float> OnFixedUpdate;
    }
}