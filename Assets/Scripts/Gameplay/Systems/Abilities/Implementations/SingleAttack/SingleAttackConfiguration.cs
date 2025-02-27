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

        public float Radius => _radius;
        public int Damage => _damage;
        
        public int MouseIndex => _mouseIndex;
        public override AbilityBuilder GetBuilder(IObjectResolver container) => new SingleAttackBuilder(container, this);
    }
}