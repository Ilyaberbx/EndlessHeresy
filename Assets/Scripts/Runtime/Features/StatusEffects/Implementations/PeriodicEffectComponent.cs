using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Services.Tick;
using EndlessHeresy.Runtime.Stats;

namespace EndlessHeresy.Runtime.StatusEffects
{
    public sealed class PeriodicEffectComponent : IStatusEffectComponent,
        IApplyStatusEffect,
        IRemoveStatusEffect
    {
        private readonly IGameUpdateService _gameUpdateService;
        private readonly PeriodicEffectData[] _data;
        private readonly float[] _cooldowns;

        public PeriodicEffectComponent(IGameUpdateService gameUpdateService, PeriodicEffectData[] data)
        {
            _gameUpdateService = gameUpdateService;
            _data = data;
            _cooldowns = new float[_data.Length];
        }

        public void Apply(StatsComponent stats)
        {
            _gameUpdateService.OnUpdate += OnUpdate;
        }

        public void Remove(StatsComponent stats)
        {
            _gameUpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            for (var i = 0; i < _data.Length; i++)
            {
                if (_cooldowns[i] > 0)
                {
                    _cooldowns[i] -= deltaTime;
                    continue;
                }

                var data = _data[i];
                var command = data.CommandInstaller.GetCommand();
                command.Execute();
                _cooldowns[i] = data.Cooldown;
            }
        }
    }
}