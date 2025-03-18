using System.Collections.Generic;
using EndlessHeresy.UI.MVC;
using EndlessHeresy.UI.ViewComponents;
using UnityEngine;

namespace EndlessHeresy.UI.Huds
{
    public sealed class AbilitiesHudView : BaseView
    {
        [SerializeField] private AbilityItemView[] _abilityItemViews;

        public IEnumerable<AbilityItemView> ItemViews => _abilityItemViews;
    }
}