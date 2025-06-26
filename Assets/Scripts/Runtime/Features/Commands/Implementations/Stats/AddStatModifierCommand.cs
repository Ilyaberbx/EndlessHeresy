using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Stats.Modifiers;

namespace EndlessHeresy.Runtime.Commands.Stats
{
    public sealed class AddStatModifierCommand : IActorCommand, IUndoableCommand
    {
        private readonly StatModifierData _data;
        private readonly IStatModifierSource _source;
        private IActor _actor;

        public AddStatModifierCommand(StatModifierData data, IStatModifierSource source)
        {
            _data = data;
            _source = source;
        }

        public void Setup(IActor actor)
        {
            _actor = actor;
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

            stats.GetStat(_data.StatIdentifier)
                .AddModifier(_data.GetStatModifier(_source));
        }

        public ICommand GetUndoCommand()
        {
            return new RemoveStatModifierCommand(_source);
        }
    }
}