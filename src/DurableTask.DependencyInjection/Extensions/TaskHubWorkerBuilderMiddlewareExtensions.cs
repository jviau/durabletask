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

using System;

namespace DurableTask.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="ITaskHubWorkerBuilder"/>.
    /// </summary>
    public static class TaskHubWorkerBuilderMiddlewareExtensions
    {
        /// <summary>
        /// Adds the provided middleware to the activity pipeline.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="middleware">The middleware instance to add.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseActivityMiddleware(
            this ITaskHubWorkerBuilder builder, ITaskHubMiddleware middleware)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddActivityMiddleware(TaskHubMiddlewareDescriptor.Singleton(middleware));
        }

        /// <summary>
        /// Adds the provided middleware to the activity pipeline.
        /// </summary>
        /// <typeparam name="TMiddleware">The type of middleware to add.</typeparam>
        /// <param name="builder">The builder to add to.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseActivityMiddleware<TMiddleware>(this ITaskHubWorkerBuilder builder)
            where TMiddleware : class, ITaskHubMiddleware
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddActivityMiddleware(TaskHubMiddlewareDescriptor.Transient<TMiddleware>());
        }

        /// <summary>
        /// Adds the provided middleware to the activity pipeline.
        /// </summary>
        /// <typeparam name="TMiddleware">The type of middleware to add.</typeparam>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="factory">The factory to create the middleware.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseActivityMiddleware<TMiddleware>(
            this ITaskHubWorkerBuilder builder, Func<IServiceProvider, TMiddleware> factory)
            where TMiddleware : class, ITaskHubMiddleware
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddActivityMiddleware(TaskHubMiddlewareDescriptor.Transient(factory));
        }

        /// <summary>
        /// Adds the provided middleware to the orchestration pipeline.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="middleware">The middleware instance to add.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseOrchestrationMiddleware(
            this ITaskHubWorkerBuilder builder, ITaskHubMiddleware middleware)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestrationMiddleware(TaskHubMiddlewareDescriptor.Singleton(middleware));
        }

        /// <summary>
        /// Adds the provided middleware to the orchestration pipeline.
        /// </summary>
        /// <typeparam name="TMiddleware">The type of middleware to add.</typeparam>
        /// <param name="builder">The builder to add to.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseOrchestrationMiddleware<TMiddleware>(this ITaskHubWorkerBuilder builder)
            where TMiddleware : class, ITaskHubMiddleware
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestrationMiddleware(TaskHubMiddlewareDescriptor.Transient<TMiddleware>());
        }

        /// <summary>
        /// Adds the provided middleware to the orchestration pipeline.
        /// </summary>
        /// <typeparam name="TMiddleware">The type of middleware to add.</typeparam>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="factory">The factory to create the middleware.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseOrchestrationMiddleware<TMiddleware>(
            this ITaskHubWorkerBuilder builder, Func<IServiceProvider, TMiddleware> factory)
            where TMiddleware : class, ITaskHubMiddleware
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestrationMiddleware(TaskHubMiddlewareDescriptor.Transient(factory));
        }
    }
}
