
namespace Microsoft.Extensions.Hosting
{
    using System;
    using DurableTask.Core;
    using DurableTask.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extensions for configuring durable task on <see cref="IHostBuilder"/>.
    /// </summary>
    public static class TaskHubHostBuilderExtensions
    {
        /// <summary>
        /// Configures the task hub worker background service.
        /// </summary>
        /// <param name="builder">The host builder, not null.</param>
        /// <returns>The original host builder with task hub worker configured.</returns>
        public static IHostBuilder ConfigureTaskHubWorker(this IHostBuilder builder)
        {
            return builder.ConfigureTaskHubWorker(_ => { });
        }

        /// <summary>
        /// Configures the task hub worker background service.
        /// </summary>
        /// <param name="builder">The host builder, not null.</param>
        /// <param name="configure">The action to configure the worker, not null.</param>
        /// <returns>The original host builder with task hub worker configured.</returns>
        public static IHostBuilder ConfigureTaskHubWorker(this IHostBuilder builder, Action<ITaskHubWorkerBuilder> configure)
        {
            return builder.ConfigureTaskHubWorker((_, b) => configure(b));
        }

        /// <summary>
        /// Configures the task hub worker background service.
        /// </summary>
        /// <param name="builder">The host builder, not null.</param>
        /// <param name="configure">The action to configure the worker, not null.</param>
        /// <returns>The original host builder with task hub worker configured.</returns>
        public static IHostBuilder ConfigureTaskHubWorker(
            this IHostBuilder builder, Action<HostBuilderContext, ITaskHubWorkerBuilder> configure)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddTaskHubWorker();
                var taskHubBuilder = new DefaultTaskHubWorkerBuilder(services);
                configure(context, taskHubBuilder);
            });

            return builder;
        }
    }
}
