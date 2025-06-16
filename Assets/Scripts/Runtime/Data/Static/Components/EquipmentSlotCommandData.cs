using System;
using Better.Attributes.Runtime.Select;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Commands.Installers;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Components
{
    [Serializable]
    public struct EquipmentSlotCommandData
    {
        [SerializeField] private EquipmentSlotType _identifier;
        [SerializeReference, Select] private UndoableCommandInstaller _undoableCommandInstaller;
        public EquipmentSlotType Identifier => _identifier;
        public UndoableCommandInstaller Installer => _undoableCommandInstaller;
    }
}