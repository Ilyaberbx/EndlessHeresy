using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Services.FloatingMessages;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using UnityEngine;

namespace EndlessHeresy.Runtime.Vfx
{
    public sealed class HealthChangeMessages : PocoComponent
    {
        private const string TakeDamageFormat = "- {0}";
        private const float Duration = 1f;

        private readonly IFloatingMessagesService _floatingMessagesService;
        private readonly IGameplayStaticDataService _gameplayStaticDataService;

        private HealthComponent _healthComponent;

        public HealthChangeMessages(IGameplayStaticDataService gameplayStaticDataService,
            IFloatingMessagesService floatingMessagesService)
        {
            _floatingMessagesService = floatingMessagesService;
            _gameplayStaticDataService = gameplayStaticDataService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _healthComponent = Owner.GetComponent<HealthComponent>();
            _healthComponent.OnTakeDamage += OnTakeDamage;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            _healthComponent.OnTakeDamage -= OnTakeDamage;
        }

        private void OnTakeDamage(DamageData data)
        {
            var at = Owner.Transform.position;
            var message = string.Format(TakeDamageFormat, data.Value);
            var colorData = _gameplayStaticDataService.GetDamageColorData(data.Identifier);
            var showMessageDto = new ShowFloatingMessageQuery(at, message, Duration, colorData.Color, Vector2.up);
            _floatingMessagesService.ShowAsync(showMessageDto).Forget();
        }
    }
}