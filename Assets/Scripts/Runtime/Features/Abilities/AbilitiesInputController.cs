using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Services.Input;
using UnityEngine.InputSystem;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesInputController : PocoComponent
    {
        private readonly IInputService _inputService;
        private AbilitiesStorageComponent _storage;
        private AbilitiesCastComponent _cast;

        public AbilitiesInputController(IInputService inputService)
        {
            _inputService = inputService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesStorageComponent>();
            _cast = Owner.GetComponent<AbilitiesCastComponent>();
            _inputService.GameplayActions.FirstAbilityCast.performed += OnFirstAbilityActionPerformed;
            _inputService.GameplayActions.FirstAbilityCast.canceled += OnFirstAbilityActionCanceled;
            _inputService.GameplayActions.SecondAbilityCast.performed += OnSecondAbilityActionPerformed;
            _inputService.GameplayActions.ThirdAbilityCast.performed += OnThirdAbilityActionPerformed;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            _inputService.GameplayActions.FirstAbilityCast.performed -= OnFirstAbilityActionPerformed;
            _inputService.GameplayActions.FirstAbilityCast.canceled -= OnFirstAbilityActionCanceled;
            _inputService.GameplayActions.SecondAbilityCast.performed -= OnSecondAbilityActionPerformed;
            _inputService.GameplayActions.ThirdAbilityCast.performed -= OnThirdAbilityActionPerformed;
        }

        private void OnFirstAbilityActionCanceled(InputAction.CallbackContext context)
        {
            CheckAndCastAbility(AbilityType.SingleAttack);
        }

        private void OnFirstAbilityActionPerformed(InputAction.CallbackContext context)
        {
            CheckAndCastAbility(AbilityType.DoubleAttack);
        }

        private void OnSecondAbilityActionPerformed(InputAction.CallbackContext context)
        {
            CheckAndCastAbility(AbilityType.Dash);
        }

        private void OnThirdAbilityActionPerformed(InputAction.CallbackContext context)
        {
            CheckAndCastAbility(AbilityType.CrescentStrike);
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