using EndlessHeresy.UI.Core;
using EndlessHeresy.UI.ViewComponents;
using UnityEngine;

namespace EndlessHeresy.UI.Huds.StatusEffects
{
    public sealed class StatusEffectsHudView : BaseView
    {
        [SerializeField] private StatusEffectItemView[] _itemViews;

        public StatusEffectItemView[] ItemViews => _itemViews;
    }
}