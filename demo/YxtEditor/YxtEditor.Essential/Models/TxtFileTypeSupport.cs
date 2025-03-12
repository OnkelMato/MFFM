namespace YxtEditor.Essential.Models;

internal class TxtFileTypeSupport : IFileTypeSupport
{
    public string Extension { get; } = ".txt";
    public string Name { get; } = "Textfile";
    public YxtDocument LoadFromFile(string fileName)
    {

        // this is for a text file but not for a yxt file
        var document = new YxtDocument
        {
            Filename = fileName,
            Contents = File.ReadAllText(fileName)
        };

        return document;
    }

    public void SaveToFile(string filename, YxtDocument document)
    {
        File.WriteAllText(filename, document.Contents);

        document.HasPendingChanges = true;
        document.Filename = filename;
    }
}