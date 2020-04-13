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

namespace DurableTask.DependencyInjection.Middleware
{
    using System;
    using System.Threading.Tasks;
    using DurableTask.Core;
    using DurableTask.Core.Middleware;
    using DurableTask.DependencyInjection.Activities;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Middleware that constructs and injects the real orchestration or activity at the necessary time.
    /// </summary>
    public class ServiceProviderActivityMiddleware : ITaskMiddleware
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// A middleware that lazily sets the inner orchestration to be ran.
        /// </summary>
        /// <param name="serviceProvider">The service provider. Not null.</param>
        public ServiceProviderActivityMiddleware(IServiceProvider serviceProvider)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));
            this.serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public async Task InvokeAsync(DispatchMiddlewareContext context, Func<Task> next)
        {
            TaskActivity taskActivity = context.GetProperty<TaskActivity>();

            if (taskActivity is WrapperActivity wrapper)
            {
                wrapper.InnerActivity = (TaskActivity)this.serviceProvider
                    .GetRequiredService(wrapper.InnerActivityType);

                await next();
                return;
            }

            await next();
        }
    }
}
