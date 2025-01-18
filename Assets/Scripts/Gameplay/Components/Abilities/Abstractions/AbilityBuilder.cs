using System;
using DebugUtility = Better.Commons.Runtime.Utility.DebugUtility;

namespace EndlessHeresy.Gameplay.Abilities
{
    public abstract class AbilityBuilder
    {
        private Ability _ability;
        private readonly AbilityConfiguration _configuration;

        protected AbilityBuilder(AbilityConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Ability Build()
        {
            _ability = BuildInternally();
            if (_ability == null)
            {
                DebugUtility.LogException<NullReferenceException>(ConcreteAbilityNullMessage());
                return null;
            }

            _ability.SetHotkey(_configuration.HotKey);
            _ability.SetCooldown(_configuration.Cooldown);
            _ability.SetStatus(AbilityStatus.Ready);
            return _ability;
        }

        protected abstract Ability BuildInternally();
        private string ConcreteAbilityNullMessage() => $"Ability created by {GetType().Name} is null.";
    }
}