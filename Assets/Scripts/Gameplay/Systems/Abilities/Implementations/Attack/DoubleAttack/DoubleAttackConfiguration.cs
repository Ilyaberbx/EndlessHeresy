using EndlessHeresy.Gameplay.Data.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities.DoubleAttack
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/DoubleAttack", fileName = "DoubleAttackConfiguration",
        order = 0)]
    public sealed class DoubleAttackConfiguration : AbilityConfiguration
    {
        [SerializeField] private int _mouseIndex;
        [SerializeField] private AttackData _firstAttackData;
        [SerializeField] private AttackData _secondAttackData;
        [SerializeField] private float _cooldown;
        

        public AttackData FirstAttackData => _firstAttackData;
        public AttackData SecondAttackData => _secondAttackData;
        public int MouseIndex => _mouseIndex;
        public float Cooldown => _cooldown;

        public override AbilityFactory GetFactory(IObjectResolver container)
        {
            return new DoubleAttackFactory(container, this);
        }
    }
}