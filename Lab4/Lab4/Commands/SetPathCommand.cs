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
            Environment.SetEnvironmentVariable(ENVIRONMENT_LAB_PATH, InputPath, EnvironmentVariableTarget.Machine);
            console.WriteLine($"LAB_PATH set to: {InputPath}");
        }
        else
        {
            console.WriteLine("Command available only on windows.");
        }
        //else
        //{
            //var labPath = InputPath;

            //// Set the environment variable for the current session
            //Environment.SetEnvironmentVariable(ENVIRONMENT_LAB_PATH, labPath);

            //// Save the environment variable in .zshrc to make it permanent
            //var zshrcPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".bashrc");

            //Console.WriteLine(zshrcPath);
            //// Append the export command to .zshrc
            //File.AppendAllText(zshrcPath, $"\nexport LAB_PATH=\"{labPath}\"\n");

            //Console.WriteLine($"LAB_PATH set to {labPath} and saved to .zshrc");

            //// Manually load the variable into the current C# environment
            //// Parsing .zshrc to update the current environment variable
            //var lines = File.ReadAllLines(zshrcPath);
            //foreach (var line in lines.Where(line => line.StartsWith("export LAB_PATH=")))
            //{
            //    var value = line.Split('=')[1].Trim('"');
            //    Environment.SetEnvironmentVariable("LAB_PATH", value);
            //    Console.WriteLine($"LAB_PATH loaded into current session as: {value}");
            //}


            // Set the environment variable for the current session
            //Environment.SetEnvironmentVariable(ENVIRONMENT_LAB_PATH, InputPath);

            //// Determine the shell configuration file
            //var shellProfile = Environment.GetEnvironmentVariable("SHELL")?.Contains("zsh") == true
            //    ? ".zshrc"
            //    : ".bashrc";

            //var profilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), shellProfile);

            //// Append the export command if it's not already present
            //var exportCommand = $"\nexport {ENVIRONMENT_LAB_PATH}=\"{InputPath}\"\n";

            //if (!File.ReadAllText(profilePath).Contains(exportCommand.Trim()))
            //{
            //    File.AppendAllText(profilePath, exportCommand);
            //    Console.WriteLine($"{ENVIRONMENT_LAB_PATH} set to {InputPath} and saved to {shellProfile}");
            //}
            //else
            //{
            //    Console.WriteLine($"{ENVIRONMENT_LAB_PATH} already set in {shellProfile}");
            //}
        //}
    }
}
