using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Controls;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesInputController : PocoComponent
    {
        private readonly AbilityInputData[] _data;
        private AbilitiesStorageComponent _storage;
        private AbilitiesCastComponent _cast;
        private InputActionPhaseRegistry _inputActionPhaseRegistry;

        public AbilitiesInputController(AbilityInputData[] data)
        {
            _data = data;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesStorageComponent>();
            _cast = Owner.GetComponent<AbilitiesCastComponent>();
            _inputActionPhaseRegistry = Owner.GetComponent<InputActionPhaseRegistry>();

            foreach (var data in _data)
            {
                SubscribeToInputAction(data);
            }

            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            foreach (var data in _data)
            {
                UnsubscribeFromInputAction(data);
            }
        }

        private void SubscribeToInputAction(AbilityInputData data)
        {
            switch (data.ActionPhase)
            {
                case InputActionPhase.Started:
                    data.Action.started += OnActionStarted;
                    break;
                case InputActionPhase.Performed:
                    data.Action.performed += OnActionPerformed;
                    break;
                case InputActionPhase.Canceled:
                    data.Action.canceled += OnActionCanceled;
                    break;
            }
        }

        private void UnsubscribeFromInputAction(AbilityInputData data)
        {
            switch (data.ActionPhase)
            {
                case InputActionPhase.Started:
                    data.Action.started -= OnActionStarted;
                    break;
                case InputActionPhase.Performed:
                    data.Action.performed -= OnActionPerformed;
                    break;
                case InputActionPhase.Canceled:
                    data.Action.canceled -= OnActionCanceled;
                    break;
            }
        }

        private void OnActionStarted(InputAction.CallbackContext context)
        {
            HandleAction(context, InputActionPhase.Started);
        }

        private void OnActionPerformed(InputAction.CallbackContext context)
        {
            HandleAction(context, InputActionPhase.Performed);
        }

        private void OnActionCanceled(InputAction.CallbackContext context)
        {
            HandleAction(context, InputActionPhase.Canceled);
        }

        private void HandleAction(InputAction.CallbackContext context, InputActionPhase phase)
        {
            var phaseAbilityData = _data.Where(temp => temp.ActionPhase == phase).ToArray();
            if (!phaseAbilityData.Any())
            {
                return;
            }

            var index = Array.IndexOf(phaseAbilityData.Select(temp => temp.Action).ToArray(), context.action);
            var data = phaseAbilityData.ElementAt(index);
            var abilityIdentifier = data.AbilityIdentifier;
            _inputActionPhaseRegistry.Update(data.Action, phase);
            CheckAndCastAbility(abilityIdentifier);
        }

        private void CheckAndCastAbility(AbilityType identifier)
        {
            if (_storage.Abilities.Any(temp => temp.IsInUse()))
            {
                return;
            }

            _cast.TryCast(identifier);
        }
    }
}