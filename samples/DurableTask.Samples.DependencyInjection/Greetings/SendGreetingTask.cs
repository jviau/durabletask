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

namespace DurableTask.Samples.DependencyInjection.Greetings
{
    using System;
    using System.Threading.Tasks;
    using DurableTask.Core;

    /// <summary>
    /// A task for sending a greeting.
    /// </summary>
    public sealed class SendGreetingTask : AsyncTaskActivity<string, string>
    {
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of <see cref="SendGreetingTask"/>.
        /// </summary>
        /// <param name="console">The console output helper.</param>
        public SendGreetingTask(IConsole console)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
        }

        /// <inheritdoc />
        protected override async Task<string> ExecuteAsync(TaskContext context, string user)
        {
            string message;
            if (!string.IsNullOrWhiteSpace(user) && user.Equals("TimedOut"))
            {
                message = "GetUser Timed out!!!";
                _console.WriteLine(message);
            }
            else
            {
                _console.WriteLine("Sending greetings to user: " + user + "...");

                await Task.Delay(5 * 1000);

                message = "Greeting sent to " + user;
                _console.WriteLine(message);
            }

            return message;
        }
    }
}