using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Operational
{
    public struct ProcessAttackQuery
    {
        public int Damage { get; }
        public float Force { get; }
        public CapsuleOverlapData OverlapData { get; }
        public Vector2 At { get; }

        public ProcessAttackQuery(int damage, float force, CapsuleOverlapData overlapData, Vector2 at)
        {
            Damage = damage;
            Force = force;
            OverlapData = overlapData;
            At = at;
        }
    }
}