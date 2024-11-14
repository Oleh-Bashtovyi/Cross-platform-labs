using McMaster.Extensions.CommandLineUtils;
using LabLibrary;
using Lab4.Commands;

namespace Lab_4;

[Command(Name = "Lab4", Description = "Console app for labs")]
[Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
class Program
{
    static int Main(string[] args)
    {
        try
        {
            CommandLineApplication.Execute<Program>(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erroro occured: {ex.Message}");
        }

        return 1;
    }

    private void OnExecute()
    {
        Console.WriteLine("Specify a command");
    }

    private void OnUnknownCommand(CommandLineApplication app)
    {
        Console.WriteLine("Unknown command. Use one of the following:");
        Console.WriteLine(" - version: Displays app version and author");
        Console.WriteLine(" - run: Run a specific lab");
        Console.WriteLine(" - set-path: Set input/output path");
    }
}






