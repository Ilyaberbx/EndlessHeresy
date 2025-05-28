using EndlessHeresy.Runtime.UI.Core.MVVM;
using EndlessHeresy.Runtime.UI.Widgets.StatusEffects;

namespace EndlessHeresy.Runtime.UI.Huds.StatusEffects
{
    public sealed class StatusEffectsHudModel : IModel
    {
        public StatusEffectsModel StatusEffectsModel { get; }

        public StatusEffectsHudModel(StatusEffectsModel statusEffectsModel)
        {
            StatusEffectsModel = statusEffectsModel;
        }
    }
}