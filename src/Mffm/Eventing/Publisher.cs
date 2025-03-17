using Mffm.Contracts;

namespace Mffm.Eventing;

internal class Publisher<T>(IEventAggregator eventAggregator) : IPublish<T>
{
    public Task PublishAsync(T message, CancellationToken cancellationToken)
    {
        return eventAggregator.PublishAsync(message, cancellationToken);
    }
}

internal class Publisher<T1, T2>(IEventAggregator eventAggregator) : IPublish<T1, T2>
{
    public Task PublishAsync(T1 message, CancellationToken cancellationToken)
    {
        return eventAggregator.PublishAsync(message, cancellationToken);
    }
    public Task PublishAsync(T2 message, CancellationToken cancellationToken)
    {
        return eventAggregator.PublishAsync(message, cancellationToken);
    }
}

internal class Publisher<T1, T2, T3>(IEventAggregator eventAggregator) : IPublish<T1, T2, T3>
{
    public Task PublishAsync(T1 message, CancellationToken cancellationToken)
    {
        return eventAggregator.PublishAsync(message, cancellationToken);
    }
    public Task PublishAsync(T2 message, CancellationToken cancellationToken)
    {
        return eventAggregator.PublishAsync(message, cancellationToken);
    }
    public Task PublishAsync(T3 message, CancellationToken cancellationToken)
    {
        return eventAggregator.PublishAsync(message, cancellationToken);
    }

}
