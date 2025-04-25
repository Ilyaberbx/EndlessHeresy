using EndlessHeresy.Gameplay.Stats;

namespace EndlessHeresy.Gameplay.StatusEffects.Implementations
{
    public sealed class StackDurationSynchronizer : StatusEffectDecorator
    {
        private readonly IStackNotifier _stackNotifier;
        private readonly TemporaryStatusEffect _temporaryStatusEffect;

        public StackDurationSynchronizer(IStackNotifier stackNotifier,
            TemporaryStatusEffect temporaryStatusEffect) : base(temporaryStatusEffect)
        {
            _stackNotifier = stackNotifier;
            _temporaryStatusEffect = temporaryStatusEffect;
        }

        public override void Apply(StatsComponent stats)
        {
            base.Apply(stats);

            _stackNotifier.OnStackAdded += OnStackAdded;
        }

        public override void Remove(StatsComponent stats)
        {
            base.Remove(stats);

            _stackNotifier.OnStackAdded -= OnStackAdded;
        }

        private void OnStackAdded(int stack) => _temporaryStatusEffect.Reset();
    }
}