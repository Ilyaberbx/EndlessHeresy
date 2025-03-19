using System.Threading.Tasks;
using EndlessHeresy.Gameplay.Abilities.Enums;
using EndlessHeresy.Global.Services.AssetsManagement;
using UnityEngine;

namespace EndlessHeresy.Global.Services.Sprites
{
    public sealed class SpritesService : ISpritesService
    {
        private const string Abilities = "Abilities/";
        private const string Sprites = "Sprites/";

        private readonly IAssetsService _assetsService;

        public SpritesService(IAssetsService assetsService) => _assetsService = assetsService;

        public Task<Sprite> GetAbilitySpriteAsync(AbilityType type)
        {
            return _assetsService.Load<Sprite>(Sprites + Abilities + type);
        }
    }
}