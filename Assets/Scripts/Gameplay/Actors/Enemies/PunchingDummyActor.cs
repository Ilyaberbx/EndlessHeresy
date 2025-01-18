using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Health;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Enemies
{
    public sealed class PunchingDummyActor : MonoActor
    {
        private HealthComponent _healthComponent;

        private void Start()
        {
            TryGetComponent(out _healthComponent);
            _healthComponent.OnHealthDepleted += OnHealthDepleted;
        }

        private void OnDestroy()
        {
            _healthComponent.OnHealthDepleted -= OnHealthDepleted;
        }

        private void OnHealthDepleted()
        {
            Debug.Log("Die");
            Destroy(gameObject);
        }
    }
}