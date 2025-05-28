using System;
using System.Collections;
using System.Collections.Generic;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EndlessHeresy.Runtime.UI.Core.Components
{
    [Serializable]
    public sealed class CollectionView<TView> : IEnumerable<TView> where TView : BaseView
    {
        [SerializeField] private TView _itemPrefab;
        [SerializeField] private Transform _container;

        private List<TView> _items = new();
        
        public TView Add()
        {
            var itemView = Object.Instantiate(_itemPrefab, _container);
            _items.Add(itemView);
            return itemView;
        }

        public bool Remove(TView item)
        {
            if (_items.Remove(item))
            {
                Object.Destroy(item.gameObject);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            foreach (var item in _items)
            {
                Object.Destroy(item.gameObject);
            }

            _items.Clear();
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