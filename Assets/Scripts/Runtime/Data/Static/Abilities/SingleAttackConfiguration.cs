using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Abilities.SingleAttack;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/SingleAttack", fileName = "SingleAttackConfiguration",
        order = 0)]
    public sealed class SingleAttackConfiguration : AbilityConfiguration
    {
        [SerializeField] private int _mouseIndex;
        [SerializeField] private int _cooldown;
        [SerializeField] private AttackData _attackData;

        public int MouseIndex => _mouseIndex;
        public int Cooldown => _cooldown;
        public AttackData AttackData => _attackData;
        public override AbilityFactory GetFactory(IObjectResolver container)
        {
            return new SingleAttackFactory(container, this);
        }
    }
}