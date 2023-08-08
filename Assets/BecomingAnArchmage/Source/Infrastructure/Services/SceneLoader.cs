using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace BecomingAnArchmage.Source.Infrastructure.Services
{
    public interface ISceneLoader
    {
       Task<SceneInstance> LoadScene(string sceneName, Action onLoaded = null);
    }

    public class SceneLoader : ISceneLoader
    {
        public async Task<SceneInstance> LoadScene(string nextScene, Action onLoaded = null)
        {
            AsyncOperationHandle<SceneInstance> asyncOperationHandle = Addressables.LoadSceneAsync(nextScene);
            return await asyncOperationHandle.Task;
        }
    }
}