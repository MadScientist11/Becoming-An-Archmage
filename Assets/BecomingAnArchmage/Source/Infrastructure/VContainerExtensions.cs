using BecomingAnArchmage.Source.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace BecomingAnArchmage.Source.Infrastructure
{
    public static class VContainerExtensions
    {
        public static async UniTask LoadSceneInjected(this ISceneLoader sceneLoader, string path, LoadSceneMode loadSceneMode,
            LifetimeScope parentScope)
        {
            using (LifetimeScope.EnqueueParent(parentScope))
            {
                await sceneLoader.LoadScene(path, loadSceneMode);
            }
        }

        public static void Install(this IContainerBuilder containerBuilder, IInstaller installer)
        {
            installer.Install(containerBuilder);
        }
    }
}