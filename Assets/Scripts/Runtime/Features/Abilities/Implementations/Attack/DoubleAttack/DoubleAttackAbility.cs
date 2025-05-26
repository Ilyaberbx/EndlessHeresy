using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Actors;
using EndlessHeresy.Runtime.Animations;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Generic;

namespace EndlessHeresy.Runtime.Abilities.DoubleAttack
{
    public sealed class DoubleAttackAbility : MeleeAttackAbility
    {
        private AttackData _firstAttackData;
        private AttackData _secondAttackData;

        private DoubleAttackAnimation _doubleAttackAnimation;

        private bool _isAttacksFinished;

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            var animationsStorage = Owner.GetComponent<AnimationsStorageComponent>();

            if (animationsStorage.TryGetAnimation(out _doubleAttackAnimation))
            {
                SubscribeAnimationEvents();
            }

            _isAttacksFinished = true;
        }

        public override void Dispose()
        {
            base.Dispose();
            UnsubscribeAnimationEvents();
        }

        public void SetFirstAttackData(AttackData firstAttackData) => _firstAttackData = firstAttackData;
        public void SetSecondAttackData(AttackData secondAttackData) => _secondAttackData = secondAttackData;

        public override async Task UseAsync(CancellationToken token)
        {
            await base.UseAsync(token);
            StartAttacks();
            await WaitForAttacksAsync(token);
            FinishAttacks();
        }

        private void SubscribeAnimationEvents()
        {
            _doubleAttackAnimation.OnFirstHitTriggered += OnFirstHitTriggered;
            _doubleAttackAnimation.OnSecondHitTriggered += OnSecondHitTriggered;
            _doubleAttackAnimation.OnAttacksFinishTriggered += OnAttacksFinishTriggered;
            _doubleAttackAnimation.OnFirstDragTriggered += OnFirstDragTriggered;
            _doubleAttackAnimation.OnSecondDragTriggered += OnSecondDragTriggered;
        }

        private void UnsubscribeAnimationEvents()
        {
            if (_doubleAttackAnimation == null)
            {
                return;
            }

            _doubleAttackAnimation.OnFirstHitTriggered -= OnFirstHitTriggered;
            _doubleAttackAnimation.OnSecondHitTriggered -= OnSecondHitTriggered;
            _doubleAttackAnimation.OnAttacksFinishTriggered -= OnAttacksFinishTriggered;
            _doubleAttackAnimation.OnFirstDragTriggered -= OnFirstDragTriggered;
            _doubleAttackAnimation.OnSecondDragTriggered -= OnSecondDragTriggered;
        }

        private void StartAttacks()
        {
            SetState(AbilityState.InUse);
            FacingComponent.Lock(GetType());
            _doubleAttackAnimation.Play();
            _isAttacksFinished = false;
        }

        private void FinishAttacks()
        {
            FacingComponent.Unlock(GetType());
            SetState(AbilityState.Cooldown);
        }

        private async Task WaitForAttacksAsync(CancellationToken token)
        {
            while (!_isAttacksFinished)
            {
                if (token.IsCancellationRequested) return;
                await Task.Yield();
            }
        }

        private void OnSecondDragTriggered() => ProcessOwnerFacingForce(_secondAttackData.DragForce);
        private void OnFirstDragTriggered() => ProcessOwnerFacingForce(_firstAttackData.DragForce);

        private void OnFirstHitTriggered()
        {
            var processAttackDto = CollectProcessAttackQuery(_firstAttackData, Owner.Transform.position);
            ProcessAttack(processAttackDto);
        }

        private void OnSecondHitTriggered()
        {
            var processAttackDto = CollectProcessAttackQuery(_secondAttackData, Owner.Transform.position);
            ProcessAttack(processAttackDto);
        }

        private void OnAttacksFinishTriggered() => _isAttacksFinished = true;
    }
}