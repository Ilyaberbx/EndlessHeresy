using UnityEngine;

namespace EndlessHeresy.Gameplay.Actors.Enemies
{
    [CreateAssetMenu(menuName = "Configs/Actors/PunchingDummy", fileName = "PunchingDummyConfiguration", order = 0)]
    public sealed class PunchingDummyConfiguration : ScriptableObject
    {
        [SerializeField] private PunchingDummyActor _prefab;

        public PunchingDummyActor Prefab => _prefab;
    }
}