using System.Reflection;
using Mffm.Contracts;

namespace Mffm.Core;

/// <summary>
/// Provides logic for registering forms and form models. A form model, i.e. name without namespace, can only exist once.
/// A 
/// </summary>
internal class DefaultFormMapperBuilder : IFormMapperBuilder
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
        // a form model is the model for a form, i.e. the data that is displayed in the form
        // the design of this framework says, that the FormModel is the base and a Form is attached
        // (not the other way around)
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

    public IFormMapper Build(IServiceRegistrationAdapter containerBuilder)
    {
        // let's map the form models to the forms in a dictionary
        var mappedDic = new Dictionary<Type, Type>();
        _modelNameToFormModelMapping.Keys.ToList().ForEach(key =>
        {
            if (_modelNameToFormMapping.ContainsKey(key))
                mappedDic[_modelNameToFormModelMapping[key]] = _modelNameToFormMapping[key];
        });

        // register all mappings (key and value)
        mappedDic.Keys.Concat(mappedDic.Values)
            .ToList()
            .ForEach(t => containerBuilder.RegisterTransientType(t, t));

        // let's create the mapper with the final mapping dictionary
        var formMapper = new DefaultFormMapper(mappedDic);
        return formMapper;
    }
}