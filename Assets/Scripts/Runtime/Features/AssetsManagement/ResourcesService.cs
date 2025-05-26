using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EndlessHeresy.Runtime.AssetsManagement
{
    public sealed class ResourcesService : IAssetsService
    {
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