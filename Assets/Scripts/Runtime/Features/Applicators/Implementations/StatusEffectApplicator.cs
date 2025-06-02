using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.StatusEffects;

namespace EndlessHeresy.Runtime.Applicators
{
    public sealed class StatusEffectApplicator : IApplicator
    {
        private readonly StatusEffectType _identifier;

        public StatusEffectApplicator(StatusEffectType identifier)
        {
            _identifier = identifier;
        }

        public void Apply(IActor actor)
        {
            var statusEffectsComponent = actor.GetComponent<StatusEffectsComponent>();
            statusEffectsComponent.Add(_identifier);
        }

        public void Remove(IActor actor)
        {
            var statusEffectsComponent = actor.GetComponent<StatusEffectsComponent>();
            statusEffectsComponent.Remove(_identifier);
        }
    }
}