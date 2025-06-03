using System.Collections.Generic;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.StatusEffects.Installers.Abstractions;
using EndlessHeresy.Runtime.StatusEffects.Builder;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.StatusEffects
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/StatusEffect", fileName = "StatusEffectConfiguration", order = 0)]
    public sealed class StatusEffectConfiguration : ScriptableObject
    {
        [SerializeField] private StatusEffectClassType _classIdentifier;
        [SerializeField] private StatusEffectType _identifier;
        [SerializeField] private Sprite _icon;
        [SerializeReference, Select] private List<StatusEffectComponentInstaller> _installers;

        public StatusEffectType Identifier => _identifier;
        public Sprite Icon => _icon;

        public void ConfigureBuilder(StatusEffectsBuilder builder)
        {
            builder.WithId(_identifier);
            builder.WithClass(_classIdentifier);

            foreach (var installer in _installers)
            {
                installer.Install(builder);
            }
        }
    }
}