using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Animations;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Generic;

namespace EndlessHeresy.Runtime.Abilities.SingleAttack
{
    public sealed class SingleAttackAbility : MeleeAttackAbility
    {
        private AttackData _attackData;
        private SingleAttackAnimation _singleAttackAnimation;
        private bool _isAttackFinished;

        public override void Initialize(IActor owner)
        {
            base.Initialize(owner);
            var animationsStorage = Owner.GetComponent<AnimationsStorageComponent>();

            if (animationsStorage.TryGetAnimation(out _singleAttackAnimation))
            {
                SubscribeAnimationEvents();
            }

            _isAttackFinished = true;
        }

        public override void Dispose()
        {
            base.Dispose();
            UnsubscribeAnimationEvents();
        }

        public void SetAttackData(AttackData attackData) => _attackData = attackData;

        public override async Task UseAsync(CancellationToken token)
        {
            await base.UseAsync(token);
            StartAttack();
            await WaitForAttackAsync(token);
            FinishAttacks();
        }

        private void FinishAttacks()
        {
            FacingComponent.Unlock(GetType());
            SetState(AbilityState.Cooldown);
        }

        private void SubscribeAnimationEvents()
        {
            _singleAttackAnimation.OnAttackTriggered += OnAttackTriggered;
            _singleAttackAnimation.OnAttackFinished += OnAttackFinished;
        }

        private void UnsubscribeAnimationEvents()
        {
            if (_singleAttackAnimation == null)
            {
                return;
            }

            _singleAttackAnimation.OnAttackTriggered -= OnAttackTriggered;
            _singleAttackAnimation.OnAttackFinished -= OnAttackFinished;
        }

        private void StartAttack()
        {
            SetState(AbilityState.InUse);
            FacingComponent.Lock(GetType());
            _singleAttackAnimation.Play();
            _isAttackFinished = false;
        }

        private async Task WaitForAttackAsync(CancellationToken token)
        {
            while (!_isAttackFinished)
            {
                if (token.IsCancellationRequested) return;
                await Task.Yield();
            }
        }

        private void OnAttackTriggered()
        {
            ProcessOwnerFacingForce(_attackData.DragForce);
            var processAttackDto = CollectProcessAttackQuery(_attackData, Owner.Transform.position);
            ProcessAttack(processAttackDto);
        }

        private void OnAttackFinished() => _isAttackFinished = true;
    }
}