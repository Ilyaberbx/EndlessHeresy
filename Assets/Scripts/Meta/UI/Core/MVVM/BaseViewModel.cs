using System;

namespace EndlessHeresy.Meta.UI.Core.MVVM
{
    public abstract class BaseViewModel<TModel> : BaseViewModel where TModel : IModel
    {
        public sealed override void Initialize(IModel derivedModel)
        {
            base.Initialize(derivedModel);

            if (derivedModel is TModel model)
            {
                Model = model;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        protected TModel Model { get; private set; }
    }

    public abstract class BaseViewModel : IDisposable
    {
        protected IModel DerivedModel { get; private set; }

        public virtual void Initialize(IModel derivedModel)
        {
            DerivedModel = derivedModel;
            Show();
        }

        public void Dispose()
        {
            Hide();
            DerivedModel = null;
        }

        protected abstract void Show();
        protected abstract void Hide();
    }
}