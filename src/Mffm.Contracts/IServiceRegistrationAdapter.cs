namespace Mffm.Contracts
{
    using System;
    /// <summary>
    /// Provides a mechanism for registering service types and instances.
    /// </summary>
    public interface IServiceRegistrationAdapter
    {
        /// <summary>
        /// Registers a singleton service type.
        /// </summary>
        /// <param name="inf">The interface type of the service.</param>
        /// <param name="impl">The implementation type of the service.</param>
        public void RegisterSingletonType(Type inf, Type impl);

        /// <summary>
        /// Registers a transient service type.
        /// </summary>
        /// <param name="inf">The interface type of the service.</param>
        /// <param name="impl">The implementation type of the service.</param>
        public void RegisterTransientType(Type inf, Type impl);

        /// <summary>
        /// Registers a singleton service instance.
        /// </summary>
        /// <param name="inf">The interface type of the service.</param>
        /// <param name="impl">The instance of the service.</param>
        public void RegisterSingletonInstance(Type inf, object impl);
    }
}