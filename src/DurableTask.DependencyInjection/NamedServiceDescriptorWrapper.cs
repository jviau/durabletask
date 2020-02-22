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
    public abstract class NamedServiceDescriptorWrapper : ServiceDescriptorWrapper
    {
        private string name;
        private string version;

        /// <summary>
        /// Initializes a new instance of <see cref="NamedServiceDescriptorWrapper"/>.
        /// </summary>
        /// <param name="type">The service type.</param>
        /// <param name="descriptor">The service descriptor.</param>
        protected NamedServiceDescriptorWrapper(Type type, ServiceDescriptor descriptor)
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
            protected set
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
            protected set
            {
                this.version = value;
            }
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
