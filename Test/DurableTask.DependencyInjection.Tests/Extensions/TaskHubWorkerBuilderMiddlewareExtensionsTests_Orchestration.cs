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
    using DurableTask.Core.Middleware;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using static TestHelpers;

    [TestClass]
    public class TaskHubWorkerBuilderMiddlewareExtensionsTests_Orchestration
    {
        [TestMethod]
        public void UseOrchestrationMiddlewareInstanceNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderMiddlewareExtensions.UseOrchestrationMiddleware(null, new TestMiddleware()));

        [TestMethod]
        public void UseOrchestrationMiddlewareTypeNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderMiddlewareExtensions.UseOrchestrationMiddleware(null, typeof(TestMiddleware)));

        [TestMethod]
        public void UseOrchestrationMiddlewareTypeNullInstance()
            => RunTestException<ArgumentNullException>(
                builder => builder.UseOrchestrationMiddleware((ITaskMiddleware)null));

        [TestMethod]
        public void UseOrchestrationMiddlewareTypeNullType()
            => RunTestException<ArgumentNullException>(
                builder => builder.UseOrchestrationMiddleware((Type)null));

        [TestMethod]
        public void UseOrchestrationMiddlewareGenericNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderMiddlewareExtensions.UseOrchestrationMiddleware<TestMiddleware>(null));

        [TestMethod]
        public void UseOrchestrationMiddlewareFactoryNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderMiddlewareExtensions.UseOrchestrationMiddleware(null, __ => new TestMiddleware()));

        [TestMethod]
        public void UseOrchestrationMiddlewareInstance()
        {
            var instance = new TestMiddleware();
            RunTest(
                builder => builder.UseOrchestrationMiddleware(instance),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.UseOrchestrationMiddleware(IsDescriptor(instance)), Times.Once);
                    mock.VerifyNoOtherCalls();
                });
        }

        [TestMethod]
        public void UseOrchestrationMiddlewareType()
            => RunTest(
                builder => builder.UseOrchestrationMiddleware(typeof(TestMiddleware)),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.UseOrchestrationMiddleware(IsDescriptor(typeof(TestMiddleware))), Times.Once);
                    mock.VerifyNoOtherCalls();
                });

        [TestMethod]
        public void UseOrchestrationMiddlewareGeneric()
            => RunTest(
                builder => builder.UseOrchestrationMiddleware<TestMiddleware>(),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.UseOrchestrationMiddleware(IsDescriptor(typeof(TestMiddleware))), Times.Once);
                    mock.VerifyNoOtherCalls();
                });

        [TestMethod]
        public void UseOrchestrationMiddlewareFactory()
            => RunTest(
                builder => builder.UseOrchestrationMiddleware(_ => new TestMiddleware()),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.UseOrchestrationMiddleware(IsDescriptor(typeof(TestMiddleware))), Times.Once);
                    mock.VerifyNoOtherCalls();
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
            mock.Setup(m => m.UseOrchestrationMiddleware(It.IsAny<TaskMiddlewareDescriptor>())).Returns(mock.Object);

            TResult result = act(mock.Object);
            verify?.Invoke(mock, result);
        }

        private class TestMiddleware : ITaskMiddleware
        {
            public Task InvokeAsync(DispatchMiddlewareContext context, Func<Task> next)
            {
                throw new NotImplementedException();
            }
        }

        private TaskMiddlewareDescriptor IsDescriptor(Type type)
            => Match.Create<TaskMiddlewareDescriptor>(
                descriptor => descriptor.Type == type
                    && descriptor.Descriptor.Lifetime == ServiceLifetime.Transient);

        private TaskMiddlewareDescriptor IsDescriptor(ITaskMiddleware instance)
            => Match.Create<TaskMiddlewareDescriptor>(
                descriptor => descriptor.Type == instance.GetType()
                    && descriptor.Descriptor.ImplementationInstance == instance
                    && descriptor.Descriptor.Lifetime == ServiceLifetime.Singleton);
    }
}
