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
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Holds the <see cref="IServiceScope"/> per orchestration instance execution.
    /// </summary>
    internal class OrchestrationScope : IOrchestrationScope
    {
        private static readonly IDictionary<OrchestrationInstance, IOrchestrationScope> scopes
            = new Dictionary<OrchestrationInstance, IOrchestrationScope>();

        private readonly IServiceScope innerScope;
        private readonly TaskCompletionSource<bool> middlewareCompleted
            = new TaskCompletionSource<bool>();

        private OrchestrationScope(IServiceScope scope)
        {
            this.innerScope = Check.NotNull(scope, nameof(scope));
        }

        public IServiceProvider ServiceProvider => this.innerScope.ServiceProvider;

        /// <summary>
        /// Gets the current scope for the orchestration instance. Throws if not found.
        /// </summary>
        /// <param name="orchestrationInstance">The orchestration instance. Not null.</param>
        /// <returns>A non-null <see cref="IOrchestrationScope"/>.</returns>
        public static IOrchestrationScope GetScope(OrchestrationInstance orchestrationInstance)
        {
            Check.NotNull(orchestrationInstance, nameof(orchestrationInstance));
            lock (scopes)
            {
                return scopes[orchestrationInstance];
            }
        }

        /// <summary>
        /// Creates a new <see cref="IOrchestrationScope"/> for the orchestration instance.
        /// </summary>
        /// <param name="orchestrationInstance">The orchestration instance. Not null.</param>
        /// <param name="serviceProvider">The service provider. Not null.</param>
        /// <returns></returns>
        public static IOrchestrationScope CreateScope(
            OrchestrationInstance orchestrationInstance, IServiceProvider serviceProvider)
        {
            Check.NotNull(orchestrationInstance, nameof(orchestrationInstance));
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            lock (scopes)
            {
                if (scopes.ContainsKey(orchestrationInstance))
                {
                    throw new InvalidOperationException(
                        $"Scope already exists for orchestration {orchestrationInstance.InstanceId}");
                }


                IOrchestrationScope scope = new OrchestrationScope(serviceProvider.CreateScope());
                scopes[orchestrationInstance] = scope;
                return scope;
            }
        }

        /// <summary>
        /// Waits for middleware completion then disposes the <see cref="IOrchestrationScope"/>
        /// for the provided orchestration instance, if found.
        /// </summary>
        /// <param name="orchestrationInstance">The orchestration instance, not null.</param>
        /// <returns>A task that completes when the orchestration scope is disposed.</returns>
        public static async Task SafeDisposeScopeAsync(OrchestrationInstance orchestrationInstance)
        {
            Check.NotNull(orchestrationInstance, nameof(orchestrationInstance));

            IOrchestrationScope scope;
            lock (scopes)
            {
                if (scopes.TryGetValue(orchestrationInstance, out scope))
                {
                    scopes.Remove(orchestrationInstance);
                }
            }

            if (scope != null)
            {
                await scope.WaitForMiddlewareCompletionAsync().ConfigureAwait(false);
                scope.Dispose();
            }
        }

        /// <inheritdoc />
        public void SignalMiddlewareCompletion() => this.middlewareCompleted.TrySetResult(true);

        /// <inheritdoc />
        public Task WaitForMiddlewareCompletionAsync() => this.middlewareCompleted.Task;

        /// <inheritdoc />
        public void Dispose()
        {
            this.innerScope.Dispose();

            if (!this.middlewareCompleted.Task.IsCompleted)
            {
                this.middlewareCompleted.TrySetCanceled();
            }
        }
    }
}
