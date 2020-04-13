namespace DurableTask.DependencyInjection
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The scope for a running orchestration instance.
    /// </summary>
    public interface IOrchestrationScope : IServiceScope
    {
        /// <summary>
        /// Signals that the middleware portion has completed, so the service scope can be disposed.
        /// </summary>
        void SignalMiddlewareCompletion();

        /// <summary>
        /// Wait for middleware to complete processing,
        /// </summary>
        /// <returns>A task that completes when middleware is done.</returns>
        Task WaitForMiddlewareCompletionAsync();
    }
}
