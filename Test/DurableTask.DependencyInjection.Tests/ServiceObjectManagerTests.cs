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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using static TestHelpers;

    [TestClass]
    public class ServiceObjectManagerTests
    {
        [TestMethod]
        public void CtorArgumentNull1()
        {
            // arrange, act
            ArgumentNullException ex = Capture<ArgumentNullException>(
                () => new ServiceObjectManager<object>(null, Mock.Of<ITaskObjectCollection>()));

            // assert
            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void CtorArgumentNull2()
        {
            // arrange, act
            ArgumentNullException ex = Capture<ArgumentNullException>(
                () => new ServiceObjectManager<object>(Mock.Of<IServiceProvider>(), null));

            // assert
            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void AddNotSupported()
        {
            // arrange
            var manager = new ServiceObjectManager<object>(
                Mock.Of<IServiceProvider>(), Mock.Of<ITaskObjectCollection>());

            // act
            NotSupportedException ex = Capture<NotSupportedException>(
                () => manager.Add(Mock.Of<ObjectCreator<object>>()));

            // assert
            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void GetUsesServiceProvider()
        {
            // arrange
            string name = "ServiceObjectManagerTests_Name";
            string version = "ServiceObjectManagerTests_Version";
            var obj = new MyType();

            var descriptors = new Mock<ITaskObjectCollection>();
            descriptors.Setup(x => x[name, version]).Returns(typeof(MyType));

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(MyType))).Returns(obj);

            var manager = new ServiceObjectManager<MyType>(serviceProvider.Object, descriptors.Object);

            // act
            MyType actual = manager.GetObject(name, version);

            // assert
            Assert.IsNotNull(actual);
            Assert.AreSame(obj, actual);
        }

        private class MyType
        {
        }
    }
}
