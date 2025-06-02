using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Extensions;
using VContainer;

namespace EndlessHeresy.Runtime.StatusEffects.Builder
{
    public sealed class StatusEffectsBuilder
    {
        private readonly Dictionary<Type, object[]> _componentsParamsMap;
        private readonly IList<IStatusEffectComponent> _forComponents;
        private readonly IScopedObjectResolver _childScope;

        private StatusEffectType _identifier;
        private StatusEffectClassType _classIdentifier;
        public IObjectResolver Resolver => _childScope;

        public StatusEffectsBuilder(IObjectResolver resolver)
        {
            _childScope = resolver.CreateScope();
            _componentsParamsMap = new Dictionary<Type, object[]>();
            _forComponents = new List<IStatusEffectComponent>();
        }

        public void WithId(StatusEffectType identifier)
        {
            _identifier = identifier;
        }

        public void WithClass(StatusEffectClassType classIdentifier)
        {
            _classIdentifier = classIdentifier;
        }

        public void WithComponent<TComponent>(TComponent component) where TComponent : IStatusEffectComponent
        {
            _forComponents.Add(component);
        }

        public void WithComponent<TComponent>(params object[] parameters) where TComponent : IStatusEffectComponent
        {
            _componentsParamsMap.Add(typeof(TComponent), parameters);
        }


        public StatusEffectRoot Build()
        {
            foreach (var component in from componentParamsPair in _componentsParamsMap
                     let type = componentParamsPair.Key
                     let parameters = componentParamsPair.Value
                     select (IStatusEffectComponent)_childScope.Instantiate(type, Lifetime.Singleton, parameters))
            {
                _forComponents.Add(component);
            }

            return _childScope.Instantiate<StatusEffectRoot>(Lifetime.Singleton, _forComponents.ToArray(), _identifier,
                _classIdentifier);
        }
    }
}