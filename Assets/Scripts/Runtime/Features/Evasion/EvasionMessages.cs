using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Services.FloatingMessages;
using UnityEngine;

namespace EndlessHeresy.Runtime.Evasion
{
    public sealed class EvasionMessages : PocoComponent
    {
        private const float MessageDuration = 1f;
        private readonly IFloatingMessagesService _floatingMessagesService;
        private EvasionComponent _evasion;
        private static readonly Vector2 MessageFlyDirection = Vector2.down;

        public EvasionMessages(IFloatingMessagesService floatingMessagesService)
        {
            _floatingMessagesService = floatingMessagesService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _evasion = Owner.GetComponent<EvasionComponent>();
            _evasion.OnDodged += OnDodged;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            _evasion.OnDodged -= OnDodged;
        }

        private void OnDodged()
        {
            ShowFloatingMessage("Dodged!", Color.white);
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