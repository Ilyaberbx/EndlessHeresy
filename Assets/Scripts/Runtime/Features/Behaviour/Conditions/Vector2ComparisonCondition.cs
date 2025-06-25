using System;
using Unity.Behavior;
using UnityEngine;

namespace EndlessHeresy.Runtime.Behaviour.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Vector2ComparisonCondition", story: "[FirstVector] [ComparisonOperator] [SecondVector]",
        category: "Conditions/EndlessHeresy", id: "07d1e39df18acc514962a7ecd06f1450")]
    public partial class Vector2ComparisonCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Vector2> FirstVector;

        [Comparison(comparisonType: ComparisonType.Boolean)] [SerializeReference]
        public BlackboardVariable<ConditionOperator> ComparisonOperator;

        [SerializeReference] public BlackboardVariable<Vector2> SecondVector;

        public override bool IsTrue()
        {
            var comparison = ComparisonOperator.Value;
            switch (comparison)
            {
                case ConditionOperator.Equal:
                    return FirstVector.Value == SecondVector.Value;
                case ConditionOperator.NotEqual:
                    return FirstVector.Value != SecondVector.Value;
            }

            return false;
        }
    }
}