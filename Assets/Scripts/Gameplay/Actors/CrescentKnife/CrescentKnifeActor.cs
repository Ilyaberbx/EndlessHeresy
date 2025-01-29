using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Tags;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.CrescentKnife
{
    public sealed class CrescentKnifeActor : MonoActor
    {
        public event Action<HealthComponent> OnHit;

        private EnemyTriggerObserver _enemyTriggerObserver;
        private readonly IList<HealthComponent> _attachedHealthComponents = new List<HealthComponent>();

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            if (TryGetComponent(out _enemyTriggerObserver))
            {
                _enemyTriggerObserver.OnTriggerEnter += OnEnemyTriggerEntered;
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            Clear();

            if (_enemyTriggerObserver == null)
            {
                return;
            }

            _enemyTriggerObserver.OnTriggerEnter -= OnEnemyTriggerEntered;
        }

        private void OnEnemyTriggerEntered(EnemyTagComponent enemyTagComponent)
        {
            if (!enemyTagComponent.TryGetComponent(out HealthComponent healthComponent))
            {
                return;
            }

            if (_attachedHealthComponents.Contains(healthComponent))
            {
                return;
            }

            _attachedHealthComponents.Add(healthComponent);
            OnHit?.Invoke(healthComponent);
        }


        public void Show() => GameObject.SetActive(true);
        public void SetParent(Transform parent) => GameObject.transform.SetParent(parent);

        public void Hide()
        {
            GameObject.SetActive(false);
            Clear();
        }

        private void Clear() => _attachedHealthComponents.Clear();
    }
}