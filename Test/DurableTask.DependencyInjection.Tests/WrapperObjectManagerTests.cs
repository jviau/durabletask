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
                () => new WrapperObjectManager<object>(null, _ => null));

            // assert
            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void CtorArgumentNull2()
        {
            // arrange, act
            ArgumentNullException ex = Capture<ArgumentNullException>(
                () => new WrapperObjectManager<object>(Mock.Of<ITaskObjectCollection>(), null));

            // assert
            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void AddNotSupported()
        {
            // arrange
            var manager = new WrapperObjectManager<object>(
                Mock.Of<ITaskObjectCollection>(), _ => new object());

            // act
            NotSupportedException ex = Capture<NotSupportedException>(
                () => manager.Add(Mock.Of<ObjectCreator<object>>()));

            // assert
            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void GetUsesFactory()
        {
            // arrange
            string name = "WrapperObjectManagerTests_Name";
            string version = "WrapperObjectManagerTests_Version";

            var descriptors = new Mock<ITaskObjectCollection>();
            descriptors.Setup(x => x[name, version]).Returns(typeof(MyType));

            var manager = new WrapperObjectManager<MyType>(descriptors.Object, t => new MyTypeWrapped(t));

            // act
            MyType actual = manager.GetObject(name, version);
            var wrapper = actual as MyTypeWrapped;

            // assert
            Assert.IsNotNull(wrapper);
            Assert.AreEqual(typeof(MyType), wrapper.Type);
        }

        private class MyType
        {
        }

        private class MyTypeWrapped : MyType
        {
            public MyTypeWrapped(Type t)
            {
                Type = t;
            }

            public Type Type { get; }
        }
    }
}
