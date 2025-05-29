using EndlessHeresy.Runtime.Services.Tick;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public abstract class PeriodicStatusEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect,
        IRootHandler
    {
        private readonly IGameUpdateService _gameUpdateService;
        protected IStatusEffectRoot Root { get; private set; }
        protected StatsComponent Stats { get; private set; }

        private float _elapsedTime;

        protected PeriodicStatusEffectComponent(IGameUpdateService gameUpdateService)
        {
            _gameUpdateService = gameUpdateService;
        }

        public void Initialize(IStatusEffectRoot root) => Root = root;

        public virtual void Apply(StatsComponent stats)
        {
            Stats = stats;
            _elapsedTime = 0;
            _gameUpdateService.OnUpdate += OnUpdate;
        }

        public virtual void Remove(StatsComponent stats)
        {
            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            _elapsedTime += deltaTime;
            if (_elapsedTime >= GetInterval())
            {
                _elapsedTime = 0;
                OnIntervalTick();
            }
        }

        protected abstract float GetInterval();
        protected abstract void OnIntervalTick();
    }
}