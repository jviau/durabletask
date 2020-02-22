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

namespace DurableTask.DependencyInjection.Tests
{
    using System;
    using System.Threading.Tasks;
    using DurableTask.Core;
    using DurableTask.Core.Middleware;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using static TestHelpers;

    [TestClass]
    public class TaskHubWorkerBuilderTests
    {
        [TestMethod]
        public void CtorArgumentNull()
            => RunTestException<ArgumentNullException>(_ => new DefaultTaskHubWorkerBuilder(null));

        [TestMethod]
        public void AddActivityNull()
            => RunTestException<ArgumentNullException>(b => b.AddActivity(null));

        [TestMethod]
        public void AddActivityMiddlewareNull()
            => RunTestException<ArgumentNullException>(b => b.AddActivityMiddleware(null));

        [TestMethod]
        public void AddOrchestrationNull()
            => RunTestException<ArgumentNullException>(b => b.AddOrchestration(null));

        [TestMethod]
        public void AddOrchestrationMiddlewareNull()
            => RunTestException<ArgumentNullException>(b => b.AddOrchestrationMiddleware(null));

        [TestMethod]
        public void BuildNullServiceProvider()
            => RunTestException<ArgumentNullException>(b => b.Build(null));

        [TestMethod]
        public void BuildOrchestrationNotSet()
            => RunTestException<InvalidOperationException>(b => b.Build(Mock.Of<IServiceProvider>()));

        [TestMethod]
        public void AddActivity()
        {
            TaskActivityDescriptor descriptor = TaskActivityDescriptor.Singleton<TestActivity>();
            RunTest(
                null,
                b => b.AddActivity(descriptor),
                (_, services) => Assert.IsTrue(services.Contains(descriptor.Descriptor)));
        }

        [TestMethod]
        public void AddActivityMiddleware()
        {
            TaskMiddlewareDescriptor descriptor = TaskMiddlewareDescriptor.Singleton<TestMiddleware>();
            RunTest(
                null,
                b => b.AddActivityMiddleware(descriptor),
                (_, services) => Assert.IsTrue(services.Contains(descriptor.Descriptor)));
        }

        [TestMethod]
        public void AddOrchestration()
        {
            TaskOrchestrationDescriptor descriptor = TaskOrchestrationDescriptor.Singleton<TestOrchestration>();
            RunTest(
                null,
                b => b.AddOrchestration(descriptor),
                (_, services) => Assert.IsTrue(services.Contains(descriptor.Descriptor)));
        }

        [TestMethod]
        public void AddOrchestrationMiddleware()
        {
            TaskMiddlewareDescriptor descriptor = TaskMiddlewareDescriptor.Singleton<TestMiddleware>();
            RunTest(
                null,
                b => b.AddOrchestrationMiddleware(descriptor),
                (_, services) => Assert.IsTrue(services.Contains(descriptor.Descriptor)));
        }

        [TestMethod]
        public void BuildTaskHubWorker()
        {
            RunTest(
                null,
                b =>
                {
                    b.OrchestrationService = Mock.Of<IOrchestrationService>();
                    TaskHubWorker worker = b.Build(Mock.Of<IServiceProvider>());
                    Assert.IsNotNull(worker);
                    Assert.AreSame(b.OrchestrationService, worker.orchestrationService);
                },
                null);
        }

        private static void RunTestException<TException>(Action<DefaultTaskHubWorkerBuilder> act)
            where TException : Exception
        {
            TException exception = Capture<TException>(() => RunTest(null, act, null));
            Assert.IsNotNull(exception);
        }

        private static void RunTest(
            Action<IServiceCollection> arrange,
            Action<DefaultTaskHubWorkerBuilder> act,
            Action<DefaultTaskHubWorkerBuilder, IServiceCollection> verify)
        {
            var services = new ServiceCollection();
            arrange?.Invoke(services);
            var builder = new DefaultTaskHubWorkerBuilder(services);

            act(builder);

            verify?.Invoke(builder, services);
        }

        private class TestOrchestration : TaskOrchestration
        {
            public override Task<string> Execute(OrchestrationContext context, string input)
            {
                throw new NotImplementedException();
            }

            public override string GetStatus()
            {
                throw new NotImplementedException();
            }

            public override void RaiseEvent(OrchestrationContext context, string name, string input)
            {
                throw new NotImplementedException();
            }
        }

        private class TestActivity : TaskActivity
        {
            public override string Run(TaskContext context, string input)
            {
                throw new NotImplementedException();
            }
        }

        private class TestMiddleware : ITaskMiddleware
        {
            public Task InvokeAsync(DispatchMiddlewareContext context, Func<Task> next)
            {
                throw new NotImplementedException();
            }
        }
    }
}
