using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace EndlessHeresy.Runtime.Behaviour.Events
{
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/OnAbilityUsageFinished")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "OnAbilityUsageFinished", message: "[Ability] Usage Finished", category: "Action/EndlessHeresy", id: "d64bc2462c3e867b970ac88cf3e245e2")]
    public sealed partial class OnAbilityUsageFinished : EventChannel<AbilityType> { }
}

