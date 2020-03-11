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

namespace DurableTask.Samples.DependencyInjection.Greetings
{
    using System;
    using DurableTask.Core;

    /// <summary>
    /// A task activity for getting a username from console.
    /// </summary>
    public class GetUserTask : TaskActivity<string, string>
    {
        private readonly IConsole _console;

        /// <summary>
        /// Initializes a new instance of <see cref="GetUserTask"/>.
        /// </summary>
        /// <param name="console">The console output helper.</param>
        public GetUserTask(IConsole console)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
        }

        /// <inheritdoc />
        protected override string Execute(TaskContext context, string input)
        {
            _console.WriteLine("Please enter your name:");
            return _console.ReadLine();
        }
    }
}
