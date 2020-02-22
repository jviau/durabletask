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

namespace DurableTask.DependencyInjection
{
    using DurableTask.Core;

    /// <summary>
    /// Extensions for <see cref="ITaskHubWorkerBuilder"/>.
    /// </summary>
    public static class TaskHubWorkerBuilderExtensions
    {
        /// <summary>
        /// Sets the provided <paramref name="orchestrationService"/> to the 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="orchestrationService"></param>
        /// <returns></returns>
        public static ITaskHubWorkerBuilder WithOrchestrationService(
            this ITaskHubWorkerBuilder builder, IOrchestrationService orchestrationService)
        {
            Check.NotNull(builder, nameof(builder));
            builder.OrchestrationService = orchestrationService;
            return builder;
        }
    }
}
