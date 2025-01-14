using System.Collections.Generic;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Gameplay.Abilities;
using UnityEngine;

namespace EndlessHeresy.Gameplay
{
    public sealed class AbilityStorageComponent : MonoBehaviour
    {
        [SerializeField] private AbilityConfiguration[] _abilityConfigurations;
        private readonly List<Ability> _abilities = new();
        public IReadOnlyList<Ability> Abilities => _abilities;

        public void Initialize()
        {
            _abilities.Clear();

            if (_abilityConfigurations.IsNullOrEmpty())
            {
                return;
            }

            foreach (var configuration in _abilityConfigurations)
            {
                var builder = configuration.GetBuilder();
                var ability = builder.Build();
                _abilities.Add(ability);
            }
        }
    }
}