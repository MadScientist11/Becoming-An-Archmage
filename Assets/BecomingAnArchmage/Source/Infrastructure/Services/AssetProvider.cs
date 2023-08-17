using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace BecomingAnArchmage.Source.Infrastructure.Services
{
    public interface IResourceManager
    { 
        UniTask Initialize();
       UniTask LoadAsset();
       UniTask<SceneInstance> LoadScene(string nextScene);
    }

    public class ResourceManager : IResourceManager
    {
        public async UniTask Initialize()
        {
            await UniTask.Yield();
        }

        public async UniTask LoadAsset()
        {
            
        }
        
        public async UniTask<SceneInstance> LoadScene(string nextScene)
        {
            AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.LoadSceneAsync(nextScene);
            return await asyncOperationHandle.Task;
        }
    }
}