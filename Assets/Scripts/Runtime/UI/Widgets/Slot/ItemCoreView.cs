using EndlessHeresy.Runtime.UI.Core.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessHeresy.Runtime.UI.Widgets.Slot
{
    public abstract class ItemCoreView<TViewModel> : BaseView<TViewModel> where TViewModel : BaseViewModel
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Button _selectionButton;
        [SerializeField] private GameObject _selectionContainer;

        protected Image IconImage => _iconImage;
        protected Button SelectionButton => _selectionButton;
        protected GameObject SelectionContainer => _selectionContainer;
    }
}