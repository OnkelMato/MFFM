namespace Mffm.Contracts;

public interface IServiceRegistrationAdapter
{
    public void RegisterSingletonType(Type inf, Type impl);
    public void RegisterTransientType(Type inf, Type impl);
    public void RegisterSingletonInstance(Type inf, object impl);
}