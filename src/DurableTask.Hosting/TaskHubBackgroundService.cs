﻿//  ----------------------------------------------------------------------------------
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


namespace DurableTask.Hosting
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DurableTask.Core;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// A dotnet hosted service for <see cref="TaskHubWorker"/>.
    /// </summary>
    public class TaskHubBackgroundService : BackgroundService
    {
        private readonly TaskHubWorker _worker;

        /// <summary>
        /// Initializes a new instance of <see cref="TaskHubBackgroundService"/>.
        /// </summary>
        /// <param name="worker">The task hub worker.</param>
        public TaskHubBackgroundService(TaskHubWorker worker)
        {
            _worker = worker ?? throw new ArgumentNullException(nameof(worker));
        }

        /// <inheritdoc />
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _worker.StopAsync();
            await base.StopAsync(cancellationToken);
        }

        /// <inheritdoc />
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _worker.StartAsync();
        }
    }
}
