using System.Collections.Generic;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using UnityEngine.U2D.Animation;

namespace EndlessHeresy.Runtime.EquipmentSword
{
    public sealed class EquipmentSwordStorage : PocoComponent
    {
        private readonly Dictionary<EquipmentSlotType, SpriteLibraryAsset> _swordLibraryAssets;

        public EquipmentSwordStorage(EquipmentSwordData[] defaultData)
        {
            _swordLibraryAssets = new Dictionary<EquipmentSlotType, SpriteLibraryAsset>();

            foreach (var data in defaultData)
            {
                _swordLibraryAssets.TryAdd(data.SlotIdentifier, data.LibraryAsset);
            }
        }

        public bool TryGetSwordLibraryAsset(EquipmentSlotType slotIdentifier, out SpriteLibraryAsset asset)
        {
            return _swordLibraryAssets.TryGetValue(slotIdentifier, out asset);
        }

        public void SetSwordLibraryAsset(EquipmentSlotType slotIdentifier, SpriteLibraryAsset asset)
        {
            if (_swordLibraryAssets.TryAdd(slotIdentifier, asset))
            {
                return;
            }

            _swordLibraryAssets[slotIdentifier] = asset;
        }
    }
}