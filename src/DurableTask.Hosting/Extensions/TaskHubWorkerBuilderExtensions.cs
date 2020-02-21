namespace DurableTask.Hosting.Extensions
{
    using System;
    using DurableTask.Core;

    /// <summary>
    /// Extensions for <see cref="ITaskHubWorkerBuilder"/>.
    /// </summary>
    public static class TaskHubWorkerBuilderExtensions
    {
        /// <summary>
        /// Adds the supplied activity type to the builder.
        /// Must be of type <see cref="TaskActivity"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="type">The activity type to add.</param>
        /// <returns>The original builder, with the activity type added.</returns>
        public static ITaskHubWorkerBuilder AddActivity(this ITaskHubWorkerBuilder builder, Type type)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddActivity(TaskHubDescriptor.Activity(type));
        }

        /// <summary>
        /// Adds the supplied activity type to the builder.
        /// Must be of type <see cref="TaskActivity"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <typeparam name="TActivity">The task activity type to add.</typeparam>
        /// <returns>The original builder, with the activity type added.</returns>
        public static ITaskHubWorkerBuilder AddActivity<TActivity>(this ITaskHubWorkerBuilder builder)
            where TActivity : TaskActivity
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddActivity(TaskHubDescriptor.Activity<TActivity>());
        }

        /// <summary>
        /// Adds the supplied activity type to the builder.
        /// Must be of type <see cref="TaskActivity"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="name">The name of the task to add.</param>
        /// <param name="version">The version of the task to add.</param>
        /// <param name="type">The activity type to add.</param>
        /// <returns>The original builder, with the activity type added.</returns>
        public static ITaskHubWorkerBuilder AddActivity(
            this ITaskHubWorkerBuilder builder, string name, string version, Type type)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddActivity(TaskHubDescriptor.Activity(name, version, type));
        }

        /// <summary>
        /// Adds the supplied activity type to the builder.
        /// Must be of type <see cref="TaskActivity"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="name">The name of the task to add.</param>
        /// <param name="version">The version of the task to add.</param>
        /// <typeparam name="TActivity">The task activity type to add.</typeparam>
        /// <returns>The original builder, with the activity type added.</returns>
        public static ITaskHubWorkerBuilder AddActivity<TActivity>(
            this ITaskHubWorkerBuilder builder, string name, string version)
            where TActivity : TaskActivity
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddActivity(TaskHubDescriptor.Activity<TActivity>(name, version));
        }

        /// <summary>
        /// Adds the supplied activity type to the builder.
        /// Must be of type <see cref="TaskActivity"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="name">The name of the task to add.</param>
        /// <param name="version">The version of the task to add.</param>
        /// <param name="creator">The delegate to create the task activity.</param>
        /// <typeparam name="TActivity">The task activity type to add.</typeparam>
        /// <returns>The original builder, with the activity type added.</returns>
        public static ITaskHubWorkerBuilder AddActivity<TActivity>(
            this ITaskHubWorkerBuilder builder, string name, string version, Func<IServiceProvider, TActivity> creator)
            where TActivity : TaskActivity
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddActivity(TaskHubDescriptor.Activity(name, version, creator));
        }

        /// <summary>
        /// Adds the supplied activity type to the builder.
        /// Must be of type <see cref="TaskActivity"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="creator">The delegate to create the task activity.</param>
        /// <typeparam name="TActivity">The task activity type to add.</typeparam>
        /// <returns>The original builder, with the activity type added.</returns>
        public static ITaskHubWorkerBuilder AddActivity<TActivity>(
            this ITaskHubWorkerBuilder builder, Func<IServiceProvider, TActivity> creator)
            where TActivity : TaskActivity
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddActivity(TaskHubDescriptor.Activity(creator));
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="type">The orchestration type to add.</param>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration(this ITaskHubWorkerBuilder builder, Type type)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskHubDescriptor.Orchestration(type));
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <typeparam name="TOrchestration">The task orchestration type to add.</typeparam>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration<TOrchestration>(this ITaskHubWorkerBuilder builder)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskHubDescriptor.Orchestration<TOrchestration>());
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="name">The name of the task to add.</param>
        /// <param name="version">The version of the task to add.</param>
        /// <param name="type">The orchestration type to add.</param>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration(
            this ITaskHubWorkerBuilder builder, string name, string version, Type type)
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskHubDescriptor.Orchestration(name, version, type));
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="name">The name of the task to add.</param>
        /// <param name="version">The version of the task to add.</param>
        /// <typeparam name="TOrchestration">The task orchestration type to add.</typeparam>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration<TOrchestration>(
            this ITaskHubWorkerBuilder builder, string name, string version)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskHubDescriptor.Orchestration<TOrchestration>(name, version));
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="name">The name of the task to add.</param>
        /// <param name="version">The version of the task to add.</param>
        /// <param name="creator">The delegate to create the task orchestration.</param>
        /// <typeparam name="TOrchestration">The task orchestration type to add.</typeparam>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration<TOrchestration>(
            this ITaskHubWorkerBuilder builder, string name, string version, Func<IServiceProvider, TOrchestration> creator)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskHubDescriptor.Orchestration(name, version, creator));
        }

        /// <summary>
        /// Adds the supplied orchestration type to the builder.
        /// Must be of type <see cref="TaskOrchestration"/>.
        /// </summary>
        /// <param name="builder">The builder to add to.</param>
        /// <param name="creator">The delegate to create the task orchestration.</param>
        /// <typeparam name="TOrchestration">The task orchestration type to add.</typeparam>
        /// <returns>The original builder, with the orchestration type added.</returns>
        public static ITaskHubWorkerBuilder AddOrchestration<TOrchestration>(
            this ITaskHubWorkerBuilder builder, Func<IServiceProvider, TOrchestration> creator)
            where TOrchestration : TaskOrchestration
        {
            Check.NotNull(builder, nameof(builder));
            return builder.AddOrchestration(TaskHubDescriptor.Orchestration(creator));
        }
    }
}
