using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace BecomingAnArchmage.Source.Infrastructure.Services.Initialization
{
    public class InitializationChain : IProgress<float>
    {
        private readonly List<Func<UniTask>> initializationSteps = new();
        public event Action<float> ProgressUpdated; 
        public void AddInitializationStep(Func<UniTask> step)
        {
            initializationSteps.Add(step);
        }

        public async UniTask ExecuteInitializationAsync(IProgress<float> progress)
        {
            for (int i = 0; i < initializationSteps.Count; i++)
            {
                await initializationSteps[i]();
                progress.Report((float)(i + 1) / initializationSteps.Count);
            }
        }

        public void Report(float value)
        {
            ProgressUpdated?.Invoke(value);
        }
    }
}