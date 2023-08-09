using System;
using UnityEngine;

namespace BecomingAnArchmage.Source.Infrastructure.Services
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        int Hour { get; }
    }

    public class TimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
        public int Hour => DateTime.Now.Hour;
    }
}