using System;
using System.Collections.Generic;
using System.Text;
using DurableTask.Core;

namespace DurableTask.DependencyInjection
{
    /// <summary>
    /// An object manager that gets its items via the service provider.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class ServiceObjectManager<TObject> : INameVersionObjectManager<TObject>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creator"></param>
        public void Add(ObjectCreator<TObject> creator)
        {
            throw new NotImplementedException();
        }

        public TObject GetObject(string name, string version)
        {
            throw new NotImplementedException();
        }
    }
}
