using Cysharp.Threading.Tasks;

namespace BecomingAnArchmage.Source.Infrastructure.Services
{
    public interface IInitializableService
    {
        UniTask Initialize();
    }
}