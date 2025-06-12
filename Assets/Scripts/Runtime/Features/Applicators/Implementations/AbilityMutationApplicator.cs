using System.Linq;
using EndlessHeresy.Runtime.Data.Static.Components;
using EndlessHeresy.Runtime.Extensions;
using EndlessHeresy.Runtime.NewAbilities;
using EndlessHeresy.Runtime.NewAbilities.Nodes;

namespace EndlessHeresy.Runtime.Applicators
{
    public sealed class AbilityMutationApplicator : IApplicator
    {
        private readonly AbilityMutationData _data;
        private AbilitiesNewStorageComponent _storage;
        private bool _isApplied;
        private AbilityNode _newNode;

        public AbilityMutationApplicator(AbilityMutationData data)
        {
            _data = data;
        }

        public void Apply(IActor actor)
        {
            _storage = actor.GetComponent<AbilitiesNewStorageComponent>();
            var abilityToMutate = _storage.Abilities.FirstOrDefault(temp => temp.Identifier == _data.AbilityIdentifier);

            if (abilityToMutate == null)
            {
                _isApplied = false;
                return;
            }

            _newNode = _data.GetNode();
            var targetAbilityType = _data.TargetSerializedType.GetType();

            if (_data.InsertAfter)
            {
                abilityToMutate.RootNode.InsertAfter(targetAbilityType, _data.OccurenceIndex, _newNode);
            }
            else
            {
                abilityToMutate.RootNode.InsertBefore(targetAbilityType, _data.OccurenceIndex, _newNode);
            }

            _isApplied = true;
        }

        public void Remove(IActor actor)
        {
            if (!_isApplied)
            {
                return;
            }

            var abilityToMutate = _storage.Abilities.FirstOrDefault(temp => temp.Identifier == _data.AbilityIdentifier);
            if (abilityToMutate == null)
            {
                _isApplied = false;
                return;
            }

            abilityToMutate.RootNode.RemoveInstance(_newNode);
            _isApplied = false;
        }
    }
}