using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EndlessHeresy.Runtime.Behaviour.Events;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Services.Tick;
using Unity.Behavior;
using Unity.Behavior.GraphFramework;

namespace EndlessHeresy.Runtime.Abilities
{
    public sealed class AbilitiesCastComponent : PocoComponent
    {
        private readonly IGameUpdateService _gameUpdateService;
        private readonly OnHeroStateChanged _heroStatesChannel;
        private readonly OnAbilityUsageFinished _abilityUsageFinishedChannel;
        private BlackboardReference _blackboard;
        private readonly SerializableGUID _abilityToCastGuid;
        private AbilitiesStorageComponent _storage;

        public AbilitiesCastComponent(IGameUpdateService gameUpdateService,
            OnHeroStateChanged heroStatesChannel,
            OnAbilityUsageFinished abilityUsageFinishedChannel,
            SerializableGUID abilityToCastGuid)
        {
            _heroStatesChannel = heroStatesChannel;
            _abilityUsageFinishedChannel = abilityUsageFinishedChannel;
            _abilityToCastGuid = abilityToCastGuid;
            _gameUpdateService = gameUpdateService;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _storage = Owner.GetComponent<AbilitiesStorageComponent>();
            _blackboard = Owner.GameObject.GetComponent<BehaviorGraphAgent>().BlackboardReference;
            _gameUpdateService.OnUpdate += OnUpdate;
            _abilityUsageFinishedChannel.Event += OnAbilityUsageFinished;
            return Task.CompletedTask;
        }


        protected override void OnDispose()
        {
            _gameUpdateService.OnUpdate -= OnUpdate;
            _abilityUsageFinishedChannel.Event -= OnAbilityUsageFinished;
        }

        private void OnAbilityUsageFinished(AbilityType identifier)
        {
            var ability = _storage.Abilities.FirstOrDefault(temp => temp.Identifier == identifier);
            if (ability == null)
            {
                return;
            }

            if (ability.HasCooldown)
            {
                ability.SetState(AbilityState.Cooldown);
                return;
            }

            ability.SetState(AbilityState.Ready);
        }

        public bool TryCast(AbilityType identifier)
        {
            if (HasActiveAbilities())
            {
                return false;
            }

            var ability = _storage.Abilities.FirstOrDefault(temp => temp.Identifier == identifier);
            if (ability == null)
            {
                return false;
            }

            if (!ability.IsReady())
            {
                return false;
            }

            ability.SetState(AbilityState.InUse);
            _blackboard.SetVariableValue(_abilityToCastGuid, ability.Identifier);
            _heroStatesChannel.SendEventMessage(HeroState.CastingAbility);
            return true;
        }

        private void OnUpdate(float deltaTime)
        {
            foreach (var ability in _storage.Abilities)
            {
                if (ability.State.Value == AbilityState.Cooldown)
                {
                    ability.TickCooldown(deltaTime);
                }
            }
        }

        private bool HasActiveAbilities()
        {
            return _storage.Abilities.Any(temp => temp.State.Value == AbilityState.InUse);
        }
    }
}