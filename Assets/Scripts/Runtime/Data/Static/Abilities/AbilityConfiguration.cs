using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Ability", fileName = "AbilityConfiguration", order = 0)]
    public sealed class AbilityConfiguration : ScriptableObject
    {
        [SerializeField] private AbilityType _identifier;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _description;
        [SerializeField] private string _name;
        [SerializeField] private float _cooldown;
        [SerializeReference, Select] private ICommandInstaller _commandInstaller;
        public AbilityType Identifier => _identifier;
        public Sprite Icon => _icon;
        public string Description => _description;
        public string Name => _name;
        public float Cooldown => _cooldown;

        public Ability GetAbility()
        {
            var ability = new Ability();
            ability.WithCooldown(_cooldown);
            ability.WithIdentifier(_identifier);
            ability.WithInitialState(AbilityState.Ready);
            ability.WithCommandInstaller(_commandInstaller);
            return ability;
        }
    }
}