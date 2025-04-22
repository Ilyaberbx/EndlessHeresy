using System;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class StackDurationSynchronizer : StatusEffectDecorator, IDisposable, IInitializable
    {
        private readonly IStackNotifier _stackNotifier;
        private readonly TemporaryStatusEffect _temporaryStatusEffect;

        public StackDurationSynchronizer(IStackNotifier stackNotifier,
            TemporaryStatusEffect temporaryStatusEffect) : base(temporaryStatusEffect)
        {
            _stackNotifier = stackNotifier;
            _temporaryStatusEffect = temporaryStatusEffect;
        }

        public void Initialize() => _stackNotifier.OnStackAdded += OnStackAdded;
        public void Dispose() => _stackNotifier.OnStackAdded -= OnStackAdded;
        private void OnStackAdded(int stack) => _temporaryStatusEffect.Reset();
    }
}