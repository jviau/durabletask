using System;
using DurableTask.Core;

namespace DurableTask.DependencyInjection
{
    /// <summary>
    /// An object manager that gets its items via the service provider.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class ServiceObjectManager<TObject> : INameVersionObjectManager<TObject>
    {
        /// <inheritdoc />
        public void Add(ObjectCreator<TObject> creator)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TObject GetObject(string name, string version)
        {
            throw new NotImplementedException();
        }
    }
}
