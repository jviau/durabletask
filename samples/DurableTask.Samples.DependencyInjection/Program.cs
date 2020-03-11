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

namespace DurableTask.Samples.DependencyInjection
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DurableTask.Core;
    using DurableTask.DependencyInjection;
    using DurableTask.Emulator;
    using DurableTask.Hosting;
    using DurableTask.Samples.DependencyInjection.Greetings;
    using DurableTask.ServiceBus;
    using DurableTask.ServiceBus.Tracking;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    class Program
    {
        public static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IConsole, ConsoleWrapper>();
                    services.AddHostedService<TaskEnqueuer>();
                })
                .ConfigureTaskHubWorker((context, builder) =>
                {
                    //IOrchestrationService orchestrationService = UseServiceBus(context.Configuration);
                    IOrchestrationService orchestrationService = UseLocalEmulator();

                    builder.OrchestrationService = orchestrationService;

                    // TODO: add a .AddClient(bool add) method to the builder.
                    builder.Services.AddSingleton(new TaskHubClient(orchestrationService as IOrchestrationServiceClient));

                    builder.AddOrchestration<GreetingsOrchestration>();
                    builder
                        .AddActivity<GetUserTask>()
                        .AddActivity<SendGreetingTask>();
                })
                .RunConsoleAsync();
        }

        private static IOrchestrationService UseServiceBus(IConfiguration config)
        {
            string taskHubName = config.GetValue<string>("DurableTask:TaskHubName");
            string azureStorageConnectionString = config.GetValue<string>("DurableTask:AzureStorage:ConnectionString");
            string serviceBusConnectionString = config.GetValue<string>("DurableTask:ServiceBus:ConnectionString");

            IOrchestrationServiceInstanceStore instanceStore =
                new AzureTableInstanceStore(taskHubName, azureStorageConnectionString);

            var orchestrationService =
                new ServiceBusOrchestrationService(
                    serviceBusConnectionString,
                    taskHubName,
                    instanceStore,
                    null,
                    null);

            // TODO: do by default via config
            orchestrationService.CreateIfNotExistsAsync().GetAwaiter().GetResult();

            return orchestrationService;
        }

        private static IOrchestrationService UseLocalEmulator()
            => new LocalOrchestrationService();

        private class TaskEnqueuer : BackgroundService
        {
            private readonly TaskHubClient _client;
            private readonly IConsole _console;
            private readonly string _instanceId = Guid.NewGuid().ToString();

            public TaskEnqueuer(TaskHubClient client, IConsole console)
            {
                _client = client ?? throw new ArgumentNullException(nameof(client));
                _console = console ?? throw new ArgumentNullException(nameof(console));
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                var instance = await _client.CreateOrchestrationInstanceAsync(typeof(GreetingsOrchestration), _instanceId, null);
                var result = await _client.WaitForOrchestrationAsync(instance, TimeSpan.FromSeconds(60));

                _console.WriteLine();
                _console.WriteLine($"Orchestration finished.");
                _console.WriteLine($"Run stats: {result.Status}");
                _console.WriteLine("Press Ctrl+C to exit");
            }
        }
    }
}
