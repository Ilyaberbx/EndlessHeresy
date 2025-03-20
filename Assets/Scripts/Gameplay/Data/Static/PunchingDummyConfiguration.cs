using EndlessHeresy.Gameplay.Actors.Enemies;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Actors/PunchingDummy", fileName = "PunchingDummyConfiguration", order = 0)]
    public sealed class PunchingDummyConfiguration : ScriptableObject
    {
        [SerializeField] private PunchingDummyActor _prefab;

        public PunchingDummyActor Prefab => _prefab;
    }
}