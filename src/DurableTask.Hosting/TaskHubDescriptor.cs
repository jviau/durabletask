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

namespace DurableTask.Hosting
{
    using System;
    using DurableTask.Core;

    /// <summary>
    /// Describes a task hub activity or orchestration.
    /// </summary>
    public abstract class TaskHubDescriptor
    {
        private string name;
        private string version;

        internal TaskHubDescriptor(Type type)
        {
            Check.NotNull(type, nameof(type));
            this.Type = type;
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
        /// Gets the task type.
        /// </summary>
        public Type Type { get; }

        internal Func<IServiceProvider, object> Creator { get; set; }

        /// <summary>
        /// Creates a new <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Orchestration(Type type)
        {
            VerifyType<TaskOrchestration>(type);
            return new TaskOrchestrationDescriptor(type);
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
            VerifyType<TaskOrchestration>(type);
            return new TaskOrchestrationDescriptor(type)
            {
                Name = name,
                Version = version,
            };
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
        /// <param name="creator">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Orchestration<TOrchestration>(Func<IServiceProvider, TOrchestration> creator)
            where TOrchestration : TaskOrchestration
        {
            VerifyType<TaskOrchestration>(typeof(TOrchestration));
            return new TaskOrchestrationDescriptor(typeof(TOrchestration))
            {
                Creator = creator,
            };
        }

        /// <summary>
        /// Creates a new <see cref="TaskOrchestrationDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="creator">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskOrchestrationDescriptor Orchestration<TOrchestration>(
            string name, string version, Func<IServiceProvider, TOrchestration> creator)
            where TOrchestration : TaskOrchestration
        {
            VerifyType<TaskOrchestration>(typeof(TOrchestration));
            return new TaskOrchestrationDescriptor(typeof(TOrchestration))
            {
                Name = name,
                Version = version,
                Creator = creator,
            };
        }

        /// <summary>
        /// Creates a new <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="type">The type of the task.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Activity(Type type)
        {
            VerifyType<TaskActivity>(type);
            return new TaskActivityDescriptor(type);
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
            VerifyType<TaskActivity>(type);
            return new TaskActivityDescriptor(type)
            {
                Name = name,
                Version = version,
            };
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
        /// <param name="creator">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Activity<TActivity>(Func<IServiceProvider, TActivity> creator)
            where TActivity : TaskActivity
        {
            VerifyType<TaskActivity>(typeof(TActivity));
            return new TaskActivityDescriptor(typeof(TActivity))
            {
                Creator = creator,
            };
        }

        /// <summary>
        /// Creates a new <see cref="TaskActivityDescriptor"/>.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="version">The version of the task.</param>
        /// <param name="creator">The delegate to create the task instance.</param>
        /// <returns>A new task hub descriptor.</returns>
        public static TaskActivityDescriptor Activity<TActivity>(
            string name, string version, Func<IServiceProvider, TActivity> creator)
            where TActivity : TaskActivity
        {
            VerifyType<TaskActivity>(typeof(TActivity));
            return new TaskActivityDescriptor(typeof(TActivity))
            {
                Name = name,
                Version = version,
                Creator = creator,
            };
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

        private static void VerifyType<TExpected>(Type type)
        {
            Check.NotNull(type, nameof(type));
            if (!typeof(TExpected).IsAssignableFrom(type) || !type.IsClass || type.IsAbstract)
            {
                throw new ArgumentException(
                    $"Task hub type {type} must inherit from {typeof(TExpected)}, be a class, and not be abstract");
            }
        }
    }
}
