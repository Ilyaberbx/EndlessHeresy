using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
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
        [SerializeField] private StatusEffectBehaviourData _behaviourData;

        public StatusEffectType Identifier => _identifier;
        public Sprite Icon => _icon;

        public void ConfigureBuilder(StatusEffectBuilder builder)
        {
            builder.WithId(_identifier);
            builder.WithClass(_classIdentifier);

            foreach (var installer in _behaviourData.Installers)
            {
                installer.Install(builder);
            }
        }
    }
}