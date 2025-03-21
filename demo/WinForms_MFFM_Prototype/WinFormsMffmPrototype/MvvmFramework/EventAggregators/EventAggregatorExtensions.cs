﻿namespace WinFormsMffmPrototype.MvvmFramework.EventAggregators
{
    /// <summary>
    /// Extensions for <see cref="IEventAggregator"/>.
    /// </summary>
    public static class EventAggregatorExtensions
    {
        /// <summary>
        /// Publishes a message on the current thread (synchrone).
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name = "message">The message instance.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static void Publish(this IEventAggregator eventAggregator, object message)
        {
            eventAggregator.PublishAsync(message, CancellationToken.None).Wait();
        }
    }
}
