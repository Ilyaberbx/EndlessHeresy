using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EndlessHeresy.Global.Services.AssetsManagement
{
    public sealed class ResourcesService : IAssetsService
    {
        private const string ResourcesFolder = "Resources/";

        public Task<TAsset[]> LoadAll<TAsset>(string address) where TAsset : Object
        {
            var assets = Resources.LoadAll<TAsset>(address);
            return Task.FromResult(assets);
        }

        public Task<TAsset> Load<TAsset>(string address) where TAsset : Object
        {
            var asset = Resources.Load<TAsset>(address);
            return Task.FromResult(asset);
        }
    }
}