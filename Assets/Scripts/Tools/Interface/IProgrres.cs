namespace Tzipory.Tools.Interface
{
    public interface IProgress
    {
        public float CompletionPercentage { get; }
        public bool IsDone { get; }
    }
}