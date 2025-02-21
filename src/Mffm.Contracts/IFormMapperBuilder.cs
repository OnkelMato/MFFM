using System.Reflection;

namespace Mffm.Contracts
{
    /// <summary>
    /// Interface for building form mappers.
    /// </summary>
    public interface IFormMapperBuilder
    {
        /// <summary>
        /// Registers an assembly with the form mapper builder.
        /// </summary>
        /// <param name="assembly">The assembly to register.</param>
        void RegisterAssembly(Assembly assembly);

        /// <summary>
        /// Builds the form mapper using the specified service registration adapter.
        /// </summary>
        /// <param name="containerBuilder">The service registration adapter to use for building the form mapper.</param>
        /// <returns>An instance of <see cref="IFormMapper"/>.</returns>
        IFormMapper Build(IServiceRegistrationAdapter containerBuilder);
    }
}