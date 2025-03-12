namespace YxtEditor.Essential.Extensions;

public static class StringExtensions
{
    public static string NormalizeNewLines(this string value)
    {
        return value
            .Replace("\r\n", "\n")
            .Replace("\n", Environment.NewLine);
    }

    public static string TrimNewLines(this string value)
    {
        return value.TrimEnd('\r', '\n');
    }
}