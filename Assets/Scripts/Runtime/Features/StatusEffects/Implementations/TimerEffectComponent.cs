using EndlessHeresy.Runtime.Services.Tick;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class TimerEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect
    {
        private readonly IGameUpdateService _gameUpdateService;
        private readonly Timer.Timer _timer;

        public TimerEffectComponent(IGameUpdateService gameUpdateService, Timer.Timer timer)
        {
            _gameUpdateService = gameUpdateService;
            _timer = timer;
        }

        public void Apply(StatsComponent stats)
        {
            _timer.Start();
            _gameUpdateService.OnUpdate += OnUpdate;
        }

        public void Remove(StatsComponent stats)
        {
            _timer.Stop();
            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float delta) => _timer.Update(delta);
    }
}