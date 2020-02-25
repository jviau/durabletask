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
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using static TestHelpers;

    [TestClass]
    public class TaskHubServiceCollectionExtensionsTests
    {
        [TestMethod]
        public void AddTaskHubWorkerNull()
            => RunTestException<ArgumentNullException>(
                _ => TaskHubServiceCollectionExtensions.AddTaskHubWorker(null));

        [TestMethod]
        public void AddTaskHubWorkerNull2()
            => RunTestException<ArgumentNullException>(
                services => TaskHubServiceCollectionExtensions.AddTaskHubWorker(services, null));

        [TestMethod]
        public void AddTaskHubWorker()
            => RunTest(
                null,
                services => services.AddTaskHubWorker(),
                (services, builder) =>
                {
                    Assert.IsNotNull(builder);
                    Assert.AreSame(services, builder.Services);
                    Assert.AreEqual(1, services.Count);

                    ServiceDescriptor descriptor = services.First();
                    Assert.AreEqual(ServiceLifetime.Singleton, descriptor.Lifetime);
                    Assert.IsNotNull(descriptor.ImplementationFactory);
                });

        [TestMethod]
        public void AddTaskHubWorkerFunc()
            => RunTest(
                null,
                services => services.AddTaskHubWorker(
                    builder =>
                    {
                        Assert.IsNotNull(builder);
                    }),
                (original, returned) =>
                {
                    Assert.IsNotNull(returned);
                    Assert.AreSame(original, returned);
                    Assert.AreEqual(1, original.Count);

                    ServiceDescriptor descriptor = original.First();
                    Assert.AreEqual(ServiceLifetime.Singleton, descriptor.Lifetime);
                    Assert.IsNotNull(descriptor.ImplementationFactory);
                });

        private static void RunTestException<TException>(Action<IServiceCollection> act)
            where TException : Exception
        {
            bool Act(IServiceCollection services)
            {
                act(services);
                return true;
            }

            TException exception = Capture<TException>(() => RunTest(null, Act, null));
            Assert.IsNotNull(exception);
        }

        private static void RunTest<TResult>(
            Action<IServiceCollection> arrange,
            Func<IServiceCollection, TResult> act,
            Action<IServiceCollection, TResult> verify)
        {
            var services = new ServiceCollection();
            arrange?.Invoke(services);
            TResult result = act(services);
            verify?.Invoke(services, result);
        }
    }
}
