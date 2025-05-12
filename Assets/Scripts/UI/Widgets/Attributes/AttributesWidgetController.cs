using EndlessHeresy.UI.MVC;
using EndlessHeresy.Gameplay.Services.StaticData;
using EndlessHeresy.Gameplay.Data.Identifiers;
using EndlessHeresy.Gameplay.Data.Static.Components;
using UnityEngine;
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
            for (var i = 0; i < view.AttributesView.Length; i++)
            {
                if (i >= attributes.Count)
                {
                    view.AttributesView[i].gameObject.SetActive(false);
                    continue;
                }
                
                var data = _staticDataService.GetAttributeData(attributes[i].Identifier);
                view.AttributesView[i].SetName(string.IsNullOrEmpty(data.DisplayName) ? attributes[i].Identifier.ToString() : data.DisplayName);
                view.AttributesView[i].SetIcon(data.Icon);
                view.AttributesView[i].SetValue(attributes[i].Value);
            }
        }
    }
}