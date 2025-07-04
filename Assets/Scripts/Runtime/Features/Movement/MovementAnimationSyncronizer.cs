using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.AnimationLayers;
using EndlessHeresy.Runtime.Generic;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.Movement
{
    public sealed class MovementAnimationSyncronizer : PocoComponent
    {
        private readonly AnimationLayerSelectorAsset _layerSelectorAsset;
        private const string IdleKey = "Idle";
        private const string RunningKey = "Run";

        private readonly AnimatorLayerType[] _layerIdentifiers;
        private MovementComponent _movement;
        private AnimatorsAggregatorComponent _animatorsAggregator;
        private bool _isLocked;

        public MovementAnimationSyncronizer(AnimationLayerSelectorAsset layerSelectorAsset)
        {
            _layerSelectorAsset = layerSelectorAsset;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _movement = Owner.GetComponent<MovementComponent>();
            _animatorsAggregator = Owner.GetComponent<AnimatorsAggregatorComponent>();
            _movement.MovementProperty.Subscribe(OnMovementChanged).AddTo(CompositeDisposable);
            _movement.IsLockedProperty.Subscribe(OnIsLockedChanged).AddTo(CompositeDisposable);
            return Task.CompletedTask;
        }

        public void Lock()
        {
            _isLocked = true;
        }

        public void Unlock()
        {
            _isLocked = false;
        }

        private void OnIsLockedChanged(bool value)
        {
            if (value)
            {
                return;
            }

            OnMovementChanged(_movement.MovementProperty.Value);
        }

        private void OnMovementChanged(Vector2 value)
        {
            var layers = _layerSelectorAsset.LayerIdentifiers;

            if (_isLocked)
            {
                return;
            }

            if (value == Vector2.zero)
            {
                if (_animatorsAggregator.IsPlaying(IdleKey, layers))
                {
                    return;
                }

                _animatorsAggregator.Play(IdleKey, layers);
                return;
            }

            if (_animatorsAggregator.IsPlaying(RunningKey, layers))
            {
                return;
            }

            _animatorsAggregator.Play(RunningKey, layers);
        }
    }
}