using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using UnityEngine;

namespace EndlessHeresy.Core
{
    public abstract class MonoActor : MonoBehaviour, IActor
    {
        [SerializeField] protected MonoComponent[] MonoComponents;

        private IComponentsLocator _componentsLocator;
        private IEnumerable<IComponent> _components;
        private Transform _transform;
        private GameObject _gameObject;

        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;
        public bool ActiveSelf => GameObject.activeSelf;
        public CancellationToken DestroyCancellationToken => destroyCancellationToken;

        public async Task InitializeAsync(IComponentsLocator locator)
        {
            _componentsLocator = locator;
            CollectMonoComponents();
            await InitializeComponents();
            await OnInitializeAsync();
        }

        private async Task InitializeComponents()
        {
            _components = _componentsLocator.GetAllComponents();

            var components = _components as IComponent[] ?? _components.ToArray();

            foreach (var component in components)
            {
                component.SetActor(this);
            }

            var initializationTasks = components
                .Select(component => component.InitializeAsync())
                .ToList();

            var postInitializeTasks = components
                .Select(component => component.PostInitializeAsync())
                .ToList();

            if (initializationTasks.Any())
            {
                await Task.WhenAll(initializationTasks);
            }

            if (postInitializeTasks.Any())
            {
                await Task.WhenAll(postInitializeTasks);
            }
        }

        public void Dispose()
        {
            OnDispose();
            _componentsLocator = null;
        }

        public IEnumerable<IComponent> GetAllComponents()
        {
            return _componentsLocator.GetAllComponents();
        }

        public new bool TryGetComponent<TComponent>(out TComponent component) where TComponent : IComponent
        {
            return _componentsLocator.TryGetComponent(out component);
        }

        public bool TryAddComponent<TComponent>(TComponent component) where TComponent : IComponent
        {
            if (_componentsLocator.TryAddComponent(component))
            {
                component.SetActor(this);
                return true;
            }

            return false;
        }

        public bool TryRemoveComponent<TComponent>(TComponent component) where TComponent : IComponent
        {
            return _componentsLocator.TryRemoveComponent(component);
        }

        public new TComponent GetComponent<TComponent>() where TComponent : IComponent
        {
            return _componentsLocator.GetComponent<TComponent>();
        }

        protected virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual void OnDispose()
        {
            if (_components.IsNullOrEmpty())
            {
                return;
            }

            foreach (var component in _components)
            {
                component.Dispose();
            }
        }

        private void CollectMonoComponents()
        {
            _transform = transform;
            _gameObject = gameObject;

            foreach (var monoComponent in MonoComponents)
            {
                TryAddComponent(monoComponent);
            }
        }
    }
}