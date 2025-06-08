using EndlessHeresy.Runtime.Attributes;
using EndlessHeresy.Runtime.Data.Static.Components;

namespace EndlessHeresy.Runtime.Applicators
{
    public sealed class AttributeModifiersApplicator : IApplicator
    {
        private readonly AttributeModifierData[] _data;
        private AttributesComponent _attributes;

        public AttributeModifiersApplicator(AttributeModifierData[] data)
        {
            _data = data;
        }

        public void Apply(IActor actor)
        {
            _attributes = actor.GetComponent<AttributesComponent>();

            foreach (var modifierData in _data)
            {
                _attributes.Increase(modifierData.Identifier, modifierData.Value);
            }
        }

        public void Remove(IActor actor)
        {
            foreach (var modifierData in _data)
            {
                _attributes.Decrease(modifierData.Identifier, modifierData.Value);
            }

            _attributes = null;
        }
    }
}