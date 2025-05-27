using System.Threading.Tasks;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Services.Gameplay.Factory;
using VContainer;

namespace EndlessHeresy.Runtime.Actors.Enemies
{
    public sealed class PunchingDummyActor : EnemyActor
    {
        private IGameplayFactoryService _gameplayFactoryService;
        private HealthComponent _healthComponent;

        [Inject]
        public void Construct(IGameplayFactoryService gameplayFactoryService) =>
            _gameplayFactoryService = gameplayFactoryService;

        protected override Task OnInitializeAsync()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.OnHealthDepleted += OnHealthDepleted;
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _healthComponent.OnHealthDepleted -= OnHealthDepleted;
        }

        private void OnHealthDepleted()
        {
            _gameplayFactoryService.Dispose(this);
        }
    }
}