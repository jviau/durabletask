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
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A descriptor wrapper for <see cref="ITaskMiddleware"/>.
    /// </summary>
    public sealed class TaskMiddlewareDescriptor : ServiceDescriptorWrapper
    {
        private TaskMiddlewareDescriptor(Type implementationType, ServiceDescriptor descriptor)
            : base(implementationType, descriptor)
        {
        }

        /// <summary>
        /// Creates a singleton middleware descriptor.
        /// </summary>
        /// <param name="type">The concrete <see cref="ITaskMiddleware"/> type.</param>
        /// <returns>A descriptor for a singleton taskhub middleware.</returns>
        public static TaskMiddlewareDescriptor Singleton(Type type)
        {
            Check.NotNull(type, nameof(type));
            Check.ConcreteType<ITaskMiddleware>(type, nameof(type));

            return new TaskMiddlewareDescriptor(type, ServiceDescriptor.Singleton(type));
        }

        /// <summary>
        /// Creates a singleton middleware descriptor.
        /// </summary>
        /// <typeparam name="TMiddleware">The concrete <see cref="ITaskMiddleware"/> type.</typeparam>
        /// <returns>A descriptor for a singleton taskhub middleware.</returns>
        public static TaskMiddlewareDescriptor Singleton<TMiddleware>()
            where TMiddleware : class, ITaskMiddleware
            => Singleton(typeof(TMiddleware));

        /// <summary>
        /// Creates a singleton middleware descriptor.
        /// </summary>
        /// <param name="instance">The implementation instance.</param>
        /// <returns>A descriptor for a singleton taskhub middleware.</returns>
        public static TaskMiddlewareDescriptor Singleton(ITaskMiddleware instance)
        {
            Check.NotNull(instance, nameof(instance));
            Check.ConcreteType<ITaskMiddleware>(instance.GetType(), nameof(instance));

            return new TaskMiddlewareDescriptor(
                instance.GetType(), ServiceDescriptor.Singleton(instance.GetType(), instance));
        }

        /// <summary>
        /// Creates a singleton middleware descriptor.
        /// </summary>
        /// <param name="factory">The factory to produce the singleton middleware.</param>
        /// <returns>A descriptor for a singleton taskhub middleware.</returns>
        public static TaskMiddlewareDescriptor Singleton<TMiddleware>(Func<IServiceProvider, TMiddleware> factory)
            where TMiddleware : class, ITaskMiddleware
        {
            Check.NotNull(factory, nameof(factory));
            Check.ConcreteType<ITaskMiddleware>(typeof(TMiddleware), nameof(TMiddleware));

            return new TaskMiddlewareDescriptor(typeof(TMiddleware), ServiceDescriptor.Singleton(factory));
        }

        /// <summary>
        /// Creates a transient middleware descriptor.
        /// </summary>
        /// <param name="type">The concrete <see cref="ITaskMiddleware"/> type.</param>
        /// <returns>A descriptor for a transient taskhub middleware.</returns>
        public static TaskMiddlewareDescriptor Transient(Type type)
        {
            Check.NotNull(type, nameof(type));
            Check.ConcreteType<ITaskMiddleware>(type, nameof(type));

            return new TaskMiddlewareDescriptor(type, ServiceDescriptor.Transient(type, type));
        }

        /// <summary>
        /// Creates a transient middleware descriptor.
        /// </summary>
        /// <typeparam name="TMiddleware">The concrete <see cref="ITaskMiddleware"/> type.</typeparam>
        /// <returns>A descriptor for a transient taskhub middleware.</returns>
        public static TaskMiddlewareDescriptor Transient<TMiddleware>()
            where TMiddleware : class, ITaskMiddleware
            => Transient(typeof(TMiddleware));

        /// <summary>
        /// Creates a transient middleware descriptor.
        /// </summary>
        /// <param name="factory">The factory to produce the transient middleware.</param>
        /// <returns>A descriptor for a transient taskhub middleware.</returns>
        public static TaskMiddlewareDescriptor Transient<TMiddleware>(Func<IServiceProvider, TMiddleware> factory)
            where TMiddleware : class, ITaskMiddleware
        {
            Check.NotNull(factory, nameof(factory));
            Check.ConcreteType<ITaskMiddleware>(typeof(TMiddleware), nameof(TMiddleware));

            return new TaskMiddlewareDescriptor(typeof(TMiddleware), ServiceDescriptor.Transient(factory));
        }
    }
}
