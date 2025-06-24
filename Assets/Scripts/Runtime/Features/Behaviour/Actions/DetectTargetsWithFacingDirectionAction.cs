using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.CustomGizmos;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.Facing;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Utilities;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Detect Targets With Facing Direction",
        story:
        "Detect [Targets] with facing direction using [CapsuleCenter] and [CapsuleSize] with [OffSet] on [Actor] and assign [Point]",
        category: "Action/EndlessHeresy", id: "8c18504848557bb69e273a2051a526b2")]
    public partial class DetectTargetsWithFacingDirectionAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> CapsuleCenter;
        [SerializeReference] public BlackboardVariable<Vector2> CapsuleSize;
        [SerializeReference] public BlackboardVariable<Vector2> OffSet;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;
        [SerializeReference] public BlackboardVariable<List<GameObject>> Targets;
        [SerializeReference] public BlackboardVariable<Vector2> Point;

        private FacingComponent _facingComponent;

        protected override Status OnStart()
        {
            var actor = Actor.Value;
            if (actor == null)
            {
                return Status.Failure;
            }

            if (!actor.TryGetComponent(out _facingComponent))
            {
                return Status.Failure;
            }

            var ownerPosition = actor.Transform.position;
            var offSet = OffSet.Value;
            var attackPosition = ownerPosition
                .AddX(_facingComponent.IsFacingRight ? offSet.x : -offSet.x)
                .AddY(offSet.y)
                .ToVector2();
            var overlapData = new CapsuleOverlapData(CapsuleCenter.Value, CapsuleSize.Value,
                CapsuleDirection2D.Horizontal, 0f);

            var isDetectedSomeone = GamePhysicsUtility.TryOverlapCapsuleAll<HealthComponent>(
                overlapData,
                attackPosition,
                out var healthComponents);

            Point.Value = attackPosition;

            if (!isDetectedSomeone)
            {
                return Status.Interrupted;
            }

            foreach (var target in healthComponents.Select(temp => temp.Owner.GameObject))
            {
                if (actor.GameObject == target)
                {
                    continue;
                }

                Targets.Value.Add(target);
            }

            var gizmosDrawer = actor.GetComponent<GizmosOverlapDrawer>();
            gizmosDrawer.SetOverlapData(overlapData, attackPosition);
            return Status.Success;
        }
    }
}