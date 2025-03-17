namespace Mffm.Contracts;

/// <summary>
/// Publishes a message.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPublish<in T>
{
    /// <summary>
    /// Publish a message async
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync(T message, CancellationToken cancellationToken);
}

/// <summary>
/// Publishes a message.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPublish<in T1, in T2>
{
    /// <summary>
    /// Publish a message async
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync(T1 message, CancellationToken cancellationToken);

    /// <summary>
    /// Publish a message async
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync(T2 message, CancellationToken cancellationToken);
}

/// <summary>
/// Publishes a message.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPublish<in T1, in T2, in T3>
{
    /// <summary>
    /// Publish a message async
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync(T1 message, CancellationToken cancellationToken);

    /// <summary>
    /// Publish a message async
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync(T2 message, CancellationToken cancellationToken);

    /// <summary>
    /// Publish a message async
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync(T3 message, CancellationToken cancellationToken);
}
