using System.Reflection;
using Mffm.Contracts;

namespace Mffm.Core;

public class DefaultFormLookup : IFormLookup
{
    private readonly Dictionary<Type, Type> _formModelToFormMapping = new();

    public Type GetFormFor<TFormModel>() where TFormModel : class, IFormModel
    {
        if (!_formModelToFormMapping.ContainsKey(typeof(TFormModel)))
            throw new Exception($"Cannot find the form for ${typeof(TFormModel)}");

        return _formModelToFormMapping[typeof(TFormModel)];
    }

    public Type[] GetForms()
    {
        return _formModelToFormMapping.Values.ToArray();
    }

    public Type[] GetFormModels()
    {
        return _formModelToFormMapping.Keys.ToArray();
    }

    private IEnumerable<Type> GetServices<T>(Assembly assembly)
    {
        var types = assembly
            .GetTypes()
            .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        return types;
    }

    public void RegisterAssembly(Assembly assembly)
    {
        var formModels = GetServices<IFormModel>(assembly);
        var forms = GetServices<Form>(assembly);
        foreach (var formModel in formModels)
        {
            var form = forms.FirstOrDefault(f => f.Name == formModel.Name.Replace("Model", ""));
            if (form == null) continue;
            _formModelToFormMapping.Add(formModel, form);
        }
    }
}