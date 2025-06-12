using System;
using Better.Attributes.Runtime.Select;
using Better.Conditions.Runtime;
using EndlessHeresy.Runtime.NewAbilities.Nodes;
using UnityEngine;

namespace EndlessHeresy.Runtime.Data.Static.Abilities.New.Installers
{
    [Serializable]
    public sealed class ConditionalNodeInstaller : AbilityNodeInstaller
    {
        [SerializeReference, Select] private AbilityNodeInstaller _trueNode;
        [SerializeReference, Select] private AbilityNodeInstaller _falseNode;
        [SerializeReference, Select] private Condition _condition;

        public override AbilityNode GetNode()
        {
            return new ConditionalNode(_trueNode.GetNode(), _falseNode.GetNode(), _condition);
        }
    }
}