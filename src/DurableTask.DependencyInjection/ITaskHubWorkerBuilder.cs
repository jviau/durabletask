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
    using DurableTask.Core;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A builder for hosting a durable task worker.
    /// </summary>
    public interface ITaskHubWorkerBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where durable task services are configured.
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// Gets or sets the <see cref="IOrchestrationService"/> to use.
        /// </summary>
        IOrchestrationService OrchestrationService { get; set; }

        /// <summary>
        /// Adds the provided descriptor of an activity to the builder.
        /// </summary>
        /// <param name="descriptor">The activity descriptor to add.</param>
        /// <returns>This instance, for chaining calls</returns>
        ITaskHubWorkerBuilder AddActivity(TaskActivityDescriptor descriptor);

        /// <summary>
        /// Adds the provided middleware for task activities.
        /// </summary>
        /// <param name="descriptor">The middleware descriptor to add.</param>
        /// <returns>This instance, for chaining calls</returns>
        ITaskHubWorkerBuilder AddActivityMiddleware(TaskMiddlewareDescriptor descriptor);

        /// <summary>
        /// Adds the provided descriptor to the builder.
        /// </summary>
        /// <param name="descriptor">The descriptor to add.</param>
        /// <returns>This instance, for chaining calls</returns>
        ITaskHubWorkerBuilder AddOrchestration(TaskOrchestrationDescriptor descriptor);

        /// <summary>
        /// Adds the provided middleware for task orchestrations.
        /// </summary>
        /// <param name="descriptor">The middleware descriptor to add.</param>
        /// <returns>This instance, for chaining calls</returns>
        ITaskHubWorkerBuilder AddOrchestrationMiddleware(TaskMiddlewareDescriptor descriptor);

    }
}
