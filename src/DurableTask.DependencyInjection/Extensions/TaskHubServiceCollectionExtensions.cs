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

namespace DurableTask.DependencyInjection
{
    using System;
    using DurableTask.Core;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// Extension methods for adding task hub services to a service collection.
    /// </summary>
    public static class TaskHubServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a <see cref="TaskHubWorker"/> and related services to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add to.</param>
        /// <param name="configure">The action to configure the task hub builder with.</param>
        /// <returns>The original service collection, with services added.</returns>
        public static IServiceCollection AddTaskHubWorker(
            this IServiceCollection services, Action<ITaskHubWorkerBuilder> configure)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(configure, nameof(configure));

            var builder = new DefaultTaskHubWorkerBuilder(services);
            configure(builder);
            services.TryAddSingleton(sp => builder.Build(sp));

            return services;
        }
    }
}
