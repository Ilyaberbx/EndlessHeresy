using System;
using EndlessHeresy.Gameplay.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct DamageColorData
    {
        [SerializeField] private DamageType _identifier;
        [SerializeField] private Color _color;

        public DamageType Identifier => _identifier;
        public Color Color => _color;
    }
}