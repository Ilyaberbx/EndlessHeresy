using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Services.FloatingMessages;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.Health
{
    public sealed class HealthChangesMessages : PocoComponent
    {
        private const float MessageDuration = 1f;
        private static readonly Color HealColor = Color.green;
        private static readonly Vector2 MessageFlyDirection = Vector2.up;

        private readonly IFloatingMessagesService _floatingMessagesService;
        private readonly IGameplayStaticDataService _staticDataService;

        private HealthComponent _health;

        public HealthChangesMessages(
            IGameplayStaticDataService staticDataService,
            IFloatingMessagesService floatingMessagesService)
        {
            _floatingMessagesService = floatingMessagesService;
            _staticDataService = staticDataService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _health = Owner.GetComponent<HealthComponent>();

            _health.OnTookDamage += OnTookDamage;

            _health.CurrentHealthProperty
                .Pairwise()
                .Subscribe(OnHealthChanged)
                .AddTo(CompositeDisposable);

            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            _health.OnTookDamage -= OnTookDamage;
        }

        private void OnHealthChanged(Pair<float> healthPair)
        {
            var difference = healthPair.Current - healthPair.Previous;

            if (difference > 0f)
            {
                ShowFloatingMessage($"+ {difference}", HealColor);
            }
        }

        private void OnTookDamage(DamageData damageData)
        {
            var color = _staticDataService.GetDamageColorData(damageData.Identifier).Color;
            ShowFloatingMessage($"- {damageData.Value}", color);
        }

        private void ShowFloatingMessage(string message, Color color)
        {
            var query = new ShowFloatingMessageQuery(Owner.Transform.position,
                message,
                MessageDuration,
                color, MessageFlyDirection
            );

            _floatingMessagesService.ShowAsync(query).Forget();
        }
    }
}