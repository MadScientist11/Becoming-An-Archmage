using System;
using UnityEngine;

namespace BecomingAnArchmage.Source.Infrastructure.Services
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        float Current { get; }
        float FixedDeltaTime { get; }
    }

    public class TimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
        public float Current => Time.time;

        public float FixedDeltaTime => Time.fixedDeltaTime;

    }
}