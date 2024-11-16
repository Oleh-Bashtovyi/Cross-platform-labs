using McMaster.Extensions.CommandLineUtils;
using System.IO;

namespace Lab4.Commands;

[Command(Name = "set-path", Description = "Set input/output path.")]
public class SetPathCommand
{
    [Option("-p|--path", "Set LAB_PATH environment variable", CommandOptionType.SingleValue)]
    public string? InputPath { get; set; }

    const string ENVIRONMENT_LAB_PATH = "LAB_PATH";

    private void OnExecute(IConsole console)
    {
        if (string.IsNullOrEmpty(InputPath))
        {
            console.WriteLine("Please specify a path.");
            return; 
        }

        if (OperatingSystem.IsWindows())
        {
            Environment.SetEnvironmentVariable(ENVIRONMENT_LAB_PATH, InputPath, EnvironmentVariableTarget.User);
        }
        else
        {
            Environment.SetEnvironmentVariable(ENVIRONMENT_LAB_PATH, InputPath);
        }
        console.WriteLine($"{ENVIRONMENT_LAB_PATH} set to: {InputPath}");
    }
}
