//  ----------------------------------------------------------------------------------
//  Copyright Microsoft Corporation
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  http://www.apache.org/licenses/LICENSE-2.0
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  ----------------------------------------------------------------------------------

namespace DurableTask.Hosting
{
    using System;
    using DurableTask.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Extensions for configuring a task hub worker service on <see cref="IHostBuilder"/>.
    /// </summary>
    public static class TaskHubHostBuilderExtensions
    {
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
                services.AddTaskHubWorker(taskHubBuilder => configure(context, taskHubBuilder));
                services.AddHostedService<TaskHubBackgroundService>();
            });

            return builder;
        }
    }
}
