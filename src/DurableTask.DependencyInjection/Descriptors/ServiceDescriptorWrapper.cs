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
    /// A wrapper for <see cref="ServiceDescriptor"/> to track the concrete implementation type.
    /// </summary>
    public abstract class ServiceDescriptorWrapper
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ServiceDescriptorWrapper"/>.
        /// </summary>
        /// <param name="type">The service type.</param>
        /// <param name="descriptor">The service descriptor.</param>
        protected ServiceDescriptorWrapper(Type type, ServiceDescriptor descriptor)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(descriptor, nameof(descriptor));

            this.Type = type;
            this.Descriptor = descriptor;
        }

        /// <summary>
        /// Gets the implementation type. Never null.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets the service descriptor being wrapped.
        /// </summary>
        internal ServiceDescriptor Descriptor { get; }
    }
}
