using System;
using EndlessHeresy.Gameplay.Data.Identifiers;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public struct StatPercentageData
    {
        [SerializeField] private StatType _identifier;
        [SerializeField, Range(0, 100)] private int _percentage;
    }
}