using System;
using System.Collections;
using System.Collections.Generic;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Core.Components
{
    [Serializable]
    public sealed class StaticCollectionView<TView> : IEnumerable<TView> where TView : BaseView
    {
        [SerializeField] private List<TView> _items;

        public bool TryGet(int index, out TView item)
        {
            if (index < _items.Count)
            {
                item = _items[index];
                return true;
            }

            item = null;
            return false;
        }

        public IEnumerator<TView> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}