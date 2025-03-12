using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using YxtEditor.Essential.Extensions;

namespace YxtEditor.Essential.Models;

internal class YamlFileTypeSupport : IFileTypeSupport
{
    // ReSharper disable once ClassNeverInstantiated.Local
    private class YxtDocumentInternal
    {
        public string Contents { get; set; } = string.Empty;

        public Dictionary<string, string> Properties { get; set; } = new();
    }

    public string Extension { get; } = ".yxt";
    public string Name { get; } = "Yaml Textfile";

    public YxtDocument LoadFromFile(string fileName)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
        var intDoc = deserializer.Deserialize<YxtDocumentInternal>(File.ReadAllText(fileName));

        var result = new YxtDocument()
        {
            Contents = intDoc.Contents.NormalizeNewLines(),
            Properties = intDoc.Properties,
            Filename = fileName
        };

        return result;
    }

    public void SaveToFile(string filename, YxtDocument document)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        var docInt = new YxtDocumentInternal() { Contents = document.Contents, Properties = document.Properties };
        var docAsYaml = serializer.Serialize(docInt);
        File.WriteAllText(filename, docAsYaml);

        document.HasPendingChanges = true;
        document.Filename = filename;
    }
}