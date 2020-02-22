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
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A descriptor for <see cref="TaskOrchestration"/>.
    /// </summary>
    public sealed class TaskOrchestrationDescriptor : NamedServiceDescriptorWrapper
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of orchestration to describe.</param>
        /// <param name="descriptor">The service descriptor.</param>
        private TaskOrchestrationDescriptor(Type type, ServiceDescriptor descriptor)
            : base(type, descriptor)
        {
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Singleton(Type type)
        {
            Check.NotNull(type, nameof(type));
            Check.ConcreteType<TaskOrchestration>(type, nameof(type));
            return new TaskOrchestrationDescriptor(type, ServiceDescriptor.Singleton(type));
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <typeparam name="TOrchestration">The type of orchestration to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Singleton<TOrchestration>()
            where TOrchestration : TaskOrchestration
            => Singleton(typeof(TOrchestration));

        /// <summary>
        /// Creates a new singleton <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="instance">The task orchestration instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Singleton(TaskOrchestration instance)
        {
            Check.NotNull(instance, nameof(instance));
            Check.ConcreteType<TaskOrchestration>(instance.GetType(), nameof(instance));
            return new TaskOrchestrationDescriptor(instance.GetType(), ServiceDescriptor.Singleton(instance));
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Singleton(string name, string version, Type type)
        {
            TaskOrchestrationDescriptor descriptor = Singleton(type);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <typeparam name="TOrchestration">The type of orchestration to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Singleton<TOrchestration>(string name, string version)
            where TOrchestration : TaskOrchestration
            => Singleton(name, version, typeof(TOrchestration));

        /// <summary>
        /// Creates a new singleton <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="instance">The task orchestration instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Singleton(string name, string version, TaskOrchestration instance)
        {
            TaskOrchestrationDescriptor descriptor = Singleton(instance);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Singleton<TOrchestration>(Func<IServiceProvider, TOrchestration> factory)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(factory, nameof(factory));
            Check.ConcreteType<TaskOrchestration>(typeof(TOrchestration), nameof(TOrchestration));
            return new TaskOrchestrationDescriptor(typeof(TOrchestration), ServiceDescriptor.Singleton(factory));
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Singleton<TOrchestration>(
            string name, string version, Func<IServiceProvider, TOrchestration> factory)
            where TOrchestration : TaskOrchestration
        {
            TaskOrchestrationDescriptor descriptor = Singleton(factory);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new transient <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Transient(Type type)
        {
            Check.NotNull(type, nameof(type));
            Check.ConcreteType<TaskOrchestration>(type, nameof(type));
            return new TaskOrchestrationDescriptor(type, ServiceDescriptor.Transient(type, type));
        }

        /// <summary>
        /// Creates a new transient <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <typeparam name="TOrchestration">The type of orchestration to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Transient<TOrchestration>()
            where TOrchestration : TaskOrchestration
            => Transient(typeof(TOrchestration));

        /// <summary>
        /// Creates a new transient <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Transient(string name, string version, Type type)
        {
            TaskOrchestrationDescriptor descriptor = Transient(type);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new transient <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <typeparam name="TOrchestration">The type of orchestration to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Transient<TOrchestration>(string name, string version)
            where TOrchestration : TaskOrchestration
            => Transient(name, version, typeof(TOrchestration));

        /// <summary>
        /// Creates a new transient <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Transient<TOrchestration>(Func<IServiceProvider, TOrchestration> factory)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(factory, nameof(factory));
            Check.ConcreteType<TaskOrchestration>(typeof(TOrchestration), nameof(TOrchestration));
            return new TaskOrchestrationDescriptor(typeof(TOrchestration), ServiceDescriptor.Transient(factory));
        }

        /// <summary>
        /// Creates a new transient <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Transient<TOrchestration>(
            string name, string version, Func<IServiceProvider, TOrchestration> factory)
            where TOrchestration : TaskOrchestration
        {
            TaskOrchestrationDescriptor descriptor = Transient(factory);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }
    }
}
