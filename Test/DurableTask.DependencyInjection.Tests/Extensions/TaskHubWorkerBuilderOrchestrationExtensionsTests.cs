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
    using Moq;
    using static TestHelpers;

    [TestClass]
    public class TaskHubWorkerBuilderOrchestrationExtensionsTests
    {
        private const string Name = "TaskHubWorkerBuilderOrchestrationExtensionsTests_Name";
        private const string Version = "TaskHubWorkerBuilderOrchestrationExtensionsTests_Version";

        [TestMethod]
        public void AddOrchestrationTypeNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderOrchestrationExtensions.AddOrchestration(null, typeof(TestOrchestration)));

        [TestMethod]
        public void AddOrchestrationTypeNullType()
            => RunTestException<ArgumentNullException>(
                builder => builder.AddOrchestration((Type)null));

        [TestMethod]
        public void AddOrchestrationGenericNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderOrchestrationExtensions.AddOrchestration<TestOrchestration>(null));

        [TestMethod]
        public void AddOrchestrationFactoryNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderOrchestrationExtensions.AddOrchestration(null, __ => new TestOrchestration()));

        [TestMethod]
        public void AddOrchestrationTypeNamedNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderOrchestrationExtensions.AddOrchestration(null, typeof(TestOrchestration), Name, Version));

        [TestMethod]
        public void AddOrchestrationTypeNamedNullType()
            => RunTestException<ArgumentNullException>(
                builder => builder.AddOrchestration(null, Name, Version));

        [TestMethod]
        public void AddOrchestrationGenericNamedNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderOrchestrationExtensions.AddOrchestration<TestOrchestration>(null, Name, Version));

        [TestMethod]
        public void AddOrchestrationFactoryNamedNullBuilder()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubWorkerBuilderOrchestrationExtensions.AddOrchestration(null, __ => new TestOrchestration(), Name, Version));

        [TestMethod]
        public void AddOrchestrationType()
            => RunTest(
                builder => builder.AddOrchestration(typeof(TestOrchestration)),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.AddOrchestration(IsDescriptor(typeof(TestOrchestration))), Times.Once);
                    mock.VerifyNoOtherCalls();
                });

        [TestMethod]
        public void AddOrchestrationGeneric()
            => RunTest(
                builder => builder.AddOrchestration<TestOrchestration>(),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.AddOrchestration(IsDescriptor(typeof(TestOrchestration))), Times.Once);
                    mock.VerifyNoOtherCalls();
                });

        [TestMethod]
        public void AddOrchestrationFactory()
            => RunTest(
                builder => builder.AddOrchestration(_ => new TestOrchestration()),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.AddOrchestration(IsDescriptor(typeof(TestOrchestration))), Times.Once);
                    mock.VerifyNoOtherCalls();
                });

        [TestMethod]
        public void AddOrchestrationTypeNamed()
            => RunTest(
                builder => builder.AddOrchestration(typeof(TestOrchestration), Name, Version),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.AddOrchestration(IsDescriptor(typeof(TestOrchestration), Name, Version)), Times.Once);
                    mock.VerifyNoOtherCalls();
                });

        [TestMethod]
        public void AddOrchestrationGenericNamed()
            => RunTest(
                builder => builder.AddOrchestration<TestOrchestration>(Name, Version),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.AddOrchestration(IsDescriptor(typeof(TestOrchestration), Name, Version)), Times.Once);
                    mock.VerifyNoOtherCalls();
                });

        [TestMethod]
        public void AddOrchestrationFactoryNamed()
            => RunTest(
                builder => builder.AddOrchestration(_ => new TestOrchestration(), Name, Version),
                (mock, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(mock.Object, builder);
                    mock.Verify(m => m.AddOrchestration(IsDescriptor(typeof(TestOrchestration), Name, Version)), Times.Once);
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
            mock.Setup(m => m.AddOrchestration(It.IsAny<TaskOrchestrationDescriptor>())).Returns(mock.Object);

            TResult result = act(mock.Object);
            verify?.Invoke(mock, result);
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

        private TaskOrchestrationDescriptor IsDescriptor(Type type)
            => Match.Create<TaskOrchestrationDescriptor>(
                descriptor => descriptor.Type == type
                    && descriptor.Descriptor.Lifetime == ServiceLifetime.Transient);

        private TaskOrchestrationDescriptor IsDescriptor(Type type, string name, string version)
            => Match.Create<TaskOrchestrationDescriptor>(
                descriptor => descriptor.Name == name
                    && descriptor.Version == version
                    && descriptor.Type == type
                    && descriptor.Descriptor.Lifetime == ServiceLifetime.Transient);
    }
}
