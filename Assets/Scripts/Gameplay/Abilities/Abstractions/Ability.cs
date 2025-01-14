using System;
using EndlessHeresy.Gameplay.Abilities.Casters;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class Ability : IDisposable
    {
        private ICastStarter _castStarter;
        private GameObject _owner;
        public KeyCode HotKey { get; private set; }
        public float Cooldown { get; private set; }
        public AbilityStatus Status { get; private set; }

        public void SetCastStarter(ICastStarter castStarter)
        {
            _castStarter = castStarter;
            _castStarter.OnCastApplied += OnCastApplied;
        }

        public void SetHotkey(KeyCode key) => HotKey = key;
        public void SetCooldown(float cooldown) => Cooldown = cooldown;
        public void SetStatus(AbilityStatus status) => Status = status;

        public void Dispose()
        {
            if (_castStarter != null)
            {
                _castStarter.OnCastApplied -= OnCastApplied;
            }
        }

        public void StartCast(GameObject owner)
        {
            if (owner == null)
            {
                return;
            }

            _owner = owner;
            _castStarter?.StartCast();
        }

        protected abstract void Cast(GameObject owner);
        private void OnCastApplied() => Cast(_owner);
    }
}