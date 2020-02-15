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

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Extensions for <see cref="ServiceDescriptor"/>.
    /// </summary>
    internal static class ServiceDescriptorExtensions
    {
        /// <summary>
        /// Gets the implementation type of the service.
        /// </summary>
        /// <param name="descriptor">The service descriptor to get the implementation type of.</param>
        /// <returns>The implementation type of the <paramref name="descriptor"/>.</returns>
        internal static Type GetImplementationType(this ServiceDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            if (descriptor.ImplementationType != null)
            {
                return descriptor.ImplementationType;
            }
            else if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance.GetType();
            }
            else if (descriptor.ImplementationFactory != null)
            {
                Type[] typeArguments = descriptor.ImplementationFactory.GetType().GenericTypeArguments;
                Debug.Assert(typeArguments.Length == 2);
                return typeArguments[1];
            }

            Debug.Assert(
                false,
                "ImplementationType, ImplementationInstance, or ImplementationFactory must be non null");
            return null;
        }
    }
}
