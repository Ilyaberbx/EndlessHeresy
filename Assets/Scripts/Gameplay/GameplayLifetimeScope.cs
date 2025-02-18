using EndlessHeresy.Gameplay.Services.Factory;
using EndlessHeresy.Gameplay.Services.Input;
using EndlessHeresy.Gameplay.Services.StaticData;
using VContainer;
using VContainer.Unity;

namespace EndlessHeresy.Gameplay
{
    public sealed class GameplayLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayEntryPoint>();
            builder.Register<IGameplayFactoryService, GameplayFactoryService>(Lifetime.Singleton);
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<IGameplayStaticDataService, GameplayStaticDataService>(Lifetime.Singleton);
        }
    }
}