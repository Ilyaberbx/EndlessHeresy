using System;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.UI
{
    [Serializable]
    public struct StatusEffectUIData
    {
        [SerializeField] private Sprite _icon;

        public Sprite Icon => _icon;
    }
}