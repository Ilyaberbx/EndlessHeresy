using System;
using EndlessHeresy.Runtime.Data.Identifiers;
using EndlessHeresy.Runtime.UI.Core.MVVM;

namespace EndlessHeresy.Runtime.Data.Persistant
{
    [Serializable]
    public sealed class AttributeData : IModel
    {
        public AttributeType Identifier;
        public int Value;
    }
}