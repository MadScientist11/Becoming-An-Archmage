using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace BecomingAnArchmage.Source.Infrastructure.Services
{
    public interface ISceneLoader
    {
        UniTask LoadScene(string path, LoadSceneMode loadSceneMode);
    }

    public class SceneLoader : ISceneLoader
    {
        private readonly IResourceManager _resourceManager;

        public SceneLoader(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public async UniTask LoadScene(string path, LoadSceneMode loadSceneMode)
        {
            await _resourceManager.LoadScene(path, loadSceneMode);
        }
    }
}