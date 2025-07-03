using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/BonusDamage", fileName = "BonusDamageConfiguration", order = 0)]
    public sealed class BonusDamageConfiguration : ScriptableObject
    {
        [SerializeField] private BonusDamageData[] _data;

        public IReadOnlyList<BonusDamageData> Data => _data;
    }
}