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
    using System.Collections.Generic;
    using DurableTask.Core;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Holds the <see cref="IServiceScope"/> per orchestration instance execution.
    /// </summary>
    internal static class OrchestrationScope
    {
        private static readonly IDictionary<OrchestrationInstance, IServiceScope> scopes
            = new Dictionary<OrchestrationInstance, IServiceScope>();

        /// <summary>
        /// Gets the current scope for the orchestration instance. Throws if not found.
        /// </summary>
        /// <param name="orchestrationInstance">The orchestration instance. Not null.</param>
        /// <returns>A non-null <see cref="IServiceScope"/>.</returns>
        public static IServiceScope GetScope(OrchestrationInstance orchestrationInstance)
        {
            Check.NotNull(orchestrationInstance, nameof(orchestrationInstance));
            lock (scopes)
            {
                return scopes[orchestrationInstance];
            }
        }

        /// <summary>
        /// Creates a new <see cref="IServiceScope"/> for the orchestration instance.
        /// </summary>
        /// <param name="orchestrationInstance">The orchestration instance. Not null.</param>
        /// <param name="serviceProvider">The service provider. Not null.</param>
        /// <returns></returns>
        public static IServiceScope CreateScope(OrchestrationInstance orchestrationInstance, IServiceProvider serviceProvider)
        {
            Check.NotNull(orchestrationInstance, nameof(orchestrationInstance));
            Check.NotNull(serviceProvider, nameof(serviceProvider));

            lock (scopes)
            {
                if (scopes.ContainsKey(orchestrationInstance))
                {
                    throw new InvalidOperationException($"Scope already exists for orchestration {orchestrationInstance.InstanceId}");
                }


                IServiceScope scope = serviceProvider.CreateScope();
                scopes[orchestrationInstance] = scope;
                return scope;
            }
        }

        /// <summary>
        /// Disposes the <see cref="IServiceScope"/> for the provided orchestration instance, if found.
        /// </summary>
        /// <param name="orchestrationInstance">The orchestration instance, not null.</param>
        public static void DisposeScope(OrchestrationInstance orchestrationInstance)
        {
            Check.NotNull(orchestrationInstance, nameof(orchestrationInstance));

            IServiceScope scope;
            lock (scopes)
            {
                if (scopes.TryGetValue(orchestrationInstance, out scope))
                {
                    scopes.Remove(orchestrationInstance);
                }
            }

            if (scope != null)
            {
                scope.Dispose();
            }
        }
    }
}
