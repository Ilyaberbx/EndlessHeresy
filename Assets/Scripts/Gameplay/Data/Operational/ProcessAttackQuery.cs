using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Operational
{
    public struct ProcessAttackQuery
    {
        public DamageData DamageData;
        public float Force { get; }
        public CapsuleOverlapData OverlapData { get; }
        public Vector2 At { get; }

        public ProcessAttackQuery(DamageData damageData, float force, CapsuleOverlapData overlapData,
            Vector2 at)
        {
            DamageData = damageData;
            Force = force;
            OverlapData = overlapData;
            At = at;
        }
    }
}