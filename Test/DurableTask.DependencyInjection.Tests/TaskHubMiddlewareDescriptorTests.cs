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

    [TestClass]
    public class TaskHubMiddlewareDescriptorTests
    {
        [TestMethod]
        public void SingletonByType()
            => RunTest<TestMiddleware>(
                () => TaskHubMiddlewareDescriptor.Singleton(typeof(TestMiddleware)),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByTypeNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskHubMiddlewareDescriptor.Singleton((Type)null));

        [TestMethod]
        public void SingleByTypeAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskHubMiddlewareDescriptor.Singleton(typeof(AbstractMiddleware)));

        [TestMethod]
        public void SingletonByTypeParam()
            => RunTest<TestMiddleware>(
                () => TaskHubMiddlewareDescriptor.Singleton<TestMiddleware>(),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByTypeParamAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskHubMiddlewareDescriptor.Singleton<AbstractMiddleware>());

        [TestMethod]
        public void SingletonByInstance()
            => RunTest<TestMiddleware>(
                () => TaskHubMiddlewareDescriptor.Singleton(new TestMiddleware()),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByInstanceNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskHubMiddlewareDescriptor.Singleton((ITaskHubMiddleware)null));

        [TestMethod]
        public void SingletonByFactory()
            => RunTest<TestMiddleware>(
                () => TaskHubMiddlewareDescriptor.Singleton(_ => new TestMiddleware()),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByFactoryNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskHubMiddlewareDescriptor.Singleton((Func<IServiceProvider, ITaskHubMiddleware>)null));

        [TestMethod]
        public void SingleByFactoryAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskHubMiddlewareDescriptor.Singleton(_ => new TestMiddleware() as AbstractMiddleware));

        [TestMethod]
        public void TransientByType()
            => RunTest<TestMiddleware>(
                () => TaskHubMiddlewareDescriptor.Transient(typeof(TestMiddleware)),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByTypeNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskHubMiddlewareDescriptor.Transient((Type)null));

        [TestMethod]
        public void TransientByTypeAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskHubMiddlewareDescriptor.Transient(typeof(AbstractMiddleware)));

        [TestMethod]
        public void TransientByTypeParam()
            => RunTest<TestMiddleware>(
                () => TaskHubMiddlewareDescriptor.Transient<TestMiddleware>(),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByTypeParamAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskHubMiddlewareDescriptor.Transient<AbstractMiddleware>());

        [TestMethod]
        public void TransientByFactory()
            => RunTest<TestMiddleware>(
                () => TaskHubMiddlewareDescriptor.Transient(_ => new TestMiddleware()),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByFactoryNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskHubMiddlewareDescriptor.Transient((Func<IServiceProvider, ITaskHubMiddleware>)null));

        [TestMethod]
        public void TransientByFactoryAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskHubMiddlewareDescriptor.Transient(_ => new TestMiddleware() as AbstractMiddleware));

        private void RunTest<TMiddleware>(Func<TaskHubMiddlewareDescriptor> test, ServiceLifetime serviceLifetime)
        {
            TaskHubMiddlewareDescriptor descriptor = test();

            Assert.IsNotNull(descriptor);
            Assert.AreEqual(typeof(TMiddleware), descriptor.Type);
            Assert.IsNotNull(descriptor.Descriptor);
            Assert.AreEqual(serviceLifetime, descriptor.Descriptor.Lifetime);
        }

        private void RunExceptionTest<TException>(Func<TaskHubMiddlewareDescriptor> test)
            where TException : Exception
        {
            try
            {
                TaskHubMiddlewareDescriptor descriptor = test();
            }
            catch (TException)
            {
            }
        }

        private class TestMiddleware : AbstractMiddleware
        {
        }

        private abstract class AbstractMiddleware : ITaskHubMiddleware
        {
            public Task InvokeAsync(DispatchMiddlewareContext context, Func<Task> next)
            {
                throw new NotImplementedException();
            }
        }

    }
}
