using System;
using System.Collections;
using System.Collections.Generic;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Core.Components
{
    [Serializable]
    public sealed class CollectionView<TView> : IEnumerable<TView> where TView : BaseView
    {
        [SerializeField] private ViewFactory<TView> _factory;
        [SerializeField] private Transform _container;

        private List<TView> _items = new();

        public TView Add()
        {
            var itemView = _factory.CreateView(_container);
            _items.Add(itemView);
            return itemView;
        }

        public bool Remove(TView item)
        {
            if (_items.Remove(item))
            {
                _factory.DestroyView(item);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            foreach (var item in _items)
            {
                _factory.DestroyView(item);
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