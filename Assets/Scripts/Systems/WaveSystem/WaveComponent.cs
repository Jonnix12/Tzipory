﻿using Tzipory.Tools.Interface;

namespace Tzipory.WaveSystem
{
    public abstract class WaveComponent<T> : IProgress
    {
        public abstract T Data { get; }
        public abstract float CompletionPercentage { get; }
        public abstract bool IsDone { get;  }
    }
}