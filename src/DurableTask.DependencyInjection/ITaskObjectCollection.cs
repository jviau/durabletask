using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DurableTask.DependencyInjection
{
    /// <summary>
    /// The collection of task types implementing <typeparamref name="TObject"/>.
    /// </summary>
    public interface ITaskObjectCollection<TObject> : IReadOnlyCollection<Task>
    {
        /// <summary>
        /// Gets the task object identified by <paramref name="taskName"/> and <paramref name="taskVersion"/>.
        /// The returned type will implement <typeparamref name="TObject"/>.
        /// </summary>
        Type this[string taskName, string taskVersion] { get; }
    }
}
