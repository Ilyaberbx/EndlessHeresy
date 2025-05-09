using System.Collections.Generic;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.StatusEffects.Implementations;
using VContainer;

namespace EndlessHeresy.Gameplay.StatusEffects.Builder
{
    public sealed class StatusEffectsBuilder
    {
        private readonly List<IStatusEffectComponent> _forComponents = new();


        public void WithComponent(IStatusEffectComponent component)
        {
            if (_forComponents.Contains(component))
            {
                return;
            }

            _forComponents.Add(component);
        }

        public IStatusEffectRoot Build(IActor owner, IObjectResolver resolver)
        {
            foreach (var component in _forComponents)
            {
                resolver.Inject(component);
            }

            var root = new StatusEffectRoot(_forComponents.ToArray());
            root.SetOwner(owner);
            resolver.Inject(root);
            return root;
        }
    }
}