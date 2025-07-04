using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public sealed class EquipmentSwordData
    {
        [SerializeField] private EquipmentSlotType _slotIdentifier;
        [SerializeField] private SpriteLibraryAsset _spriteLibraryAsset;

        public EquipmentSlotType SlotIdentifier => _slotIdentifier;
        public SpriteLibraryAsset LibraryAsset => _spriteLibraryAsset;

        public EquipmentSwordData(EquipmentSlotType slotIdentifier, SpriteLibraryAsset spriteLibraryAsset)
        {
            _slotIdentifier = slotIdentifier;
            _spriteLibraryAsset = spriteLibraryAsset;
        }
    }
}