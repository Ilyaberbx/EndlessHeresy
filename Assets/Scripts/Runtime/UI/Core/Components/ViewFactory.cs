using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Core.Components
{
    public class ViewFactory<TView> : MonoBehaviour where TView : BaseView
    {
        [SerializeField] private TView _itemPrefab;

        public TView CreateView(Transform container)
        {
            return Instantiate(_itemPrefab, container);
        }

        public void DestroyView(TView view)
        {
            Destroy(view.gameObject);
        }
    }
}