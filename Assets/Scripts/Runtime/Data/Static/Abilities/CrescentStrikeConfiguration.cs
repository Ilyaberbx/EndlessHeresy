using EndlessHeresy.Runtime.Abilities;
using EndlessHeresy.Runtime.Abilities.CrescentStrike;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine;
using VContainer;

namespace EndlessHeresy.Runtime.Data.Static.Abilities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Abilities/CrescentStrike", fileName = "CrescentStrikeConfiguration",
        order = 0)]
    public sealed class CrescentStrikeConfiguration : AbilityConfiguration
    {
        [SerializeField] private int _chargesToBeReady;
        [SerializeField] private int _maxSpinsCount;
        [SerializeField] private float _cooldown;
        [SerializeField] private AttackData _attackData;
        [SerializeField] KeyCode _keyCode;

        public int ChargesToBeReady => _chargesToBeReady;
        public int MaxSpinsCount => _maxSpinsCount;
        public float Cooldown => _cooldown;
        public KeyCode KeyCode => _keyCode;

        public AttackData AttackData => _attackData;

        public override AbilityFactory GetFactory(IObjectResolver container)
        {
            return new CrescentStrikeFactory(container, this);
        }
    }
}