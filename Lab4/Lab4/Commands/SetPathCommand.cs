using McMaster.Extensions.CommandLineUtils;
using System.IO;

namespace Lab4.Commands;

[Command(Name = "set-path", Description = "Set input/output path.")]
public class SetPathCommand
{
    [Option("-p|--path", "Set LAB_PATH environment variable", CommandOptionType.SingleValue)]
    public string? InputPath { get; set; }


    private void OnExecute(IConsole console)
    {
        if (!string.IsNullOrEmpty(InputPath))
        {
            Environment.SetEnvironmentVariable("LAB_PATH", InputPath);
            Console.WriteLine($"LAB_PATH set to: {InputPath}");
        }
        else
        {
            Console.WriteLine("Please specify a path.");
        }
    }
}
