using System.Reflection;
using Mffm.Contracts;

namespace Mffm.Core;

/// <summary>
/// Provides logic for registering forms and form models. A form model, i.e. name without namespace, can only exist once.
/// A 
/// </summary>
public class DefaultFormMapperBuilder : IFormMapperBuilder
{
    private readonly Dictionary<string, Type> _modelNameToFormModelMapping = new();
    private readonly Dictionary<string, Type> _modelNameToFormMapping = new();

    private Type[] GetServices<T>(Assembly assembly)
    {
        var types = assembly
            .GetTypes()
            .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToArray();
        return types;
    }

    public void RegisterAssembly(Assembly assembly)
    {
        var formModels = GetServices<IFormModel>(assembly);
        var forms = GetServices<Form>(assembly);

        // for all views, add the latest as key to the dictionary
        foreach (var formModel in formModels)
            _modelNameToFormModelMapping[formModel.Name] = formModel;

        // for all views, add the latest as key to the dictionary
        foreach (var form in forms)
        {
            var modelName = form.Name + "Model";
            _modelNameToFormMapping[modelName] = form;
        }
    }

    public IFormMapper Build(Action<Type> serviceRegistration)
    {
        var mappedDic = new Dictionary<Type, Type>();
        _modelNameToFormModelMapping.Keys.ToList().ForEach(key =>
        {
            if (_modelNameToFormMapping.ContainsKey(key))
                mappedDic[_modelNameToFormModelMapping[key]] = _modelNameToFormMapping[key];
        });

        mappedDic.Keys.Concat(mappedDic.Values).ToList().ForEach(serviceRegistration);

        var formMapper = new DefaultFormMapper(mappedDic);
        return formMapper;
    }
}