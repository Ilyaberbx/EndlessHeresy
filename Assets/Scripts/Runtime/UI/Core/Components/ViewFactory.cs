using System;
using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EndlessHeresy.Runtime.UI.Core.Components
{
    [Serializable]
    public class ViewFactory<TView> where TView : BaseView
    {
        [SerializeField] private TView _itemPrefab;

        public TView CreateView(Transform container)
        {
            return Object.Instantiate(_itemPrefab, container);
        }

        public void DestroyView(TView view)
        {
            Object.Destroy(view.gameObject);
        }
    }
}