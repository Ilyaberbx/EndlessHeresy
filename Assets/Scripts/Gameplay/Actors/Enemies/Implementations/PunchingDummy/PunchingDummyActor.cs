using EndlessHeresy.Gameplay.Health;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Enemies
{
    public sealed class PunchingDummyActor : EnemyActor
    {
        private HealthComponent _healthComponent;

        private void Start()
        {
            _healthComponent = GetComponent<HealthComponent>();
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