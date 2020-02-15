using System;

namespace DurableTask.DependencyInjection
{
    using DurableTask.Core;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// An <see cref="ObjectCreator{T}"/> that instantiates from the service provider.
    /// </summary>
    /// <typeparam name="T">The type to create.</typeparam>
    public class ServiceObjectCreator<T> : ObjectCreator<T>
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of <see cref="IServiceProvider"/>.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ServiceObjectCreator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <inheritdoc />
        public override T Create()
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}
