namespace YxtEditor.Essential.Models;

internal interface IFileTypeSupport
{
    string Extension { get; }
    string Name { get; }
    YxtDocument LoadFromFile(string fileName);
    void SaveToFile(string filename, YxtDocument document);
}