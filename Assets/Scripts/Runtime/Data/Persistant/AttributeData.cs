using System;
using EndlessHeresy.Meta.UI.Core.MVVM;
using EndlessHeresy.Runtime.Data.Identifiers;

namespace EndlessHeresy.Runtime.Data.Persistant
{
    [Serializable]
    public sealed class AttributeData : IModel
    {
        public AttributeType Identifier;
        public int Value;
    }
}