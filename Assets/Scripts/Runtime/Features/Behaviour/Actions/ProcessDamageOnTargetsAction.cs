using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Utilities;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Process Damage On Targets",
        story:
        "Process [Damage] [Value] with [BonusMultiplier] On [Targets] On [SelfActor] and assign to [DamagedTargets]",
        category: "Action/EndlessHeresy", id: "91ae0720b7df8e79d4ea430a33fd44ca")]
    public partial class ProcessDamageOnTargetsAction : Action
    {
        [SerializeReference] public BlackboardVariable<DamageType> Damage;
        [SerializeReference] public BlackboardVariable<float> Value;
        [SerializeReference] public BlackboardVariable<float> BonusMultiplier;
        [SerializeReference] public BlackboardVariable<List<GameObject>> Targets;
        [SerializeReference] public BlackboardVariable<MonoActor> SelfActor;
        [SerializeReference] public BlackboardVariable<List<GameObject>> DamagedTargets;

        protected override Status OnStart()
        {
            var selfActor = SelfActor.Value;
            var damageIdentifier = Damage.Value;
            var baseDamage = Value.Value;
            var bonusMultiplier = BonusMultiplier.Value;

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

                var finalDamage = FightingUtility.ProcessDamage(selfActor, baseDamage, bonusMultiplier, out var isCritical);
                DamagedTargets.Value.Add(targetActor.GameObject);
                var data = new DamageData(finalDamage, damageIdentifier);
                targetHealth.TakeDamage(data, isCritical);
            }

            return Status.Running;
        }
    }
}