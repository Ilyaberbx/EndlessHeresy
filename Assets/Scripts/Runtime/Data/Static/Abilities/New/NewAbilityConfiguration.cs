using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Abilities.New.Installers;
using EndlessHeresy.Runtime.NewAbilities;
using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Abilities.New
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/NewAbility", fileName = "NewAbilityConfiguration", order = 0)]
    public sealed class NewAbilityConfiguration : ScriptableObject
    {
        [SerializeField] private AbilityType _identifier;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _description;
        [SerializeField] private string _name;
        [SerializeField] private float _cooldown;
        [SerializeField] private SequenceNodeInstaller _rootInstaller;

        public AbilityType Identifier => _identifier;
        public Sprite Icon => _icon;
        public string Description => _description;
        public string Name => _name;
        public float Cooldown => _cooldown;

        public NewAbility GetAbility()
        {
            var ability = new NewAbility();
            ability.WithCooldown(_cooldown);
            ability.WithIdentifier(_identifier);
            ability.WithRootNode(_rootInstaller.GetNode() as SequenceNode);
            ability.WithInitialState(AbilityState.Ready);
            return ability;
        }
    }
}