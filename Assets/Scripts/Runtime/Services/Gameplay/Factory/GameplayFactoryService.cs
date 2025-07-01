using System.Threading.Tasks;
using DG.Tweening;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Actors.Enemies;
using EndlessHeresy.Runtime.Actors.Hero;
using EndlessHeresy.Runtime.Builder;
using EndlessHeresy.Runtime.Cheats;
using EndlessHeresy.Runtime.Commands;
using EndlessHeresy.Runtime.Controls;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Inventory;
using EndlessHeresy.Runtime.Movement;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.StatusEffects;
using EndlessHeresy.Runtime.Trails;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Services.Gameplay.Factory
{
    public sealed class GameplayFactoryService : IGameplayFactoryService
    {
        private readonly IGameplayStaticDataService _gameplayStaticDataService;
        private readonly IObjectResolver _container;

        public GameplayFactoryService(IGameplayStaticDataService gameplayStaticDataService, IObjectResolver container)
        {
            _gameplayStaticDataService = gameplayStaticDataService;
            _container = container;
        }

        public Task<HeroActor> CreateHeroAsync(Vector2 at)
        {
            var configuration = _gameplayStaticDataService.HeroConfiguration;

            return GetActorBuilder<HeroActor>()
                .ForPrefab(configuration.Prefab)
                .WithPosition(at)
                .WithComponent<MovementComponent>()
                .WithComponent<InputActionPhaseRegistry>()
                .WithComponent<MovementAnimationSyncronizer>()
                .WithComponent<MovementInputController>(configuration.MovementInputData)
                .WithComponent<HealthComponent>()
                .WithComponent<CheatsHudToggler>(configuration.ToggleCheatsInputData)
                .WithComponent<InventoryModalToggler>(configuration.ToggleInventoryInputData)
                .WithComponent<FacingComponent>()
                .WithComponent<CommandsInvokerComponent>()
                .WithComponent<TrailsSpawnerComponent>(configuration.TrailsPoolData.DefaultCapacity,
                    configuration.TrailsPoolData.MaxSize)
                .WithComponent<StatsComponent>(configuration.DefaultStats)
                .WithComponent<StatusEffectsComponent>()
                .WithComponent<HealthChangesMessages>()
                .WithComponent<InventoryComponent>(configuration.MaxInventorySize)
                .WithComponent<AbilitiesHudActivator>()
                .WithComponent<AbilitiesStorageComponent>(configuration.AbilityConfigurations)
                .WithComponent<AbilitiesCastComponent>(configuration.StatesChangeChannel,
                    configuration.AbilityUsageFinishedChannel,
                    configuration.AbilityToCastGuid)
                .WithComponent<AbilitiesInputController>(configuration.AbilitiesInputData)
                .Build();
        }

        public Task<PunchingDummyActor> CreateDummyAsync(Vector2 at)
        {
            var configuration = _gameplayStaticDataService.PunchingDummyConfiguration;

            return GetActorBuilder<PunchingDummyActor>()
                .ForPrefab(configuration.Prefab)
                .WithPosition(at)
                .WithComponent<HealthComponent>()
                .WithComponent<StatusEffectsComponent>()
                .WithComponent<HealthChangesMessages>()
                .WithComponent<CommandsInvokerComponent>()
                .WithComponent<StatsComponent>(configuration.StatsData)
                .Build();
        }

        public void Dispose(IActor actor)
        {
            DOTween.Kill(actor);
            Object.Destroy(actor.GameObject);
            actor.Dispose();
        }

        private MonoActorBuilder<TActor> GetActorBuilder<TActor>() where TActor : MonoActor
        {
            return new MonoActorBuilder<TActor>(_container);
        }
    }
}