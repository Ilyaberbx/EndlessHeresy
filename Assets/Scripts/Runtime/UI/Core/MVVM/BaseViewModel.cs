using System;
using UniRx;

namespace EndlessHeresy.Runtime.UI.Core.MVVM
{
    public abstract class BaseViewModel<TModel> : BaseViewModel where TModel : IModel
    {
        protected TModel Model { get; private set; }

        public sealed override void Initialize(IModel derivedModel)
        {
            base.Initialize(derivedModel);

            if (derivedModel is TModel model)
            {
                Model = model;
                Initialize(Model);
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        protected virtual void Initialize(TModel model)
        {
        }
    }

    public abstract class BaseViewModel : IDisposable
    {
        protected IModel DerivedModel { get; private set; }
        protected CompositeDisposable CompositeDisposable { get; private set; }

        public virtual void Initialize(IModel derivedModel)
        {
            DerivedModel = derivedModel;
            CompositeDisposable = new CompositeDisposable();
            Show();
        }

        public void Dispose()
        {
            Hide();
            OnDispose();
            DerivedModel = null;
            CompositeDisposable.Dispose();
        }

        protected virtual void OnDispose()
        {
        }

        protected virtual void Show()
        {
        }

        protected virtual void Hide()
        {
        }
    }
}