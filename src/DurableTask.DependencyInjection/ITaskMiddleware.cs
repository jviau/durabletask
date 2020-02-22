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
    using System.Threading.Tasks;
    using DurableTask.Core.Middleware;

    /// <summary>
    /// Middleware for running in the task hub worker pipeline.
    /// </summary>
    public interface ITaskMiddleware
    {
        /// <summary>
        /// Task hub middleware handling method.
        /// </summary>
        /// <param name="context">The <see cref="DispatchMiddlewareContext"/> context for this pipeline.</param>
        /// <param name="next">The delegate representing the remaining middleware in the pipeline.</param>
        /// <returns>A <see cref="Task"/> that represents the execution of this middleware.</returns>
        Task InvokeAsync(DispatchMiddlewareContext context, Func<Task> next);
    }
}
