using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace BecomingAnArchmage.Source.Infrastructure.Services.Initialization
{
    public class InitializationChain
    {
        private readonly List<Func<UniTask>> initializationSteps = new();
        public event Action<float> ProgressUpdated; 
        public void AddInitializationStep(Func<UniTask> step)
        {
            initializationSteps.Add(step);
        }

        public async UniTask ExecuteInitializationAsync()
        {
            for (int i = 0; i < initializationSteps.Count; i++)
            {
                await initializationSteps[i]();
            }
        }

        public void Report(float value)
        {
            ProgressUpdated?.Invoke(value);
        }
    }
}