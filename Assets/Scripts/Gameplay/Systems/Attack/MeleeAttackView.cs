using EndlessHeresy.Core;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Attack
{
    public sealed class MeleeAttackView : MonoComponent
    {
        [SerializeField] private Transform _leftAttackPoint;
        [SerializeField] private Transform _rightAttackPoint;

        public Vector2 GetAttackPosition(bool isFacingRight)
        {
            return isFacingRight ? _rightAttackPoint.position : _leftAttackPoint.position;
        }
    }
}