using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.StatusEffects;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using EndlessHeresy.Runtime.StatusEffects.Implementations;
using UniRx;
using VContainer;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class StatusEffectsComponent : PocoComponent, IStatusEffectsReadOnly
    {
        private readonly IGameplayStaticDataService _gameStaticDataService;
        private readonly IObjectResolver _resolver;
        private readonly IReactiveCollection<IStatusEffectRoot> _activeStatusEffects;

        private StatsComponent _statsComponent;
        public IReadOnlyReactiveCollection<IStatusEffectRoot> ActiveStatusEffects => _activeStatusEffects;

        public StatusEffectsComponent(IGameplayStaticDataService gameplayStaticDataService, IObjectResolver resolver)
        {
            _gameStaticDataService = gameplayStaticDataService;
            _resolver = resolver;
            _activeStatusEffects = new ReactiveCollection<IStatusEffectRoot>();
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _statsComponent = Owner.GetComponent<StatsComponent>();
            return Task.CompletedTask;
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            foreach (var activeEffect in _activeStatusEffects)
            {
                activeEffect.Remove(_statsComponent);
            }
        }

        public void Add(StatusEffectType identifier)
        {
            if (!TryGetEffectData(identifier, out var data))
            {
                return;
            }

            var exisingStatusEffect = _activeStatusEffects.FirstOrDefault(temp => temp.Identifier == identifier);

            if (exisingStatusEffect == null)
            {
                var builder = new StatusEffectsBuilder();
                data.ConfigureBuilder(builder);
                var newStatusEffect = builder.Build(_resolver);
                newStatusEffect.SetOwner(Owner);
                newStatusEffect.Apply(_statsComponent);
                _activeStatusEffects.Add(newStatusEffect);
                return;
            }

            if (exisingStatusEffect.TryGet<StackableStatusEffectComponent>(out var stackableStatusEffect))
            {
                stackableStatusEffect.Apply(_statsComponent);
            }

            if (exisingStatusEffect.TryGet<TemporaryStatusEffectComponent>(out var temporaryStatusEffect))
            {
                temporaryStatusEffect.Reset();
            }
        }

        public void Remove(StatusEffectType identifier)
        {
            var exisingStatusEffect = _activeStatusEffects.FirstOrDefault(temp => temp.Identifier == identifier);

            if (exisingStatusEffect == null)
            {
                return;
            }

            exisingStatusEffect.Remove(_statsComponent);
            _activeStatusEffects.Remove(exisingStatusEffect);
        }

        private bool TryGetEffectData(StatusEffectType identifier, out StatusEffectConfiguration data)
        {
            data = _gameStaticDataService.GetStatusEffectConfiguration(identifier);
            return data != null;
        }
    }
}