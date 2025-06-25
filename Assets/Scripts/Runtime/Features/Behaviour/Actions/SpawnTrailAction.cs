using System;
using Better.Commons.Runtime.Extensions;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Generic;
using EndlessHeresy.Runtime.Vfx;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Spawn Trail", story: "Spawn Trail with [Color] and [Lifetime] on [Actor]",
        category: "Action/EndlessHeresy", id: "946251c32c69133decafdb98fc5708da")]
    public partial class SpawnTrailAction : Action
    {
        [SerializeReference] public BlackboardVariable<Color> Color;
        [SerializeReference] public BlackboardVariable<float> Lifetime;
        [SerializeReference] public BlackboardVariable<MonoActor> Actor;

        protected override Status OnStart()
        {
            var lifeTime = Lifetime.Value;
            var color = Color.Value;
            var actor = Actor.Value;
            var at = actor.Transform.position;
            var trailsSpawner = actor.GetComponent<TrailsSpawnerComponent>();
            var spriteRendererStorage = actor.GetComponent<SpriteRendererComponent>();
            var query = new SpawnTrailQuery(lifeTime, color, at, spriteRendererStorage.SpriteRenderer.sprite,
                "Trail");
            trailsSpawner.SpawnTrailsAsync(query).Forget();
            return Status.Running;
        }
    }
}