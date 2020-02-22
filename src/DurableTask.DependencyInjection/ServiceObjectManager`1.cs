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
    /// An object manager that gets its items via the service provider.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    internal class ServiceObjectManager<TObject> : INameVersionObjectManager<TObject>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ITaskObjectCollection descriptors;

        /// <summary>
        /// Initializes a new instance of <see cref="ServiceObjectManager{TObject}"/>.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="descriptors">The descriptors of <typeparamref name="TObject"/>.</param>
        public ServiceObjectManager(
            IServiceProvider serviceProvider, ITaskObjectCollection descriptors)
        {
            Check.NotNull(serviceProvider, nameof(serviceProvider));
            Check.NotNull(descriptors, nameof(descriptors));

            this.serviceProvider = serviceProvider;
            this.descriptors = descriptors;
        }

        /// <inheritdoc />
        public void Add(ObjectCreator<TObject> creator)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public TObject GetObject(string name, string version)
        {
            Type type = this.descriptors[name, version];

            if (type == null)
            {
                return default;
            }

            return (TObject)this.serviceProvider.GetRequiredService(type);
        }
    }
}
