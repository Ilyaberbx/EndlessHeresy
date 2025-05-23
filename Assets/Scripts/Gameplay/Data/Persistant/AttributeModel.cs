using System;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.UI.Core.MVVM;
using UniRx;

namespace EndlessHeresy.Gameplay.Data.Persistant
{
    [Serializable]
    public sealed class AttributeModel : IModel
    {
        public ReactiveProperty<AttributeType> Identifier;
        public ReactiveProperty<int> ValueProperty;

        public AttributeModel(AttributeType identifier, int value)
        {
            Identifier = new ReactiveProperty<AttributeType>(identifier);
            ValueProperty = new ReactiveProperty<int>(value);
        }
    }
}