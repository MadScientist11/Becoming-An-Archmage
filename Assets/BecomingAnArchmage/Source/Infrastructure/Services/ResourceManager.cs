using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;

namespace BecomingAnArchmage.Source.Infrastructure.Services
{
    public interface IResourceManager
    {
        UniTask Initialize();
        UniTask<T> ProvideAsset<T>(string address) where T : class;
        UniTask<SceneInstance> LoadScene(string nextScene, LoadSceneMode loadSceneMode);
        void CleanUp();
    }

    public class ResourceManager : IResourceManager
    {
        private readonly Dictionary<string, AsyncOperationHandle> _assetsCache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _perAssetHandlesCache = new();

        public async UniTask Initialize()
        {
            // DO WARM UPS in factories
            await UniTask.Yield();
        }

        public async UniTask<T> ProvideAsset<T>(string address) where T : class
        {

            if (_assetsCache.TryGetValue(address, out AsyncOperationHandle assetHandle))
            {
                return assetHandle.Result as T;
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);

            handle.Completed += operationHandle =>
            {
                _assetsCache[address] = operationHandle;
            };

            AddToHandlesCache(address, handle);


            return await handle.Task;
        }

        public void CleanUp()
        {
            foreach (List<AsyncOperationHandle> handles in _perAssetHandlesCache.Values)
            {
                foreach (AsyncOperationHandle handle in handles)
                {
                    Addressables.Release(handle);
                }
            }
        }

        public async UniTask<SceneInstance> LoadScene(string nextScene, LoadSceneMode loadSceneMode)
        {
            AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.LoadSceneAsync(nextScene, loadSceneMode);
            return await asyncOperationHandle.Task;
        }

        private void AddToHandlesCache<T>(string address, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_perAssetHandlesCache.TryGetValue(address, out List<AsyncOperationHandle> handles))
            {
                handles = new List<AsyncOperationHandle>();
                _perAssetHandlesCache[address] = handles;
            }

            handles.Add(handle);
        }
    }
}