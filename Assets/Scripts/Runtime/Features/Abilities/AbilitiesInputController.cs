using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Services.Input;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesInputController : PocoComponent
    {
        private readonly AbilityInputData[] _data;
        private readonly IInputService _inputService;
        private AbilitiesStorageComponent _storage;
        private AbilitiesCastComponent _cast;

        public AbilitiesInputController(AbilityInputData[] data)
        {
            _data = data;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesStorageComponent>();
            _cast = Owner.GetComponent<AbilitiesCastComponent>();

            foreach (var data in _data)
            {
                data.Action.performed += OnActionPerformed;
            }

            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            foreach (var data in _data)
            {
                data.Action.performed -= OnActionPerformed;
            }
        }

        private void OnActionPerformed(InputAction.CallbackContext context)
        {
            var index = Array.IndexOf(_data.Select(temp => temp.Action).ToArray(), context.action);
            var abilityIdentifier = _data[index].AbilityIdentifier;
            CheckAndCastAbility(abilityIdentifier);
        }

        private void CheckAndCastAbility(AbilityType identifier)
        {
            if (_storage.Abilities.Any(temp => temp.IsInUse()))
            {
                return;
            }

            _cast.TryCastAsync(identifier).Forget();
        }
    }
}