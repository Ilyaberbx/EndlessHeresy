using System;

namespace EndlessHeresy.Gameplay.Services.Tick
{
    public interface IGameUpdateService
    {
        event Action<float> OnUpdate;
        event Action<float> OnFixedUpdate;
    }
}