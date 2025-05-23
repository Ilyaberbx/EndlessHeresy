using System;
using Better.Commons.Runtime.Components.UI;
using UnityEngine;

namespace EndlessHeresy.UI.Core.MVVM
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseView<TViewModel> : BaseView where TViewModel : BaseViewModel
    {
        private CanvasGroup _canvasGroup;
        protected TViewModel ViewModel { get; private set; }

        private CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = GetComponent<CanvasGroup>();
                }

                return _canvasGroup;
            }
        }

        public override void Initialize(BaseViewModel derivedViewModel)
        {
            base.Initialize(derivedViewModel);

            if (derivedViewModel is TViewModel viewModel)
            {
                ViewModel = viewModel;
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }

    public abstract class BaseView : UIMonoBehaviour
    {
        protected BaseViewModel DerivedViewModel { get; private set; }

        public virtual void Initialize(BaseViewModel derivedViewModel)
        {
            DerivedViewModel = derivedViewModel;
        }
    }
}