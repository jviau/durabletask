﻿//  ----------------------------------------------------------------------------------
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
        /// Adds the provided descriptor of an activity to the builder.
        /// </summary>
        /// <param name="descriptor">The activity descriptor to add.</param>
        /// <returns></returns>
        ITaskHubWorkerBuilder AddActivity(TaskActivityDescriptor descriptor);

        /// <summary>
        /// Adds the provided descriptor to the builder.
        /// </summary>
        /// <param name="descriptor">The descriptor to add.</param>
        /// <returns></returns>
        ITaskHubWorkerBuilder AddOrchestration(TaskOrchestrationDescriptor descriptor);
    }
}
