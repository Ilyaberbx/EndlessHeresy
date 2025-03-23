using EndlessHeresy.Gameplay.Abilities;
using EndlessHeresy.Gameplay.Abilities.DoubleAttack;
using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Data.Static.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/DoubleAttack", fileName = "DoubleAttackConfiguration",
        order = 0)]
    public sealed class DoubleAttackConfiguration : AbilityConfiguration
    {
        [SerializeField] private int _mouseIndex;
        [SerializeField] private AttackData _firstAttackDto;
        [SerializeField] private AttackData _secondAttackDto;
        [SerializeField] private float _cooldown;
        

        public AttackData FirstAttackDto => _firstAttackDto;
        public AttackData SecondAttackDto => _secondAttackDto;
        public int MouseIndex => _mouseIndex;
        public float Cooldown => _cooldown;

        public override AbilityFactory GetFactory(IObjectResolver container)
        {
            return new DoubleAttackFactory(container, this);
        }
    }
}