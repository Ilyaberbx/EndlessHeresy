using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Process Damage On Targets",
        story: "Process [Damage] [Value] On [Targets] with [SelfActor] and assign to [DamagedTargets]",
        category: "Action/EndlessHeresy", id: "91ae0720b7df8e79d4ea430a33fd44ca")]
    public partial class ProcessDamageOnTargetsAction : Action
    {
        [SerializeReference] public BlackboardVariable<DamageType> Damage;
        [SerializeReference] public BlackboardVariable<int> Value;
        [SerializeReference] public BlackboardVariable<List<GameObject>> Targets;
        [SerializeReference] public BlackboardVariable<MonoActor> SelfActor;
        [SerializeReference] public BlackboardVariable<List<GameObject>> DamagedTargets;

        protected override Status OnStart()
        {
            var selfActor = SelfActor.Value;
            var damageIdentifier = Damage.Value;
            var value = Value.Value;

            var targetActors = Targets.Value
                .Where(temp => temp != null)
                .Select(temp => temp.GetComponent<IActor>());

            if (!selfActor.TryGetComponent<HealthComponent>(out var selfHealth))
            {
                return Status.Failure;
            }

            foreach (var targetActor in targetActors)
            {
                if (DamagedTargets.Value.Contains(targetActor.GameObject))
                {
                    continue;
                }

                if (!targetActor.TryGetComponent<HealthComponent>(out var targetHealth))
                {
                    continue;
                }

                if (targetHealth == selfHealth)
                {
                    continue;
                }

                if (targetHealth.IsDead())
                {
                    Targets.Value.Remove(targetActor.GameObject);
                    continue;
                }

                DamagedTargets.Value.Add(targetActor.GameObject);
                var data = new DamageData(value, damageIdentifier);
                targetHealth.TakeDamage(data);
            }

            return Status.Running;
        }
    }
}