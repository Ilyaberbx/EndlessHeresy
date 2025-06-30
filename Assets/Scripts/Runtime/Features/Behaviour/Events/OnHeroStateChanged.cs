using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;


namespace EndlessHeresy.Runtime.Behaviour.Events
{
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/OnHeroStateChanged")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "OnHeroStateChanged", message: "On [State] Changed",
        category: "Events/EndlessHeresy", id: "4d4313f190dee3441a05fc58c4104c74")]
    public sealed partial class OnHeroStateChanged : EventChannel<HeroState>
    {
    }
}