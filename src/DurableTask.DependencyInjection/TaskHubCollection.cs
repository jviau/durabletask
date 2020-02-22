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

    /// <summary>
    /// Contains all task hub instances of the given type.
    /// </summary>
    internal class TaskHubCollection : ITaskObjectCollection
    {
        private readonly HashSet<NamedServiceDescriptorWrapper> descriptors = new HashSet<NamedServiceDescriptorWrapper>();
        private readonly ConcurrentDictionary<TaskVersion, Type> typeMap
            = new ConcurrentDictionary<TaskVersion, Type>();

        /// <inheritdoc />
        public Type this[string taskName, string taskVersion] => GetTaskType(taskName, taskVersion);

        /// <inheritdoc />
        public int Count => this.descriptors.Count;

        /// <inheritdoc />
        public IEnumerator<NamedServiceDescriptorWrapper> GetEnumerator() => this.descriptors.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Adds the descriptor to this collection.
        /// </summary>
        /// <param name="descriptor">The descriptor to add.</param>
        public bool Add(NamedServiceDescriptorWrapper descriptor) => this.descriptors.Add(descriptor);

        private bool IsTaskMatch(string name, string version, NamedServiceDescriptorWrapper descriptor)
        {
            return string.Equals(name, descriptor.Name, StringComparison.Ordinal)
                && string.Equals(version, descriptor.Version, StringComparison.Ordinal);
        }

        private Type GetTaskType(string name, string version)
        {
            var taskVersion = new TaskVersion(name, version);
            if (this.typeMap.TryGetValue(taskVersion, out Type type))
            {
                return type;
            }

            foreach (NamedServiceDescriptorWrapper descriptor in descriptors)
            {
                if (IsTaskMatch(name, version, descriptor))
                {
                    this.typeMap.TryAdd(taskVersion, descriptor.Type);
                    return descriptor.Type;
                }
            }

            return null;
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
