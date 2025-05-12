using System.Collections.Generic;
using Better.Commons.Runtime.DataStructures.Properties;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Data.Persistant;
using EndlessHeresy.UI.Core;
using VContainer;

namespace EndlessHeresy.UI.Widgets.Attributes
{
    public sealed class AttributesWidgetController : BaseController<AttributesWidgetModel, AttributesWidgetView>
    {
        private IGameplayStaticDataService _staticDataService;

        [Inject]
        public void Construct(IGameplayStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        protected override void Show(AttributesWidgetModel model, AttributesWidgetView view)
        {
            base.Show(model, view);

            var attributes = model.Attributes.GetAll();

            foreach (var attribute in attributes)
            {
                attribute.Subscribe(OnAttributeChanged);
            }

            UpdateView(attributes);
        }

        protected override void Hide()
        {
            base.Hide();

            foreach (var attribute in Model.Attributes.GetAll())
            {
                attribute.Unsubscribe(OnAttributeChanged);
            }
        }

        private void UpdateView(IReadOnlyList<ReactiveProperty<AttributeData>> attributes)
        {
            for (var i = 0; i < View.AttributesView.Length; i++)
            {
                if (i >= attributes.Count)
                {
                    View.AttributesView[i].gameObject.SetActive(false);
                    continue;
                }

                var property = attributes[i];
                var data = _staticDataService.GetAttributeData(property.Value.Identifier);
                View.AttributesView[i].SetName(string.IsNullOrEmpty(data.DisplayName)
                    ? property.Value.Identifier.ToString()
                    : data.DisplayName);
                View.AttributesView[i].SetIcon(data.Icon);
                View.AttributesView[i].SetValue(property.Value.Value);
            }
        }

        private void OnAttributeChanged(AttributeData data)
        {
            UpdateView(Model.Attributes.GetAll());
        }
    }
}