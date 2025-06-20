using System;
using System.Linq;
using Better.Attributes.Runtime.Select;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Commands.Charging;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Commands.Installers
{
    [Serializable]
    public sealed class ChargeToIterationsInstaller : ICommandInstaller
    {
        [SerializeField] private float _minChargeDuration;
        [SerializeField] private float _maxChargeDuration;
        [SerializeField] private float _iterationsPerSecond;
        [SerializeReference, Select] private Condition _condition;
        [SerializeReference, Select] private ICommandInstaller _overchargedCommandInstaller;
        [SerializeReference, Select] private ICommandInstaller _underchargedCommandInstaller;
        [SerializeReference, Select] private ICommandInstaller _reachedMinimumChargeCommandInstaller;
        [SerializeReference, Select] private ICommandInstaller _preIterationCommandInstaller;
        [SerializeReference, Select] private ICommandInstaller[] _chargedCommandsInstallers;
        [SerializeReference, Select] private ICommandInstaller _postIterationCommandInstaller;

        public ICommand GetCommand(IObjectResolver resolver)
        {
            var chargedCommands = _chargedCommandsInstallers.Select(installer => installer.GetCommand(resolver))
                .ToArray();
            var overchargedCommand = _overchargedCommandInstaller.GetCommand(resolver);
            var underchargedCommand = _underchargedCommandInstaller.GetCommand(resolver);
            var reachedMinimumChargeCommand = _reachedMinimumChargeCommandInstaller.GetCommand(resolver);
            var preIterationCommand = _preIterationCommandInstaller.GetCommand(resolver);
            var postIterationCommand = _postIterationCommandInstaller.GetCommand(resolver);
            return new ChargeToIterations(_condition, _minChargeDuration, _maxChargeDuration, _iterationsPerSecond,
                chargedCommands,
                overchargedCommand,
                underchargedCommand,
                reachedMinimumChargeCommand,
                preIterationCommand,
                postIterationCommand);
        }
    }
}