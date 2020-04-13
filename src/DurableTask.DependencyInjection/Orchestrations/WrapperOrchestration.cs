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

namespace DurableTask.DependencyInjection.Orchestrations
{
    using System;
    using System.Threading.Tasks;
    using DurableTask.Core;

    /// <summary>
    /// An orchestration that wraps the real orchestration type.
    /// </summary>
    internal class WrapperOrchestration : TaskOrchestration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="WrapperOrchestration"/>.
        /// </summary>
        /// <param name="innerOrchestrationType">The inner orchestration type to use.</param>
        public WrapperOrchestration(Type innerOrchestrationType)
        {
            Check.NotNull(innerOrchestrationType, nameof(innerOrchestrationType));
            this.InnerOrchestrationType = innerOrchestrationType;
        }

        /// <summary>
        /// Gets the inner orchestrations type.
        /// </summary>
        public Type InnerOrchestrationType { get; }

        /// <summary>
        /// Gets or sets the inner orchestration.
        /// </summary>
        public TaskOrchestration InnerOrchestration { get; set; }

        /// <inheritdoc />
        public override async Task<string> Execute(OrchestrationContext context, string input)
        {
            if (this.InnerOrchestration == null)
            {
                throw new InvalidOperationException($"{this.InnerOrchestration} not set!");
            }

            try
            {
                return await this.InnerOrchestration.Execute(context, input).ConfigureAwait(false);
            }
            finally
            {
                await OrchestrationScope.SafeDisposeScopeAsync(context.OrchestrationInstance)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public override string GetStatus()
            => this.InnerOrchestration.GetStatus();

        /// <inheritdoc />
        public override void RaiseEvent(OrchestrationContext context, string name, string input)
            => this.InnerOrchestration.RaiseEvent(context, name, input);
    }
}
