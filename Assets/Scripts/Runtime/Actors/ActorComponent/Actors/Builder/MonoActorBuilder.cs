using System;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace EndlessHeresy.Runtime.Actors.Builder
{
    public sealed class MonoActorBuilder<TActor> where TActor : MonoActor
    {
        private const string NoPrefabProvidedMessage = "No prefab provided";

        private readonly IObjectResolver _container;
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

        public MonoActorBuilder(IComponentsLocator forComponents, IObjectResolver container)
        {
            _forComponents = forComponents;
            _container = container;
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

        public MonoActorBuilder<TActor> WithComponent<TComponent>(TComponent component) where TComponent : IComponent
        {
            _container.Inject(component);
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
            _container.InjectGameObject(actor.gameObject);
            return actor;
        }
    }
}