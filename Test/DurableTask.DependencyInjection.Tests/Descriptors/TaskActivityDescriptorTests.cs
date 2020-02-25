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
    using DurableTask.Core;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaskActivityDescriptorTests
    {
        private const string Name = "TaskActivityDescriptorTests_Name";
        private const string Version = "TaskActivityDescriptorTests_Version";

        [TestMethod]
        public void SingletonByType()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Singleton(typeof(TestActivity)),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingletonByTypeNamed()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Singleton(typeof(TestActivity), Name, Version),
                Name,
                Version,
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByTypeNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskActivityDescriptor.Singleton((Type)null));

        [TestMethod]
        public void SingleByTypeAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskActivityDescriptor.Singleton(typeof(TaskActivity)));

        [TestMethod]
        public void SingletonByGeneric()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Singleton<TestActivity>(),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingletonByGenericNamed()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Singleton<TestActivity>(Name, Version),
                Name,
                Version,
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByGenericAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskActivityDescriptor.Singleton<TaskActivity>());

        [TestMethod]
        public void SingletonByInstance()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Singleton(new TestActivity()),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingletonByInstanceNamed()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Singleton(new TestActivity(), Name, Version),
                Name,
                Version,
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByInstanceNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskActivityDescriptor.Singleton((TaskActivity)null));

        [TestMethod]
        public void SingletonByFactory()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Singleton(_ => new TestActivity()),
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingletonByFactoryNamed()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Singleton(_ => new TestActivity(), Name, Version),
                Name,
                Version,
                ServiceLifetime.Singleton);

        [TestMethod]
        public void SingleByFactoryNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskActivityDescriptor.Singleton((Func<IServiceProvider, TaskActivity>)null));

        [TestMethod]
        public void SingleByFactoryAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskActivityDescriptor.Singleton(_ => new TestActivity() as TaskActivity));

        [TestMethod]
        public void TransientByType()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Transient(typeof(TestActivity)),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByTypeNamed()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Transient(typeof(TestActivity), Name, Version),
                Name,
                Version,
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByTypeNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskActivityDescriptor.Transient((Type)null));

        [TestMethod]
        public void TransientByTypeAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskActivityDescriptor.Transient(typeof(TaskActivity)));

        [TestMethod]
        public void TransientByGeneric()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Transient<TestActivity>(),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByGenericNamed()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Transient<TestActivity>(Name, Version),
                Name,
                Version,
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByGenericAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskActivityDescriptor.Transient<TaskActivity>());

        [TestMethod]
        public void TransientByFactory()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Transient(_ => new TestActivity()),
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByFactoryNamed()
            => RunTest<TestActivity>(
                () => TaskActivityDescriptor.Transient(_ => new TestActivity(), Name, Version),
                Name,
                Version,
                ServiceLifetime.Transient);

        [TestMethod]
        public void TransientByFactoryNull()
            => RunExceptionTest<ArgumentNullException>(
                () => TaskActivityDescriptor.Transient((Func<IServiceProvider, TaskActivity>)null));

        [TestMethod]
        public void TransientByFactoryAbstract()
            => RunExceptionTest<ArgumentException>(
                () => TaskActivityDescriptor.Transient(_ => new TestActivity() as TaskActivity));

        private void RunTest<TMiddleware>(Func<TaskActivityDescriptor> test, ServiceLifetime serviceLifetime)
        {
            TaskActivityDescriptor descriptor = test();

            Assert.IsNotNull(descriptor);
            Assert.AreEqual(typeof(TMiddleware), descriptor.Type);
            Assert.AreEqual(typeof(TMiddleware).FullName, descriptor.Name);
            Assert.AreEqual(string.Empty, descriptor.Version);
            Assert.IsNotNull(descriptor.Descriptor);
            Assert.AreEqual(serviceLifetime, descriptor.Descriptor.Lifetime);
        }

        private void RunTest<TMiddleware>(
            Func<TaskActivityDescriptor> test, string name, string version, ServiceLifetime serviceLifetime)
        {
            TaskActivityDescriptor descriptor = test();

            Assert.IsNotNull(descriptor);
            Assert.AreEqual(typeof(TMiddleware), descriptor.Type);
            Assert.AreEqual(name, descriptor.Name);
            Assert.AreEqual(version, descriptor.Version);
            Assert.IsNotNull(descriptor.Descriptor);
            Assert.AreEqual(serviceLifetime, descriptor.Descriptor.Lifetime);
        }

        private void RunExceptionTest<TException>(Func<TaskActivityDescriptor> test)
            where TException : Exception
        {
            TException exception = TestHelpers.Capture<TException>(() => test());
            Assert.IsNotNull(exception);
        }

        private class TestActivity : TaskActivity
        {
            public override string Run(TaskContext context, string input)
            {
                throw new NotImplementedException();
            }
        }
    }
}
