using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.ItemSelection
{
    public abstract class ItemCoreView<TViewModel> : BaseView<TViewModel> where TViewModel : BaseViewModel
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _selectionButton;
        [SerializeField] private GameObject _selectionContainer;
        [SerializeField] private RectTransform _root;

        public Image IconImage => _iconImage;
        public Button SelectionButton => _selectionButton;
        public GameObject SelectionContainer => _selectionContainer;
        public RectTransform Root => _root;
    }
}