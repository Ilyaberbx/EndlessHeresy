using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.Commands.Stats
{
    public sealed class RemoveStatModifierCommand : IActorCommand
    {
        private readonly IStatModifierSource _source;
        private IActor _actor;

        public RemoveStatModifierCommand(IStatModifierSource source)
        {
            _source = source;
        }

        public void Execute()
        {
            if (_actor == null)
            {
                return;
            }

            if (!_actor.TryGetComponent<StatsComponent>(out var stats))
            {
                return;
            }

            stats.RemoveAllModifiersBySource(_source);
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
        }
    }
}