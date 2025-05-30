using System;
using Better.Commons.Runtime.Components.UI;
using UniRx;
using UnityEngine;

namespace EndlessHeresy.Runtime.UI.Core.MVVM
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseView<TViewModel> : BaseView where TViewModel : BaseViewModel
    {
        private CanvasGroup _canvasGroup;
        public TViewModel ViewModel { get; private set; }

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
                Initialize(ViewModel);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        protected virtual void Initialize(TViewModel viewModel)
        {
        }
    }

    public abstract class BaseView : UIMonoBehaviour
    {
        protected CompositeDisposable CompositeDisposable { get; private set; }
        protected BaseViewModel DerivedViewModel { get; private set; }

        private void Awake()
        {
            CompositeDisposable = new CompositeDisposable();
        }

        public virtual void Initialize(BaseViewModel derivedViewModel)
        {
            DerivedViewModel = derivedViewModel;
        }

        protected virtual void OnDestroy()
        {
            CompositeDisposable.Dispose();
        }
    }
}