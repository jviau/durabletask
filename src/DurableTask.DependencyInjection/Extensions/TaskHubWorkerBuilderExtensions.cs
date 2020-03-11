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
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// Extensions for <see cref="ITaskHubWorkerBuilder"/>.
    /// </summary>
    public static class TaskHubWorkerBuilderExtensions
    {
        /// <summary>
        /// Sets the provided <paramref name="orchestrationService"/> to the 
        /// </summary>
        /// <param name="builder">The task hub builder.</param>
        /// <param name="orchestrationService">The orchestration service to use.</param>
        /// <returns>The original builder, with orchestration service set.</returns>
        public static ITaskHubWorkerBuilder WithOrchestrationService(
            this ITaskHubWorkerBuilder builder, IOrchestrationService orchestrationService)
        {
            Check.NotNull(builder, nameof(builder));
            builder.OrchestrationService = orchestrationService;
            return builder;
        }

        /// <summary>
        /// Adds <see cref="TaskHubClient"/> to the service collection.
        /// </summary>
        /// <param name="builder">The builder to add the client from.</param>
        /// <returns>The original builder, with <see cref="TaskHubClient"/> added to the service collection.</returns>
        public static ITaskHubWorkerBuilder AddClient(this ITaskHubWorkerBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            builder.Services.TryAddSingleton(_ => ClientFactory(builder));
            return builder;
        }

        private static TaskHubClient ClientFactory(ITaskHubWorkerBuilder builder)
        {
            if (builder.OrchestrationService == null)
            {
                throw new InvalidOperationException($"OrchestrationService not set on ITaskHubWorkerBuilder.");
            }

            if (builder.OrchestrationService is IOrchestrationServiceClient client)
            {
                return new TaskHubClient(client);
            }

            throw new InvalidOperationException(
                $"Failed to add TaskHubClient. " +
                $"{builder.OrchestrationService.GetType()} does not implement {typeof(IOrchestrationServiceClient)}.");
        }
    }
}
