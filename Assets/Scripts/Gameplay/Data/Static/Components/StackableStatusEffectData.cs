using System;
using UnityEngine;

namespace EndlessHeresy.Gameplay.Data.Static.Components
{
    [Serializable]
    public sealed class StackableStatusEffectData
    {
        [SerializeField] private bool _isStackable;
        [SerializeField] private bool _isTemporary;
        [SerializeField] private float _duration;
        [SerializeField] private float _progressionPerStack;

        public bool IsStackable => _isStackable;
        public bool IsTemporary => _isTemporary;
        public float Duration => _duration;
        public float ProgressionPerStack => _progressionPerStack;
    }
}