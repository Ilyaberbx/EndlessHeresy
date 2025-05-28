using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using EndlessHeresy.Runtime.Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace EndlessHeresy.Runtime.Builder
{
    public sealed class MonoActorBuilder<TActor> where TActor : MonoActor
    {
        private const string NoPrefabProvidedMessage = "No prefab provided";

        private readonly IObjectResolver _resolver;
        private readonly IComponentsLocator _forComponents;

        private Vector2 _at;
        private TActor _prefab;
        private Transform _parent;
        private TActor _actor;
        private Quaternion _rotation;

        private TActor Actor
        {
            get
            {
                if (_actor == null)
                {
                    var actorObject = CreateActorObject();
                    _actor = actorObject.GetComponent<TActor>();
                }

                return _actor;
            }
        }

        public MonoActorBuilder(IObjectResolver resolver)
        {
            _resolver = resolver;
            _forComponents = new ComponentsLocator();
        }

        public MonoActorBuilder<TActor> ForPrefab(TActor prefab)
        {
            _prefab = prefab;
            return this;
        }

        public MonoActorBuilder<TActor> WithPosition(Vector2 position)
        {
            _at = position;
            return this;
        }

        public MonoActorBuilder<TActor> WithRotation(Quaternion rotation)
        {
            _rotation = rotation;
            return this;
        }

        public MonoActorBuilder<TActor> WithParent(Transform parent)
        {
            _parent = parent;
            return this;
        }

        public MonoActorBuilder<TActor> WithComponent<TComponent>(params object[] parameters)
            where TComponent : IComponent
        {
            _forComponents.TryAddComponent(_resolver.Instantiate<TComponent>(Lifetime.Scoped, parameters));
            return this;
        }

        public MonoActorBuilder<TActor> WithComponent<TComponent>(TComponent component)
            where TComponent : IComponent
        {
            _resolver.Inject(component);
            _forComponents.TryAddComponent(component);
            return this;
        }

        public async Task<TActor> Build()
        {
            await Actor.InitializeAsync(_forComponents);
            return Actor;
        }

        private Component CreateActorObject()
        {
            if (_prefab == null)
            {
                DebugUtility.LogException<NullReferenceException>(NoPrefabProvidedMessage);
            }

            var actor = Object.Instantiate(_prefab, _at, _rotation, _parent);
            _resolver.InjectGameObject(actor.gameObject);
            return actor;
        }
    }
}