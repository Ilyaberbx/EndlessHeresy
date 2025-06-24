using System;
using EndlessHeresy.Runtime.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Play Animation ", story: "Play [Animation] on [Actor]", category: "Action/EndlessHeresy",
        id: "8c47f21655f04f2998ef09e81b78f67d")]
    public partial class PlayAnimationAction : Action
    {
        [SerializeReference] public BlackboardVariable<string> Animation;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        protected override Status OnStart()
        {
            var animator = Actor.Value.GetComponent<AnimatorStorageComponent>().Animator;
            animator.Play(Animation.Value);
            return Status.Success;
        }
    }
}