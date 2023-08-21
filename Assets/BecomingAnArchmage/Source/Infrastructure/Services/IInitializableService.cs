using BecomingAnArchmage.Source.Infrastructure.Services.Initialization;
using Cysharp.Threading.Tasks;

namespace BecomingAnArchmage.Source.Infrastructure.Services
{
    public interface IInitializableService : IHaveInitializationWeight
    {
        UniTask Initialize();
    }
}