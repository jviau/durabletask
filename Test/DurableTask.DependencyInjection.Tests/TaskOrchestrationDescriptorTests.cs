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
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaskOrchestrationDescriptorTests
    {
        private const string Name = "TaskOrchestrationDescriptorTests_Name";
        private const string Version = "TaskOrchestrationDescriptorTests_Version";

        [TestMethod]
        public void SingletonByType()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Singleton(typeof(TestOrchestration)),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingletonByTypeNamed()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Singleton(Name, Version, typeof(TestOrchestration)),
                Name,
                Version,
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByTypeNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskOrchestrationDescriptor.Singleton((Type)null));

        [TestMethod]
        public void SingleByTypeAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskOrchestrationDescriptor.Singleton(typeof(TaskOrchestration)));

        [TestMethod]
        public void SingletonByGeneric()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Singleton<TestOrchestration>(),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingletonByGenericNamed()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Singleton<TestOrchestration>(Name, Version),
                Name,
                Version,
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByGenericAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskOrchestrationDescriptor.Singleton<TaskOrchestration>());

        [TestMethod]
        public void SingletonByInstance()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Singleton(new TestOrchestration()),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingletonByInstanceNamed()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Singleton(Name, Version, new TestOrchestration()),
                Name,
                Version,
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByInstanceNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskOrchestrationDescriptor.Singleton((TaskOrchestration)null));

        [TestMethod]
        public void SingletonByFactory()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Singleton(_ => new TestOrchestration()),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingletonByFactoryNamed()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Singleton(Name, Version, _ => new TestOrchestration()),
                Name,
                Version,
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByFactoryNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskOrchestrationDescriptor.Singleton((Func<IServiceProvider, TaskOrchestration>)null));

        [TestMethod]
        public void SingleByFactoryAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskOrchestrationDescriptor.Singleton(_ => new TestOrchestration() as TaskOrchestration));

        [TestMethod]
        public void TransientByType()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Transient(typeof(TestOrchestration)),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByTypeNamed()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Transient(Name, Version, typeof(TestOrchestration)),
                Name,
                Version,
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByTypeNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskOrchestrationDescriptor.Transient((Type)null));

        [TestMethod]
        public void TransientByTypeAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskOrchestrationDescriptor.Transient(typeof(TaskOrchestration)));

        [TestMethod]
        public void TransientByGeneric()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Transient<TestOrchestration>(),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByGenericNamed()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Transient<TestOrchestration>(Name, Version),
                Name,
                Version,
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByGenericAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskOrchestrationDescriptor.Transient<TaskOrchestration>());

        [TestMethod]
        public void TransientByFactory()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Transient(_ => new TestOrchestration()),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByFactoryNamed()
            => RunTest<TestOrchestration>(
                () => TaskOrchestrationDescriptor.Transient(Name, Version, _ => new TestOrchestration()),
                Name,
                Version,
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByFactoryNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskOrchestrationDescriptor.Transient((Func<IServiceProvider, TaskOrchestration>)null));

        [TestMethod]
        public void TransientByFactoryAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskOrchestrationDescriptor.Transient(_ => new TestOrchestration() as TaskOrchestration));

        private void RunTest<TMiddleware>(Func<TaskOrchestrationDescriptor> test, ServiceLifetime serviceLifetime)
        {
            TaskOrchestrationDescriptor descriptor = test();

            Assert.IsNotNull(descriptor);
            Assert.AreEqual(typeof(TMiddleware), descriptor.Type);
            Assert.AreEqual(typeof(TMiddleware).FullName, descriptor.Name);
            Assert.AreEqual(string.Empty, descriptor.Version);
            Assert.IsNotNull(descriptor.Descriptor);
            Assert.AreEqual(serviceLifetime, descriptor.Descriptor.Lifetime);
        }

        private void RunTest<TMiddleware>(
            Func<TaskOrchestrationDescriptor> test, string name, string version, ServiceLifetime serviceLifetime)
        {
            TaskOrchestrationDescriptor descriptor = test();

            Assert.IsNotNull(descriptor);
            Assert.AreEqual(typeof(TMiddleware), descriptor.Type);
            Assert.AreEqual(name, descriptor.Name);
            Assert.AreEqual(version, descriptor.Version);
            Assert.IsNotNull(descriptor.Descriptor);
            Assert.AreEqual(serviceLifetime, descriptor.Descriptor.Lifetime);
        }

        private void RunExceptionTest<TException>(Func<TaskOrchestrationDescriptor> test)
            where TException : Exception
        {
            try
            {
                TaskOrchestrationDescriptor descriptor = test();
            }
            catch (TException)
            {
            }
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
    }
}
