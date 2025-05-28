using System;
using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.StatusEffects.Implementations;
using VContainer;

namespace EndlessHeresy.Runtime.StatusEffects.Builder
{
    public sealed class StatusEffectsBuilder
    {
        private readonly Dictionary<Type, object[]> _componentsParamsMap = new();

        private StatusEffectType _identifier;
        private StatusEffectClassType _classIdentifier;

        public void WithId(StatusEffectType identifier)
        {
            _identifier = identifier;
        }

        public void WithClass(StatusEffectClassType classIdentifier)
        {
            _classIdentifier = classIdentifier;
        }

        public void WithComponent<TComponent>(params object[] parameters) where TComponent : IStatusEffectComponent
        {
            _componentsParamsMap.Add(typeof(TComponent), parameters);
        }

        public IStatusEffectRoot Build(IObjectResolver resolver)
        {
            var forComponents = new List<IStatusEffectComponent>();

            foreach (var componentParamsPair in _componentsParamsMap)
            {
                var type = componentParamsPair.Key;
                var parameters = componentParamsPair.Value;
                var component = (IStatusEffectComponent)resolver.Instantiate(type, Lifetime.Singleton, parameters);
                forComponents.Add(component);
            }

            return resolver.Instantiate<StatusEffectRoot>(Lifetime.Singleton, forComponents.ToArray(), _identifier,
                _classIdentifier);
        }
    }
}