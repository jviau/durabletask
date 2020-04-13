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

namespace DurableTask.DependencyInjection.Activities
{
    using System;
    using System.Threading.Tasks;
    using DurableTask.Core;

    /// <summary>
    /// An orchestration that wraps the real activity type.
    /// </summary>
    internal class WrapperActivity : TaskActivity
    {
        /// <summary>
        /// Initializes a new instance of <see cref="WrapperActivity"/>.
        /// </summary>
        /// <param name="innerActivityType">The inner activity type to use.</param>
        public WrapperActivity(Type innerActivityType)
        {
            Check.NotNull(innerActivityType, nameof(innerActivityType));
            this.InnerActivityType = innerActivityType;
        }

        /// <summary>
        /// Gets the inner activity type.
        /// </summary>
        public Type InnerActivityType { get; }

        /// <summary>
        /// Gets or sets the inner activity.
        /// </summary>
        public TaskActivity InnerActivity { get; set; }

        /// <inheritdoc />
        public override string Run(TaskContext context, string input)
            => this.InnerActivity.Run(context, input);

        /// <inheritdoc />
        public override Task<string> RunAsync(TaskContext context, string input)
            => this.InnerActivity.RunAsync(context, input);
    }
}
