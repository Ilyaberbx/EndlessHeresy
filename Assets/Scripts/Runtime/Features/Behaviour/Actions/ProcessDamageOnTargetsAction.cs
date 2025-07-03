using System;
using System.Collections.Generic;
using System.Linq;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Data.Operational;
using EndlessHeresy.Runtime.Data.Static;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Health;
using EndlessHeresy.Runtime.Stats;
using EndlessHeresy.Runtime.Utilities;
using Unity.Behavior;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Process Damage On Targets",
        story:
        "Process [Damage] of [Value] with [BonusDamage] on [Targets] with [Attacker] and assign to [DamagedTargets]",
        category: "Action/EndlessHeresy", id: "91ae0720b7df8e79d4ea430a33fd44ca")]
    public partial class ProcessDamageOnTargetsAction : Action
    {
        [SerializeReference] public BlackboardVariable<DamageType> Damage;
        [SerializeReference] public BlackboardVariable<float> Value;
        [SerializeReference] public BlackboardVariable<List<GameObject>> Targets;
        [SerializeReference] public BlackboardVariable<MonoActor> Attacker;
        [SerializeReference] public BlackboardVariable<List<GameObject>> DamagedTargets;
        [SerializeReference] public BlackboardVariable<BonusDamageConfiguration> BonusDamage;

        protected override Status OnStart()
        {
            var attacker = Attacker.Value;
            var damageIdentifier = Damage.Value;
            var baseDamage = Value.Value;
            var bonusDamageConfiguration = BonusDamage.Value;

            var targetActors = Targets.Value
                .Where(temp => !temp.IsUnityNull())
                .Select(temp => temp.GetComponent<IActor>());

            if (!attacker.TryGetComponent<HealthComponent>(out var attackerHealth))
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

                if (targetHealth == attackerHealth)
                {
                    continue;
                }

                if (targetHealth.IsDead())
                {
                    Targets.Value.Remove(targetActor.GameObject);
                    continue;
                }

                var query = new DamageProcessingQuery
                (
                    baseDamage: new DamageData(baseDamage, damageIdentifier),
                    bonusDamageData: bonusDamageConfiguration.Data,
                    attackerStats: Attacker.Value.GetComponent<StatsComponent>()
                );

                var processedDamageData = FightingUtility.ProcessDamage(query, out var isCritical);
                DamagedTargets.Value.Add(targetActor.GameObject);

                foreach (var damageData in processedDamageData)
                {
                    targetHealth.TakeDamage(damageData, isCritical);

                    if (attackerHealth.IsDead())
                    {
                        return Status.Success;
                    }
                }
            }

            return Status.Success;
        }
    }
}