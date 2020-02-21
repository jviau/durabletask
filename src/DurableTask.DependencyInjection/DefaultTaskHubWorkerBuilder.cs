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
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DurableTask.Core;
    using DurableTask.Core.Middleware;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    /// <summary>
    /// The default builder for task hub worker.
    /// </summary>
    public class DefaultTaskHubWorkerBuilder : ITaskHubWorkerBuilder
    {
        private readonly TaskHubCollection activities = new TaskHubCollection();
        private readonly TaskHubCollection orchestrations = new TaskHubCollection();
        private readonly List<Type> activitiesMiddleware = new List<Type>();
        private readonly List<Type> orchestrationsMiddleware = new List<Type>();

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultTaskHubWorkerBuilder"/>.
        /// </summary>
        /// <param name="services">The current service collection, not null.</param>
        public DefaultTaskHubWorkerBuilder(IServiceCollection services)
        {
            this.Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <inheritdoc />
        public IServiceCollection Services { get; }

        /// <summary>
        /// Gets or sets the current orchestration service.
        /// </summary>
        public IOrchestrationService OrchestrationService { get; set; }

        /// <inheritdoc />
        public ITaskHubWorkerBuilder AddActivity(TaskActivityDescriptor descriptor)
        {
            this.Services.TryAdd(descriptor.Descriptor);
            this.activities.Add(descriptor);
            return this;
        }

        /// <inheritdoc />
        public ITaskHubWorkerBuilder AddActivityMiddleware(TaskHubMiddlewareDescriptor descriptor)
        {
            Check.NotNull(descriptor, nameof(descriptor));

            this.Services.TryAdd(descriptor.Descriptor);
            this.activitiesMiddleware.Add(descriptor.Type);
            return this;
        }

        /// <inheritdoc />
        public ITaskHubWorkerBuilder AddOrchestration(TaskOrchestrationDescriptor descriptor)
        {
            this.Services.TryAdd(descriptor.Descriptor);
            this.orchestrations.Add(descriptor);
            return this;
        }

        /// <inheritdoc />
        public ITaskHubWorkerBuilder AddOrchestrationMiddleware(TaskHubMiddlewareDescriptor descriptor)
        {
            Check.NotNull(descriptor, nameof(descriptor));

            this.Services.TryAdd(descriptor.Descriptor);
            this.orchestrationsMiddleware.Add(descriptor.Type);
            return this;
        }

        /// <inheritdoc />
        public TaskHubWorker Build(IServiceProvider serviceProvider)
        {
            var worker = new TaskHubWorker(
                this.OrchestrationService,
                new ServiceObjectManager<TaskOrchestration>(serviceProvider, this.orchestrations),
                new ServiceObjectManager<TaskActivity>(serviceProvider, this.activities));

            foreach (Type middlewareType in this.activitiesMiddleware)
            {
                worker.AddActivityDispatcherMiddleware(WrapMiddleware(serviceProvider, middlewareType));
            }

            foreach (Type middlewareType in this.orchestrationsMiddleware)
            {
                worker.AddActivityDispatcherMiddleware(WrapMiddleware(serviceProvider, middlewareType));
            }

            return worker;
        }

        private static Func<DispatchMiddlewareContext, Func<Task>, Task> WrapMiddleware(
            IServiceProvider serviceProvider, Type middlewareType)
        {
            return (context, next) =>
            {
                var middleware = (ITaskHubMiddleware)serviceProvider.GetRequiredService(middlewareType);
                return middleware.InvokeAsync(context, next);
            };
        }
    }
}
