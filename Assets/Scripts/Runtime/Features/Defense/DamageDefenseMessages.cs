using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Services.FloatingMessages;
using UnityEngine;

namespace EndlessHeresy.Runtime.Defense
{
    public sealed class DamageDefenseMessages : PocoComponent
    {
        private const float MessageDuration = 1f;
        private static readonly Vector2 MessageFlyDirection = Vector2.down;
        private readonly IFloatingMessagesService _floatingMessagesService;
        private DamageDefenseComponent _damageDefense;

        public DamageDefenseMessages(IFloatingMessagesService floatingMessagesService)
        {
            _floatingMessagesService = floatingMessagesService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _damageDefense = Owner.GetComponent<DamageDefenseComponent>();
            _damageDefense.OnDefendedByImmune += OnDefendedByImmune;
            _damageDefense.OnAbsorbed += OnAbsorbed;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            _damageDefense.OnDefendedByImmune -= OnDefendedByImmune;
            _damageDefense.OnAbsorbed -= OnAbsorbed;
        }

        private void OnAbsorbed()
        {
            ShowFloatingMessage("Absorbed!", Color.green);
        }

        private void OnDefendedByImmune()
        {
            ShowFloatingMessage("Immune!", Color.white);
        }

        private void ShowFloatingMessage(string message, Color color)
        {
            var query = new ShowFloatingMessageQuery(Owner.Transform.position,
                message,
                MessageDuration,
                color,
                MessageFlyDirection
            );

            _floatingMessagesService.ShowAsync(query).Forget();
        }
    }
}