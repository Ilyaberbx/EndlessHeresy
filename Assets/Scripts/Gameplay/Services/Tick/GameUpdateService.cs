using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Tick
{
    public sealed class GameUpdateService : IGameUpdateService
    {
        public event Action<float> OnUpdate;
        public event Action<float> OnFixedUpdate;
        public void Tick() => OnUpdate?.Invoke(Time.deltaTime);
        public void FixedTick() => OnFixedUpdate?.Invoke(Time.fixedDeltaTime);
    }
}