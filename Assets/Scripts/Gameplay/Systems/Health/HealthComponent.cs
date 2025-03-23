using System;
using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Health
{
    public sealed class HealthComponent : PocoComponent
    {
        public event Action OnHealthDepleted;
        public event Action<float> OnTakeDamage;

        private const float MinHealthPoints = 0;
        public float CurrentHp { get; private set; }
        public void SetHealth(float healthPoints) => CurrentHp = healthPoints;

        public void TakeDamage(float damage)
        {
            if (IsDead())
            {
                return;
            }

            CurrentHp = Mathf.Clamp(CurrentHp - damage, MinHealthPoints, CurrentHp);
            OnTakeDamage?.Invoke(damage);

            if (IsDead())
            {
                OnHealthDepleted?.Invoke();
            }
        }

        public bool IsDead() => CurrentHp <= 0;
    }
}