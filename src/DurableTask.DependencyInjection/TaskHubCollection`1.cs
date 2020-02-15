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
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Contains all task hub instances of the given type.
    /// </summary>
    /// <typeparam name="TObject">The type to track.</typeparam>
    public class TaskHubCollection<TObject> : ITaskObjectCollection<TObject>
    {
        private readonly HashSet<Type> types = new HashSet<Type>();
        private readonly ConcurrentDictionary<TaskVersion, Type> typeMap
            = new ConcurrentDictionary<TaskVersion, Type>();

        private IServiceCollection serviceDescriptors;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskHubCollection{TService}"/>.
        /// Takes a one-time snapshow of <paramref name="serviceDescriptors"/> on first access,
        /// then de-references it.
        /// </summary>
        /// <param name="serviceDescriptors"></param>
        public TaskHubCollection(IServiceCollection serviceDescriptors)
        {
            this.serviceDescriptors = serviceDescriptors ?? throw new ArgumentNullException(nameof(serviceDescriptors));
        }

        /// <inheritdoc />
        public Type this[string taskName, string taskVersion] => throw new NotImplementedException();

        /// <inheritdoc />
        public int Count => this.Types.Count;

        private HashSet<Type> Types
        {
            get
            {
                Initialize();
                return this.types;
            }
        }

        /// <inheritdoc />
        public IEnumerator<Task> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private Type GetTaskType(TaskVersion taskVersion)
        {
            if (this.typeMap.TryGetValue(taskVersion, out Type type))
            {
                return type;
            }

            foreach (Type taskType in Types)
            {

            }
        }

        private void Initialize()
        {
            if (this.serviceDescriptors == null)
            {
                return;
            }

            lock (types)
            {
                if (this.serviceDescriptors == null)
                {
                    return;
                }

                IEnumerable<Type> types = serviceDescriptors
                    .Where(sd => typeof(TObject).IsAssignableFrom(sd.ServiceType))
                    .Select(sd => sd.GetImplementationType());

                this.types.UnionWith(types);
                this.serviceDescriptors = null;
            }
        }

        private readonly struct TaskVersion : IEquatable<TaskVersion>
        {
            public TaskVersion(string name, string version)
            {
                Name = name;
                Version = version;
            }

            public string Name { get; }

            public string Version { get; }

            public bool Equals(TaskVersion other)
            {
                return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(Version, other.Version, StringComparison.OrdinalIgnoreCase);
            }

            public override bool Equals(object obj)
            {
                if (obj is TaskVersion other)
                {
                    return Equals(other);
                }

                return false;
            }

            public override int GetHashCode()
            {
                string n = Name ?? string.Empty;
                string v = Version ?? string.Empty;

                return n.ToUpper().GetHashCode() ^ v.ToUpper().GetHashCode();
            }
        }
    }
}
