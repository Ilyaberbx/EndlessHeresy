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
        protected IActor Owner { get; private set; }
        public KeyCode HotKey { get; private set; }
        public float Cooldown { get; private set; }
        public AbilityStatus Status { get; private set; }
        protected CancellationToken CastCancellationToken => _castCancellationSource.Token;

        public void Configure(KeyCode key, float cooldown)
        {
            HotKey = key;
            Cooldown = cooldown;
        }

        public virtual void Initialize(IActor owner)
        {
            Owner = owner;
        }

        public virtual void Dispose()
        {
            if (_castStarter != null)
            {
                _castStarter.OnCastApplied -= OnCastApplied;
            }

            _castCancellationSource?.Cancel();
        }

        public void SetCastStarter(ICastStarter castStarter)
        {
            _castCancellationSource = new CancellationTokenSource();
            _castStarter = castStarter;
            _castStarter.OnCastApplied += OnCastApplied;
        }

        public void SetStatus(AbilityStatus status) => Status = status;
        public void StartCast() => _castStarter?.StartCast();
        protected abstract Task CastAsync(IActor owner);

        private void OnCastApplied()
        {
            if (_castStarter == null)
            {
                return;
            }

            CastAsync(Owner);
        }
    }
}