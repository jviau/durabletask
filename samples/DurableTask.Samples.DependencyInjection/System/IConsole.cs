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

namespace System
{
    /// <summary>
    /// Abstraction for <see cref="Console"/>.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// <see cref="Console.ReadLine"/>.
        /// </summary>
        /// <returns>The line input from the console.</returns>
        string ReadLine();

        /// <summary>
        /// <see cref="Console.WriteLine()"/>.
        /// </summary>
        void WriteLine();

        /// <summary>
        /// <see cref="Console.WriteLine(string)"/>.
        /// </summary>
        /// <param name="line">The line to write.</param>
        void WriteLine(string line);
    }
}
