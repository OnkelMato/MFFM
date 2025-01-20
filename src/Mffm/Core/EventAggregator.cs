using System.Reflection;
using Mffm.Contracts;

namespace Mffm.Core;

public class EventAggregator : IEventAggregator
{
    private readonly List<Handler> _handlers = [];

    public virtual bool HandlerExistsFor(Type messageType)
    {
        lock (_handlers)
        {
            return _handlers.Any(handler => handler.Handles(messageType) && !handler.IsDead);
        }
    }

    public virtual void Subscribe(object subscriber)
    {
        if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

        lock (_handlers)
        {
            if (_handlers.Any(x => x.Matches(subscriber))) return;
            _handlers.Add(new Handler(subscriber));
        }
    }

    public virtual void Unsubscribe(object subscriber)
    {
        if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

        lock (_handlers)
        {
            var found = _handlers.FirstOrDefault(x => x.Matches(subscriber));

            if (found != null) _handlers.Remove(found);
        }
    }

    public virtual async Task PublishAsync(object message, CancellationToken cancellationToken)
    {
        if (message == null) throw new ArgumentNullException(nameof(message));

        Handler[] toNotify;

        lock (_handlers)
        {
            toNotify = _handlers.ToArray();
        }

        var messageType = message.GetType();

        var tasks = toNotify.Select(h => h.Handle(messageType, message, cancellationToken));

        await Task.WhenAll(tasks);

        var dead = toNotify.Where(h => h.IsDead).ToList();

        if (dead.Any())
            lock (_handlers)
            {
                dead.Apply(x => _handlers.Remove(x));
            }
    }

    private class Handler
    {
        private readonly WeakReference _reference;
        private readonly Dictionary<Type, MethodInfo> _supportedHandlers = new();

        public Handler(object handler)
        {
            _reference = new WeakReference(handler);

            var interfaces = handler.GetType().GetTypeInfo().ImplementedInterfaces
                .Where(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandle<>));

            foreach (var @interface in interfaces)
            {
                var type = @interface.GetTypeInfo().GenericTypeArguments[0];
                var method = @interface.GetRuntimeMethod("HandleAsync", new[] { type, typeof(CancellationToken) });

                if (method != null) _supportedHandlers[type] = method;
            }
        }

        public bool IsDead => _reference.Target == null;

        public bool Matches(object instance)
        {
            return ReferenceEquals(_reference.Target, instance);
        }

        public Task Handle(Type messageType, object message, CancellationToken cancellationToken)
        {
            var target = _reference.Target;

            if (target == null) return Task.FromResult(false);

            var tasks = _supportedHandlers
                .Where(handler => handler.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()))
                .Select(pair => pair.Value.Invoke(target, new[] { message, cancellationToken }))
                .Select(result => (Task)result)
                .ToList();

            return Task.WhenAll(tasks);
        }

        public bool Handles(Type messageType)
        {
            return _supportedHandlers.Any(pair => pair.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()));
        }
    }
}