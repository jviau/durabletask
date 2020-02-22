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
    using DurableTask.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaskHubCollectionTests
    {
        [TestMethod]
        public void AddSucceeds()
        {
            // arrange
            var activity = TaskActivityDescriptor.Singleton<TestActivity>();
            var descriptors = new TaskHubCollection();

            // act
            bool result = descriptors.Add(activity);

            // assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, descriptors.Count);
            Assert.AreEqual(activity, descriptors.Single());
        }

        [TestMethod]
        public void AddDuplicate()
        {
            // arrange
            var activity = TaskActivityDescriptor.Singleton<TestActivity>();
            var descriptors = new TaskHubCollection();

            // act
            descriptors.Add(activity);
            bool result = descriptors.Add(activity);

            // assert
            Assert.IsFalse(result);
            Assert.AreEqual(1, descriptors.Count);
            Assert.AreEqual(activity, descriptors.Single());
        }

        [TestMethod]
        public void GetByName()
        {
            // arrange
            var activity = TaskActivityDescriptor.Singleton<TestActivity>();
            var descriptors = new TaskHubCollection()
            {
                activity,
            };

            // act
            Type actual = descriptors[activity.Name, activity.Version];

            // assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(typeof(TestActivity), actual);
        }

        [TestMethod]
        public void GetByName2()
        {
            // arrange
            var activity = TaskActivityDescriptor.Singleton<TestActivity>();
            var activity2 = TaskActivityDescriptor.Singleton<TestActivity2>();
            var descriptors = new TaskHubCollection()
            {
                activity,
                activity2,
            };

            // act
            Type actual = descriptors[activity2.Name, activity2.Version];

            // assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(typeof(TestActivity2), actual);
        }

        private class TestActivity : TaskActivity
        {
            public override string Run(TaskContext context, string input)
            {
                throw new NotImplementedException();
            }
        }

        private class TestActivity2 : TaskActivity
        {
            public override string Run(TaskContext context, string input)
            {
                throw new NotImplementedException();
            }
        }
    }
}
