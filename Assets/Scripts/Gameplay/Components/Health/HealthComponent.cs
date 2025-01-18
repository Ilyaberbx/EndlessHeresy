using System;
using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Health
{
    public sealed class HealthComponent : MonoComponent
    {
        public event Action OnHealthDepleted;

        private const float MinHealthPoints = 0;
        [SerializeField] private float _currentHp;

        private bool IsDead() => _currentHp <= 0;

        public void TakeDamage(float damage)
        {
            if (IsDead())
            {
                return;
            }

            _currentHp = Mathf.Clamp(_currentHp - damage, MinHealthPoints, _currentHp);

            if (IsDead())
            {
                OnHealthDepleted?.Invoke();
            }
        }
    }
}