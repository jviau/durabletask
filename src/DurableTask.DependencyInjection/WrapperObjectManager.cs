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

    /// <summary>
    /// An object manager that gets its items via the service provider.
    /// </summary>
    /// <typeparam name="TObject">The type of object to create.</typeparam>
    internal class WrapperObjectManager<TObject> : INameVersionObjectManager<TObject>
    {
        private readonly ITaskObjectCollection descriptors;
        private readonly Func<Type, TObject> factory;

        /// <summary>
        /// Initializes a new instance of <see cref="WrapperObjectManager{TObject}"/>.
        /// </summary>
        /// <param name="descriptors">The descriptors of.</param>
        /// <param name="factory">The factory function for creating the wrapper.</param>
        public WrapperObjectManager(ITaskObjectCollection descriptors, Func<Type, TObject> factory)
        {
            this.descriptors = Check.NotNull(descriptors, nameof(descriptors));
            this.factory = Check.NotNull(factory, nameof(factory));
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

            return this.factory(type);
        }
    }
}
