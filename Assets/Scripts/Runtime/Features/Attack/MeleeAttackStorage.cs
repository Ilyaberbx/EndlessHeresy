using EndlessHeresy.Runtime.Actors;
using UnityEngine;

namespace EndlessHeresy.Runtime.Attack
{
    public sealed class MeleeAttackStorage : MonoComponent
    {
        [SerializeField] private Transform _leftAttackPoint;
        [SerializeField] private Transform _rightAttackPoint;

        public Vector2 GetPosition(bool facingRight)
        {
            return facingRight ? _rightAttackPoint.position : _leftAttackPoint.position;
        }
    }
}