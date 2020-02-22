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
    /// A descriptor for <see cref="TaskActivity"/>.
    /// </summary>
    public sealed class TaskActivityDescriptor : NamedServiceDescriptorWrapper
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of activity to describe.</param>
        /// <param name="descriptor">The service descriptor.</param>
        private TaskActivityDescriptor(Type type, ServiceDescriptor descriptor)
            : base(type, descriptor)
        {
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Singleton(Type type)
        {
            Check.NotNull(type, nameof(type));
            Check.ConcreteType<TaskActivity>(type, nameof(type));
            return new TaskActivityDescriptor(type, ServiceDescriptor.Singleton(type));
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <typeparam name="TActivity">The type of activity to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Singleton<TActivity>()
            where TActivity : TaskActivity
            => Singleton(typeof(TActivity));

        /// <summary>
        /// Creates a new singleton <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="instance">The task activity instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Singleton(TaskActivity instance)
        {
            Check.NotNull(instance, nameof(instance));
            Check.ConcreteType<TaskActivity>(instance.GetType(), nameof(instance));
            return new TaskActivityDescriptor(instance.GetType(), ServiceDescriptor.Singleton(instance));
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Singleton(string name, string version, Type type)
        {
            TaskActivityDescriptor descriptor = Singleton(type);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <typeparam name="TActivity">The type of activity to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Singleton<TActivity>(string name, string version)
            where TActivity : TaskActivity
            => Singleton(name, version, typeof(TActivity));

        /// <summary>
        /// Creates a new singleton <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="instance">The task activity instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Singleton(string name, string version, TaskActivity instance)
        {
            TaskActivityDescriptor descriptor = Singleton(instance);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Singleton<TActivity>(Func<IServiceProvider, TActivity> factory)
            where TActivity : TaskActivity
        {
            Check.NotNull(factory, nameof(factory));
            Check.ConcreteType<TaskActivity>(typeof(TActivity), nameof(TActivity));
            return new TaskActivityDescriptor(typeof(TActivity), ServiceDescriptor.Singleton(factory));
        }

        /// <summary>
        /// Creates a new singleton <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Singleton<TActivity>(
            string name, string version, Func<IServiceProvider, TActivity> factory)
            where TActivity : TaskActivity
        {
            TaskActivityDescriptor descriptor = Singleton(factory);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new transient <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Transient(Type type)
        {
            Check.NotNull(type, nameof(type));
            Check.ConcreteType<TaskActivity>(type, nameof(type));
            return new TaskActivityDescriptor(type, ServiceDescriptor.Transient(type, type));
        }

        /// <summary>
        /// Creates a new transient <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <typeparam name="TActivity">The type of activity to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Transient<TActivity>()
            where TActivity : TaskActivity
            => Transient(typeof(TActivity));

        /// <summary>
        /// Creates a new transient <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Transient(string name, string version, Type type)
        {
            TaskActivityDescriptor descriptor = Transient(type);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new transient <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <typeparam name="TActivity">The type of activity to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Transient<TActivity>(string name, string version)
            where TActivity : TaskActivity
            => Transient(name, version, typeof(TActivity));

        /// <summary>
        /// Creates a new transient <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Transient<TActivity>(Func<IServiceProvider, TActivity> factory)
            where TActivity : TaskActivity
        {
            Check.NotNull(factory, nameof(factory));
            Check.ConcreteType<TaskActivity>(typeof(TActivity), nameof(TActivity));
            return new TaskActivityDescriptor(typeof(TActivity), ServiceDescriptor.Transient(factory));
        }

        /// <summary>
        /// Creates a new transient <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Transient<TActivity>(
            string name, string version, Func<IServiceProvider, TActivity> factory)
            where TActivity : TaskActivity
        {
            TaskActivityDescriptor descriptor = Transient(factory);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }
    }
}
