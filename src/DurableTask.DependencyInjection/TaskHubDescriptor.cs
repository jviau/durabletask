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
    /// Describes a task hub activity or orchestration.
    /// </summary>
    public abstract class TaskHubDescriptor : ServiceDescriptorWrapper
    {
        private string name;
        private string version;

        internal TaskHubDescriptor(Type type, ServiceDescriptor descriptor)
            : base(type, descriptor)
        {
        }

        /// <summary>
        /// Gets the task name.
        /// </summary>
        public string Name
        {
            get
            {
                Initialize();
                return name;
            }
            private set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets the task version.
        /// </summary>
        public string Version
        {
            get
            {
                Initialize();
                return version;
            }
            private set
            {
                this.version = value;
            }
        }

        /// <summary>
        /// Creates a new <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Orchestration(Type type)
        {
            Check.NotNull(type, nameof(type));
            Check.ConcreteType<TaskOrchestration>(type, nameof(type));
            return new TaskOrchestrationDescriptor(type, ServiceDescriptor.Transient(type, type));
        }

        /// <summary>
        /// Creates a new <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <typeparam name="TOrchestration">The type of orchestration to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Orchestration<TOrchestration>()
            where TOrchestration : TaskOrchestration
            => Orchestration(typeof(TOrchestration));

        /// <summary>
        /// Creates a new <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Orchestration(string name, string version, Type type)
        {
            TaskOrchestrationDescriptor descriptor = Orchestration(type);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <typeparam name="TOrchestration">The type of orchestration to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Orchestration<TOrchestration>(string name, string version)
            where TOrchestration : TaskOrchestration
            => Orchestration(name, version, typeof(TOrchestration));

        /// <summary>
        /// Creates a new <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Orchestration<TOrchestration>(Func<IServiceProvider, TOrchestration> factory)
            where TOrchestration : TaskOrchestration
        {
            Check.ConcreteType<TaskOrchestration>(typeof(TOrchestration), nameof(TOrchestration));
            return new TaskOrchestrationDescriptor(typeof(TOrchestration), ServiceDescriptor.Transient(factory));
        }

        /// <summary>
        /// Creates a new <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Orchestration<TOrchestration>(
            string name, string version, Func<IServiceProvider, TOrchestration> factory)
            where TOrchestration : TaskOrchestration
        {
            TaskOrchestrationDescriptor descriptor = Orchestration(factory);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Activity(Type type)
        {
            Check.NotNull(type, nameof(type));
            Check.ConcreteType<TaskActivity>(type, nameof(type));
            return new TaskActivityDescriptor(type, ServiceDescriptor.Transient(type, type));
        }

        /// <summary>
        /// Creates a new <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <typeparam name="TActivity">The type of Activity to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Activity<TActivity>()
            where TActivity : TaskActivity
            => Activity(typeof(TActivity));

        /// <summary>
        /// Creates a new <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Activity(string name, string version, Type type)
        {
            TaskActivityDescriptor descriptor = Activity(type);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        /// <summary>
        /// Creates a new <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <typeparam name="TActivity">The type of Activity to create.</typeparam>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Activity<TActivity>(string name, string version)
            where TActivity : TaskActivity
            => Activity(name, version, typeof(TActivity));

        /// <summary>
        /// Creates a new <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Activity<TActivity>(Func<IServiceProvider, TActivity> factory)
            where TActivity : TaskActivity
        {
            Check.ConcreteType<TaskActivity>(typeof(TActivity), nameof(TActivity));
            return new TaskActivityDescriptor(typeof(TActivity), ServiceDescriptor.Transient(factory));
        }

        /// <summary>
        /// Creates a new <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="factory">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Activity<TActivity>(
            string name, string version, Func<IServiceProvider, TActivity> factory)
            where TActivity : TaskActivity
        {
            TaskActivityDescriptor descriptor = Activity(factory);
            descriptor.Name = name;
            descriptor.Version = version;
            return descriptor;
        }

        private void Initialize()
        {
            if (this.name == null)
            {
                this.name = NameVersionHelper.GetDefaultName(this.Type);
            }

            if (this.version == null)
            {
                this.version = NameVersionHelper.GetDefaultVersion(this.Type);
            }
        }
    }
}
