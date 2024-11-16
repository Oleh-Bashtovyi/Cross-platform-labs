using McMaster.Extensions.CommandLineUtils;
using System.Reflection;

namespace Lab4.Commands;

[Command(Name = "version", Description = "Display version and author information.")]
public class VersionCommand
{
    private void OnExecute(IConsole console)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var version = assembly.GetName()?.Version?.ToString() ?? "Unknown version";

        console.WriteLine("Author: Bashtovyi Oleh");
        console.WriteLine($"Version: {version}");
    }
}
