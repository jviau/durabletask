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
    public class TaskHubWorkerBuilderMiddlewareExtensionsTests_Activity
    {
        [TestMethod]
        public void UseActivityMiddlewareInstanceNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderMiddlewareExtensions.UseActivityMiddleware(null, new TestMiddleware()));

        [TestMethod]
        public void UseActivityMiddlewareTypeNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderMiddlewareExtensions.UseActivityMiddleware(null, typeof(TestMiddleware)));

        [TestMethod]
        public void UseActivityMiddlewareTypeNullInstance()
            => RunTestException<ArgumentNullException>(
                builder => builder.UseActivityMiddleware((ITaskMiddleware)null));

        [TestMethod]
        public void UseActivityMiddlewareTypeNullType()
            => RunTestException<ArgumentNullException>(
                builder => builder.UseActivityMiddleware((Type)null));

        [TestMethod]
        public void UseActivityMiddlewareGenericNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderMiddlewareExtensions.UseActivityMiddleware<TestMiddleware>(null));

        [TestMethod]
        public void UseActivityMiddlewareFactoryNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderMiddlewareExtensions.UseActivityMiddleware(null, __ => new TestMiddleware()));

        [TestMethod]
        public void UseActivityMiddlewareInstance()
        {
            var instance = new TestMiddleware();
            RunTest(
                builder => builder.UseActivityMiddleware(instance),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.UseActivityMiddleware(IsDescriptor(instance)), Times.Once);
                    mock.VerifyNoOtherCalls();
                });
        }

        [TestMethod]
        public void UseActivityMiddlewareType()
            => RunTest(
                builder => builder.UseActivityMiddleware(typeof(TestMiddleware)),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.UseActivityMiddleware(IsDescriptor(typeof(TestMiddleware))), Times.Once);
                    mock.VerifyNoOtherCalls();
                });

        [TestMethod]
        public void UseActivityMiddlewareGeneric()
            => RunTest(
                builder => builder.UseActivityMiddleware<TestMiddleware>(),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.UseActivityMiddleware(IsDescriptor(typeof(TestMiddleware))), Times.Once);
                    mock.VerifyNoOtherCalls();
                });

        [TestMethod]
        public void UseActivityMiddlewareFactory()
            => RunTest(
                builder => builder.UseActivityMiddleware(_ => new TestMiddleware()),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.UseActivityMiddleware(IsDescriptor(typeof(TestMiddleware))), Times.Once);
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
            mock.Setup(m => m.UseActivityMiddleware(It.IsAny<TaskMiddlewareDescriptor>())).Returns(mock.Object);

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
