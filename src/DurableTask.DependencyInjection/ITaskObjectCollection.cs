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
    using System.Collections.Generic;

    /// <summary>
    /// The collection of task types.
    /// </summary>
    internal interface ITaskObjectCollection : IReadOnlyCollection<NamedServiceDescriptorWrapper>
    {
        /// <summary>
        /// Gets the task object identified by <paramref name="taskName"/> and <paramref name="taskVersion"/>.
        /// </summary>
        Type this[string taskName, string taskVersion] { get; }
    }
}
