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


namespace DurableTask.DependencyInjection.Tests.Extensions
{
    using System;
    using DurableTask.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using static TestHelpers;

    [TestClass]
    public class TaskHubWorkerBuilderExtensionsTests
    {
        [TestMethod]
        public void WithOrchestrationServiceNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderExtensions.WithOrchestrationService(null, Mock.Of<IOrchestrationService>()));

        [TestMethod]
        public void WithOrchestrationServiceIsSet()
            => RunTest(
                builder =>
                {
                    IOrchestrationService service = Mock.Of<IOrchestrationService>();
                    var returned = builder.WithOrchestrationService(service);

                    Assert.IsNotNull(returned);
                    Assert.AreSame(returned, builder);
                    return service;
                },
                (mock, service) =>
                {
                    mock.VerifySet(m => m.OrchestrationService = service, Times.Once);
                });

        private static void RunTestException<TException>(Action<ITaskHubWorkerBuilder> act)
            where TException : Exception
        {
            bool Act(ITaskHubWorkerBuilder builder)
            {
                act(builder);
                return true;
            }

            TException exception = Capture<TException>(() => RunTest(Act, null));
            Assert.IsNotNull(exception);
        }

        private static void RunTest<TResult>(
            Func<ITaskHubWorkerBuilder, TResult> act,
            Action<Mock<ITaskHubWorkerBuilder>, TResult> verify)
        {
            var mock = new Mock<ITaskHubWorkerBuilder>();
            TResult result = act(mock.Object);
            verify?.Invoke(mock, result);
        }
    }
}
