using System.Collections.Generic;
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
        private List<Task> _postInitializationTasks;
        private Transform _transform;
        private GameObject _gameObject;

        public GameObject GameObject => _gameObject;
        public Transform Transform => _transform;
        public bool ActiveSelf => GameObject.activeSelf;

        private void Awake()
        {
            if (_componentsLocator != null)
            {
                return;
            }

            _componentsLocator = new ComponentsLocator();
            InitializeMonoComponents();
        }

        public async Task InitializeAsync(IComponentsLocator locator)
        {
            _componentsLocator = locator;
            InitializeMonoComponents();
            _components = _componentsLocator.GetAllComponents();

            foreach (var component in _components)
            {
                component.SetActor(this);
            }

            await OnInitializeAsync();
            await OnPostInitializeAsync();
        }

        public void Dispose()
        {
            OnDispose();
            _componentsLocator = null;
        }

        public IEnumerable<IComponent> GetAllComponents() => _componentsLocator.GetAllComponents();

        public new bool TryGetComponent<TComponent>(out TComponent component) where TComponent : IComponent =>
            _componentsLocator.TryGetComponent(out component);

        public bool TryAddComponent<TComponent>(TComponent component) where TComponent : IComponent
        {
            if (_componentsLocator.TryAddComponent(component))
            {
                component.SetActor(this);
                return true;
            }

            return false;
        }

        public bool TryRemoveComponent<TComponent>(TComponent component) where TComponent : IComponent =>
            _componentsLocator.TryRemoveComponent(component);

        protected virtual async Task OnPostInitializeAsync()
        {
            _postInitializationTasks = new List<Task>();

            foreach (var component in _components)
            {
                var postInitializationTask = component.InitializeAsync();
                _postInitializationTasks.Add(postInitializationTask);
            }

            if (_postInitializationTasks.IsNullOrEmpty())
            {
                return;
            }

            await Task.WhenAll(_postInitializationTasks);
        }

        protected virtual async Task OnInitializeAsync()
        {
            _postInitializationTasks = new List<Task>();

            foreach (var component in _components)
            {
                var initializationTask = component.InitializeAsync();
                _postInitializationTasks.Add(initializationTask);
            }

            if (_postInitializationTasks.IsNullOrEmpty())
            {
                return;
            }

            await Task.WhenAll(_postInitializationTasks);
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

        private void InitializeMonoComponents()
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