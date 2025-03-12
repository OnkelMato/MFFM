namespace YxtEditor.Essential.Models;

internal class YxtDocument
{
    public string? Filename { get; set; }
    public bool HasPendingChanges { get; set; } = false;

    public Dictionary<string, string> Properties { get; set; } = new();

    public IFileTypeSupport FileTypeSupport { get; set; } = new YamlFileTypeSupport();

    public string Contents { get; set; } = string.Empty;
}
