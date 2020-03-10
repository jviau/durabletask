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

    /// <summary>
    /// Extensions for <see cref="ITaskHubWorkerBuilder"/>.
    /// </summary>
    public static class TaskHubWorkerBuilderMiddlewareExtensions
    {
        /// <summary>
        /// Adds the provided middleware to the activity pipeline as a singleton service.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="middleware">The middleware instance to add.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseActivityMiddleware(
            this ITaskHubWorkerBuilder builder, ITaskMiddleware middleware)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.UseActivityMiddleware(TaskMiddlewareDescriptor.Singleton(middleware));
        }

        /// <summary>
        /// Adds the provided middleware to the activity pipeline as a transient service.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="middlewareType">The type of middleware to use.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseActivityMiddleware(this ITaskHubWorkerBuilder builder, Type middlewareType)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.UseActivityMiddleware(TaskMiddlewareDescriptor.Transient(middlewareType));
        }

        /// <summary>
        /// Adds the provided middleware to the activity pipeline as a transient service.
        /// </summary>
        /// <typeparam name="TMiddleware">The type of middleware to add.</typeparam>
        /// <param name="builder">The builder to add to.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseActivityMiddleware<TMiddleware>(this ITaskHubWorkerBuilder builder)
            where TMiddleware : class, ITaskMiddleware
        {
            Check.NotNull(builder, nameof(builder));
            return builder.UseActivityMiddleware(TaskMiddlewareDescriptor.Transient<TMiddleware>());
        }

        /// <summary>
        /// Adds the provided middleware to the activity pipeline as a transient service.
        /// </summary>
        /// <typeparam name="TMiddleware">The type of middleware to add.</typeparam>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="factory">The factory to create the middleware.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseActivityMiddleware<TMiddleware>(
            this ITaskHubWorkerBuilder builder, Func<IServiceProvider, TMiddleware> factory)
            where TMiddleware : class, ITaskMiddleware
        {
            Check.NotNull(builder, nameof(builder));
            return builder.UseActivityMiddleware(TaskMiddlewareDescriptor.Transient(factory));
        }

        /// <summary>
        /// Adds the provided middleware to the orchestration pipeline as a singleton service.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="middleware">The middleware instance to add.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseOrchestrationMiddleware(
            this ITaskHubWorkerBuilder builder, ITaskMiddleware middleware)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.UseOrchestrationMiddleware(TaskMiddlewareDescriptor.Singleton(middleware));
        }

        /// <summary>
        /// Adds the provided middleware to the orchestration pipeline as a transient service.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="middlewareType">The type mof middleware to use.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseOrchestrationMiddleware(this ITaskHubWorkerBuilder builder, Type middlewareType)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.UseOrchestrationMiddleware(TaskMiddlewareDescriptor.Transient(middlewareType));
        }

        /// <summary>
        /// Adds the provided middleware to the orchestration pipeline as a transient service.
        /// </summary>
        /// <typeparam name="TMiddleware">The type of middleware to add.</typeparam>
        /// <param name="builder">The builder to add to.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseOrchestrationMiddleware<TMiddleware>(this ITaskHubWorkerBuilder builder)
            where TMiddleware : class, ITaskMiddleware
        {
            Check.NotNull(builder, nameof(builder));
            return builder.UseOrchestrationMiddleware(TaskMiddlewareDescriptor.Transient<TMiddleware>());
        }

        /// <summary>
        /// Adds the provided middleware to the orchestration pipeline as a transient service.
        /// </summary>
        /// <typeparam name="TMiddleware">The type of middleware to add.</typeparam>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="factory">The factory to create the middleware.</param>
        /// <returns>The original builder, with middleware added.</returns>
        public static ITaskHubWorkerBuilder UseOrchestrationMiddleware<TMiddleware>(
            this ITaskHubWorkerBuilder builder, Func<IServiceProvider, TMiddleware> factory)
            where TMiddleware : class, ITaskMiddleware
        {
            Check.NotNull(builder, nameof(builder));
            return builder.UseOrchestrationMiddleware(TaskMiddlewareDescriptor.Transient(factory));
        }
    }
}
