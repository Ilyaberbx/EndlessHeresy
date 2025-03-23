using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Operational;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.UI.Services.FloatingMessages;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Effects
{
    public sealed class HealthChangeMessages : PocoComponent
    {
        private const string TakeDamageFormat = "- {0}";
        private const float Duration = 1f;

        private IFloatingMessagesService _floatingMessagesService;
        private HealthComponent _healthComponent;

        [Inject]
        public void Construct(IFloatingMessagesService floatingMessagesService)
        {
            _floatingMessagesService = floatingMessagesService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _healthComponent = Owner.GetComponent<HealthComponent>();
            _healthComponent.OnTakeDamage += OnTakeDamage;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _healthComponent.OnTakeDamage -= OnTakeDamage;
        }

        private void OnTakeDamage(float damage)
        {
            var at = Owner.Transform.position;
            var message = string.Format(TakeDamageFormat, damage);
            var messageData = new ShowFloatingMessageDto(at, message, Duration, Color.red, Vector2.up);
            _floatingMessagesService.ShowAsync(messageData).Forget();
        }
    }
}