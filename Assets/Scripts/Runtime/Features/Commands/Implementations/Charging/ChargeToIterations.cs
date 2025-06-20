using System.Threading;
using System.Threading.Tasks;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.Commands.Supporting.For;
using UnityEngine;

namespace EndlessHeresy.Runtime.Commands.Charging
{
    public sealed class ChargeToIterations : ConditionalCommand
    {
        private readonly float _minChargingDuration;
        private readonly float _maxChargingDuration;
        private readonly float _iterationsPerSeconds;
        private readonly ICommand[] _chargedCommands;
        private readonly ICommand _overChargedCommand;
        private readonly ICommand _underchargedCommand;
        private readonly ICommand _reachedMinimumChargeCommand;
        private readonly ICommand _preIterationsCommand;
        private readonly ICommand _postIterationsCommand;

        public ChargeToIterations(Condition chargingCondition,
            float minChargingDuration,
            float maxChargingDuration,
            float iterationsPerSeconds,
            ICommand[] chargedCommands,
            ICommand overChargedCommand,
            ICommand underchargedCommand,
            ICommand reachedMinimumChargeCommand,
            ICommand preIterationsCommand,
            ICommand postIterationsCommand) : base(chargingCondition)
        {
            _minChargingDuration = minChargingDuration;
            _maxChargingDuration = maxChargingDuration;
            _iterationsPerSeconds = iterationsPerSeconds;
            _chargedCommands = chargedCommands;
            _overChargedCommand = overChargedCommand;
            _underchargedCommand = underchargedCommand;
            _reachedMinimumChargeCommand = reachedMinimumChargeCommand;
            _preIterationsCommand = preIterationsCommand;
            _postIterationsCommand = postIterationsCommand;
        }

        public override async Task ExecuteAsync(IActor actor, CancellationToken cancellationToken)
        {
            var chargingCondition = GetCondition(actor);
            var chargingDuration = 0f;
            var isMinimumChargeExecuted = false;

            do
            {
                await Task.Yield();
                chargingDuration += Time.deltaTime;

                if (!isMinimumChargeExecuted && chargingDuration > _minChargingDuration)
                {
                    isMinimumChargeExecuted = true;
                    await _reachedMinimumChargeCommand.ExecuteAsync(actor, cancellationToken);
                }

                if (chargingDuration < _maxChargingDuration)
                {
                    continue;
                }

                await _overChargedCommand.ExecuteAsync(actor, cancellationToken);
                return;
            } while (chargingCondition.SafeInvoke());

            if (chargingDuration < _minChargingDuration)
            {
                await _underchargedCommand.ExecuteAsync(actor, cancellationToken);
                return;
            }


            await _preIterationsCommand.ExecuteAsync(actor, cancellationToken);
            var iterations = Mathf.RoundToInt(_iterationsPerSeconds * chargingDuration);
            var forLoop = new ForLoop(_chargedCommands, iterations);
            await forLoop.ExecuteAsync(actor, cancellationToken);
            await _postIterationsCommand.ExecuteAsync(actor, cancellationToken);
        }
    }
}