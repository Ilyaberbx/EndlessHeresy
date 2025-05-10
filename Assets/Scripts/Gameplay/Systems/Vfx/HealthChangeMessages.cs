using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Core;
using EndlessHeresy.Gameplay.Data.Operational;
using EndlessHeresy.Gameplay.Data.Static.Components;
using EndlessHeresy.Gameplay.Health;
using EndlessHeresy.Gameplay.Services.FloatingMessages;
using EndlessHeresy.Gameplay.Services.StaticData;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Vfx
{
    public sealed class HealthChangeMessages : PocoComponent
    {
        private const string TakeDamageFormat = "- {0}";
        private const float Duration = 1f;

        private IFloatingMessagesService _floatingMessagesService;
        private IGameplayStaticDataService _gameplayStaticDataService;
        private HealthComponent _healthComponent;

        [Inject]
        public void Construct(IFloatingMessagesService floatingMessagesService,
            IGameplayStaticDataService gameplayStaticDataService)
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
            base.OnDispose();
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