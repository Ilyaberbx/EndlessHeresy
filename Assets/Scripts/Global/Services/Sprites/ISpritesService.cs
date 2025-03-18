using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Abilities.Enums;
using UnityEngine;

namespace EndlessHeresy.Global.Services.Sprites
{
    public interface ISpritesService
    {
        Task<Sprite> GetAbilitySpriteAsync(AbilityType type);
    }
}