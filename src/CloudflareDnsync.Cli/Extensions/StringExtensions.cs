namespace CloudflareDnsync.Cli.Extensions;

public static class StringExtensions
{
    public static string ToTimestampedString(this string value, string color = "blue", string scope = "CLI")
    {
        return $"[[{DateTime.Now:HH:mm:ss} [blue]{scope}[/]]] [bold][white]{value}[/][/]";
    }
}
