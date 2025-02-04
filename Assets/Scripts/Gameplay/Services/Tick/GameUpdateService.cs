using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Services.Tick
{
    public sealed class GameUpdateService : MonoService, IGameUpdateService
    {
        public event Action<float> OnUpdate;
        public event Action<float> OnFixedUpdate;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        private void Update() => OnUpdate?.Invoke(Time.deltaTime);
        private void FixedUpdate() => OnFixedUpdate?.Invoke(Time.deltaTime);
    }
}