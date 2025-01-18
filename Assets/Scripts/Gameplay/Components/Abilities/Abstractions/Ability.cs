using System;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Abilities.Casters;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class Ability : IDisposable
    {
        private CancellationTokenSource _castCancellationSource;
        private ICastStarter _castStarter;
        private IActor _owner;
        public KeyCode HotKey { get; private set; }
        public float Cooldown { get; private set; }
        public AbilityStatus Status { get; private set; }
        protected CancellationToken CastCancellationToken => _castCancellationSource.Token;
        public void SetHotkey(KeyCode key) => HotKey = key;
        public void SetCooldown(float cooldown) => Cooldown = cooldown;
        public void SetStatus(AbilityStatus status) => Status = status;

        public void SetCastStarter(ICastStarter castStarter)
        {
            _castCancellationSource = new CancellationTokenSource();
            _castStarter = castStarter;
            _castStarter.OnCastApplied += OnCastApplied;
        }

        public void Dispose()
        {
            if (_castStarter != null)
            {
                _castStarter.OnCastApplied -= OnCastApplied;
            }

            _castCancellationSource?.Cancel();
        }

        public void StartCast(IActor owner)
        {
            if (owner == null)
            {
                return;
            }

            _owner = owner;
            _castStarter?.StartCast();
        }

        protected abstract Task CastAsync(IActor owner);
        private void OnCastApplied() => CastAsync(_owner);
        protected void CancelCast() => _castCancellationSource?.Cancel();
    }
}