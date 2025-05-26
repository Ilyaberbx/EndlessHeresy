using System;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.UI
{
    [Serializable]
    public struct StatusEffectUIData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _displayName;

        public Sprite Icon => _icon;
        public string DisplayName => _displayName;
    }
}