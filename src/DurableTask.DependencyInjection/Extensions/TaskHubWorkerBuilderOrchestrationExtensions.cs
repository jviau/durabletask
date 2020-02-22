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

    /// <summary>
    /// Extensions for <see cref="ITaskHubWorkerBuilder"/>.
    /// </summary>
    public static class TaskHubWorkerBuilderOrchestrationExtensions
    {
        /// <summary>
        /// Adds the supplied orchestration type to the builder as a transient service.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="type">The orchestration type to add.</param>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration(this ITaskHubWorkerBuilder builder, Type type)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskOrchestrationDescriptor.Transient(type));
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder as a transient service.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <typeparam name="TOrchestration">The task orchestration type to add.</typeparam>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration<TOrchestration>(this ITaskHubWorkerBuilder builder)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskOrchestrationDescriptor.Transient<TOrchestration>());
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder as a transient service.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="name">The name of the task to add.</param>
        /// <param name="version">The version of the task to add.</param>
        /// <param name="type">The orchestration type to add.</param>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration(
            this ITaskHubWorkerBuilder builder, string name, string version, Type type)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskOrchestrationDescriptor.Transient(name, version, type));
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder as a transient service.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="name">The name of the task to add.</param>
        /// <param name="version">The version of the task to add.</param>
        /// <typeparam name="TOrchestration">The task orchestration type to add.</typeparam>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration<TOrchestration>(
            this ITaskHubWorkerBuilder builder, string name, string version)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskOrchestrationDescriptor.Transient<TOrchestration>(name, version));
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder as a transient service.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="name">The name of the task to add.</param>
        /// <param name="version">The version of the task to add.</param>
        /// <param name="creator">The delegate to create the task orchestration.</param>
        /// <typeparam name="TOrchestration">The task orchestration type to add.</typeparam>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration<TOrchestration>(
            this ITaskHubWorkerBuilder builder, string name, string version, Func<IServiceProvider, TOrchestration> creator)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskOrchestrationDescriptor.Transient(name, version, creator));
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder as a transient service.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="creator">The delegate to create the task orchestration.</param>
        /// <typeparam name="TOrchestration">The task orchestration type to add.</typeparam>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration<TOrchestration>(
            this ITaskHubWorkerBuilder builder, Func<IServiceProvider, TOrchestration> creator)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskOrchestrationDescriptor.Transient(creator));
        }
    }
}
