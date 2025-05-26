using System.Collections.Generic;
using EndlessHeresy.Runtime.Actors.Enemies;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Actors/PunchingDummy", fileName = "PunchingDummyConfiguration", order = 0)]
    public sealed class PunchingDummyConfiguration : ScriptableObject
    {
        [SerializeField] private AddStatData[] _statsData;
        [SerializeField] private PunchingDummyActor _prefab;

        public PunchingDummyActor Prefab => _prefab;
        public IReadOnlyList<AddStatData> StatsData => _statsData;
    }
}