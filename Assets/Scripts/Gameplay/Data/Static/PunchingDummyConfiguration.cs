using System.Collections.Generic;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Gameplay.Actors.Enemies;
using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static
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