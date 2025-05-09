using System.Threading;
using System.Threading.Tasks;
using Better.Conditions.Runtime;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Gameplay.Animations.CrescentStrike;
using EndlessHeresy.Gameplay.Common;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Movement;
using EndlessHeresy.Gameplay.StatusEffects;

namespace EndlessHeresy.Gameplay.Abilities.CrescentStrike
{
    public sealed class CrescentStrikeAbility : MeleeAttackAbility
    {
        private int _chargesToBeReady;
        private int _maxSpinsCount;
        private Condition _keyCondition;
        private AttackData _attackData;

        private int _chargesCounter;
        private int _spinsCounter;
        private bool _isReady;
        private bool _isFinished;

        private StrikeStartAnimation _startAnimation;
        private StrikeChargingAnimation _chargingAnimation;
        private StrikeReadyAnimation _readyAnimation;
        private StrikeSpinningAnimation _spinningAnimation;
        private StrikeFinishAnimation _finishAnimation;
        private AnimationsStorageComponent _animationsStorage;
        private MovementComponent _movementComponent;
        private StatusEffectsComponent _statusEffect;

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);

            _animationsStorage = Owner.GetComponent<AnimationsStorageComponent>();
            _movementComponent = Owner.GetComponent<MovementComponent>();
            _statusEffect = Owner.GetComponent<StatusEffectsComponent>();

            if (TryCollectAnimations())
            {
                SubscribeAnimationEvents();
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            UnsubscribeAnimationEvents();
        }

        public void SetChargesToBeReady(int chargesToBeReady) => _chargesToBeReady = chargesToBeReady;
        public void SetMaxSpinsCount(int maxSpinsCount) => _maxSpinsCount = maxSpinsCount;
        public void SetKeyCondition(Condition keyCondition) => _keyCondition = keyCondition;
        public void SetAttackData(AttackData attackData) => _attackData = attackData;

        public override async Task UseAsync(CancellationToken token)
        {
            await base.UseAsync(token);

            ResetStrike();
            _startAnimation.Play();
            _movementComponent.Lock();
            SetState(AbilityState.InUse);
            await WaitForStrikeFinish(token);
        }


        private async Task WaitForStrikeFinish(CancellationToken token)
        {
            while (!_isFinished)
            {
                if (token.IsCancellationRequested) return;
                await Task.Yield();
            }
        }

        private void SubscribeAnimationEvents()
        {
            _startAnimation.OnEnded += OnStartAnimationEnded;
            _chargingAnimation.OnTick += OnChargingTicked;
            _readyAnimation.OnEnded += OnReadyEnded;
            _spinningAnimation.OnTick += OnSpinningTicked;
            _finishAnimation.OnEnded += OnFinishEnded;
            _finishAnimation.OnHit += OnFinishHit;
        }

        private void UnsubscribeAnimationEvents()
        {
            _startAnimation.OnEnded -= OnStartAnimationEnded;
            _chargingAnimation.OnTick -= OnChargingTicked;
            _readyAnimation.OnEnded -= OnReadyEnded;
            _spinningAnimation.OnTick -= OnSpinningTicked;
            _finishAnimation.OnEnded -= OnFinishEnded;
            _finishAnimation.OnHit -= OnFinishHit;
        }

        private void OnFinishHit()
        {
            var processAttackDto = CollectProcessAttackQuery(_attackData, Owner.Transform.position);
            ProcessAttack(processAttackDto);
        }

        private void OnStartAnimationEnded()
        {
            EnterChargingState();
        }

        private void OnChargingTicked()
        {
            if (_keyCondition.SafeInvoke())
            {
                _chargesCounter++;

                if (_chargesCounter < _chargesToBeReady)
                {
                    return;
                }

                if (_isReady)
                {
                    return;
                }

                ExitChargingState();
                EnterReadyState();
                return;
            }

            if (_isReady)
            {
                ApplySpinning();
                ExitChargingState();
                return;
            }

            ExitChargingState();
            EnterFinishState();
        }

        private void OnReadyEnded()
        {
            _isReady = true;

            if (_keyCondition.SafeInvoke())
            {
                EnterChargingState();
                return;
            }

            ApplySpinning();
        }

        private void OnSpinningTicked()
        {
            _spinsCounter++;

            if (_spinsCounter < _maxSpinsCount)
            {
                var processAttackDto = CollectProcessAttackQuery(_attackData, Owner.Transform.position);
                ProcessAttack(processAttackDto);
                return;
            }

            _movementComponent.Lock();
            ExitSpinningState();
            EnterFinishState();
        }

        private void OnFinishEnded() => FinishStrike();

        private void EnterChargingState() => _chargingAnimation.SetCharging(true);

        private void ExitChargingState() => _chargingAnimation.SetCharging(false);

        private void EnterSpinningState() => _spinningAnimation.SetSpinning(true);

        private void ExitSpinningState() => _spinningAnimation.SetSpinning(false);

        private void EnterReadyState() => _readyAnimation.Play();

        private void EnterFinishState() => _finishAnimation.Play();

        private void ApplySpinning()
        {
            _isReady = false;
            _movementComponent.Unlock();
            _statusEffect.Add(StatusEffectType.Burning);
            EnterSpinningState();
        }

        private void FinishStrike()
        {
            _isFinished = true;
            _movementComponent.Unlock();
            _statusEffect.Remove(StatusEffectType.Burning);
            SetState(AbilityState.Cooldown);
        }

        private void ResetStrike()
        {
            _isReady = false;
            _isFinished = false;
            _chargesCounter = 0;
            _spinsCounter = 0;
        }

        private bool TryCollectAnimations()
        {
            return _animationsStorage.TryGetAnimation(out _startAnimation) &&
                   _animationsStorage.TryGetAnimation(out _chargingAnimation) &&
                   _animationsStorage.TryGetComponent(out _readyAnimation) &&
                   _animationsStorage.TryGetAnimation(out _spinningAnimation) &&
                   _animationsStorage.TryGetAnimation(out _finishAnimation);
        }
    }
}