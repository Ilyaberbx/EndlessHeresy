using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.Services.Gameplay.StaticData;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using VContainer;
using Action = Unity.Behavior.Action;

namespace EndlessHeresy.Runtime.Behaviour.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Update Ability Subgraph based on AbilityToCast ",
        story: "Update [AbilitySubgraph] based on [AbilityToCast] using [Resolver]", category: "Action/EndlessHeresy",
        id: "88f72f335d7bc053da608019f89906e4")]
    public partial class UpdateAbilitySubgraphBasedOnAbilityToCastAction : Action
    {
        [SerializeReference] public BlackboardVariable<BehaviorGraph> AbilitySubgraph;
        [SerializeReference] public BlackboardVariable<AbilityType> AbilityToCast;
        [SerializeReference] public BlackboardVariable<BehaviourObjectResolver> Resolver;

        protected override Status OnStart()
        {
            var resolver = Resolver.Value.Resolver;
            var gameplayStaticDataService = resolver.Resolve<IGameplayStaticDataService>();
            var data = gameplayStaticDataService.GetAbilityData(AbilityToCast.Value);

            if (data == null)
            {
                return Status.Failure;
            }

            var graph = data.Graph;
            if (graph == null)
            {
                return Status.Failure;
            }

            AbilitySubgraph.Value = graph;
            return Status.Success;
        }
    }
}