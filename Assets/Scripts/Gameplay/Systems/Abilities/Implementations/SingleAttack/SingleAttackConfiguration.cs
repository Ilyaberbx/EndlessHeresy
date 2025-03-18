using UnityEngine;
using VContainer;

namespace EndlessHeresy.Gameplay.Abilities.SingleAttack
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/SingleAttack", fileName = "SingleAttackConfiguration",
        order = 0)]
    public sealed class SingleAttackConfiguration : AbilityConfiguration
    {
        [SerializeField] private float _radius;
        [SerializeField] private int _damage;
        [SerializeField] private int _mouseIndex;
        [SerializeField] private int _cooldown;

        public float Radius => _radius;
        public int Damage => _damage;
        public int MouseIndex => _mouseIndex;
        public int Cooldown => _cooldown;

        public override AbilityBuilder GetBuilder(IObjectResolver container)
        {
            return new SingleAttackBuilder(container, this);
        }
    }
}