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
    public class TaskMiddlewareDescriptorTests
    {
        [TestMethod]
        public void SingletonByType()
            => RunTest<TestMiddleware>(
                () => TaskMiddlewareDescriptor.Singleton(typeof(TestMiddleware)),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByTypeNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskMiddlewareDescriptor.Singleton((Type)null));

        [TestMethod]
        public void SingleByTypeAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskMiddlewareDescriptor.Singleton(typeof(AbstractMiddleware)));

        [TestMethod]
        public void SingletonByGeneric()
            => RunTest<TestMiddleware>(
                () => TaskMiddlewareDescriptor.Singleton<TestMiddleware>(),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByGenericAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskMiddlewareDescriptor.Singleton<AbstractMiddleware>());

        [TestMethod]
        public void SingletonByInstance()
            => RunTest<TestMiddleware>(
                () => TaskMiddlewareDescriptor.Singleton(new TestMiddleware()),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByInstanceNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskMiddlewareDescriptor.Singleton((ITaskHubMiddleware)null));

        [TestMethod]
        public void SingletonByFactory()
            => RunTest<TestMiddleware>(
                () => TaskMiddlewareDescriptor.Singleton(_ => new TestMiddleware()),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByFactoryNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskMiddlewareDescriptor.Singleton((Func<IServiceProvider, ITaskHubMiddleware>)null));

        [TestMethod]
        public void SingleByFactoryAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskMiddlewareDescriptor.Singleton(_ => new TestMiddleware() as AbstractMiddleware));

        [TestMethod]
        public void TransientByType()
            => RunTest<TestMiddleware>(
                () => TaskMiddlewareDescriptor.Transient(typeof(TestMiddleware)),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByTypeNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskMiddlewareDescriptor.Transient((Type)null));

        [TestMethod]
        public void TransientByTypeAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskMiddlewareDescriptor.Transient(typeof(AbstractMiddleware)));

        [TestMethod]
        public void TransientByGeneric()
            => RunTest<TestMiddleware>(
                () => TaskMiddlewareDescriptor.Transient<TestMiddleware>(),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByGenericAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskMiddlewareDescriptor.Transient<AbstractMiddleware>());

        [TestMethod]
        public void TransientByFactory()
            => RunTest<TestMiddleware>(
                () => TaskMiddlewareDescriptor.Transient(_ => new TestMiddleware()),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByFactoryNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskMiddlewareDescriptor.Transient((Func<IServiceProvider, ITaskHubMiddleware>)null));

        [TestMethod]
        public void TransientByFactoryAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskMiddlewareDescriptor.Transient(_ => new TestMiddleware() as AbstractMiddleware));

        private void RunTest<TMiddleware>(Func<TaskMiddlewareDescriptor> test, ServiceLifetime serviceLifetime)
        {
            TaskMiddlewareDescriptor descriptor = test();

            Assert.IsNotNull(descriptor);
            Assert.AreEqual(typeof(TMiddleware), descriptor.Type);
            Assert.IsNotNull(descriptor.Descriptor);
            Assert.AreEqual(serviceLifetime, descriptor.Descriptor.Lifetime);
        }

        private void RunExceptionTest<TException>(Func<TaskMiddlewareDescriptor> test)
            where TException : Exception
        {
            try
            {
                TaskMiddlewareDescriptor descriptor = test();
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
