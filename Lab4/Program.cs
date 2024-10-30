using McMaster.Extensions.CommandLineUtils;
using Lab4;

namespace Lab_4;

[Command(Name = "Lab4", Description = "Console app for labs")]
[Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
class Program
{
    static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

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


[Command(Name = "version", Description = "Display version information.")]
class VersionCommand
{
    private void OnExecute(IConsole console)
    {
        console.WriteLine("Author: Bashtovyi Oleh");
        console.WriteLine("Version: 1.0.0");
    }
}


[Command(Name = "set-path", Description = "Set input/output path.")]
class SetPathCommand
{
    [Option("-p|--path", "Set LAB_PATH environment variable", CommandOptionType.SingleValue)]
    public string? Path { get; set; }

    private void OnExecute(IConsole console)
    {
        if (!string.IsNullOrEmpty(Path))
        {
            Environment.SetEnvironmentVariable("LAB_PATH", Path);
            console.WriteLine($"LAB_PATH set to: {Path}");
        }
        else
        {
            console.WriteLine("Please specify a path.");
        }
    }
}



[Command(Name = "run", Description = "Run a specific lab")]
class RunCommand
{
    [Argument(0, "lab", "Specify lab to run (lab1)")]
    public string? Lab { get; set; }

    [Option("-I|--input", "Input file", CommandOptionType.SingleValue)]
    public string? InputFile { get; set; }

    [Option("-o|--output", "Output file", CommandOptionType.SingleValue)]
    public string? OutputFile { get; set; }


    private void OnExecute(CommandLineApplication app, IConsole console)
    {
        if (string.IsNullOrEmpty(Lab))
        {
            console.WriteLine("Error: No lab specified.");
            app.ShowHelp();
            return;
        }

        var labPath = GetLabDirectory(Lab);

        if (labPath == null)
        {
            Console.WriteLine($"Unknown lab '{Lab}'. Available labs: lab1, lab2, lab3");
            return;
        }

        var inputPath = InputFile ?? Environment.GetEnvironmentVariable("LAB_PATH");
        var outputPath = OutputFile ?? Path.Combine(labPath, "OUTPUT.TXT");

        if (string.IsNullOrEmpty(inputPath))
        {
            inputPath = Path.Combine(labPath, "INPUT.TXT");
        }

        Console.WriteLine($"Environment LAB_PATH: {Environment.GetEnvironmentVariable("LAB_PATH")}");
        Console.WriteLine($"INPUT: '{inputPath}'");
        Console.WriteLine($"OUTPUT: '{outputPath}'");

        if (!File.Exists(inputPath))
        {
            console.WriteLine($"Error: Input file not found at path '{inputPath}'");
            return;
        }


        var runner = new LabRunner();

        switch (Lab.ToLower())
        {
            case "lab1":
                runner.RunLab1(inputPath, outputPath);
                break;
            case "lab2":
                runner.RunLab2(inputPath, outputPath);
                break;
            case "lab3":
                runner.RunLab3(inputPath, outputPath);
                break;
            default:
                Console.WriteLine("Unknown lab specified.");
                break;
        }
    }

    private string? GetLabDirectory(string labName)
    {
        string projectRoot = Directory.GetCurrentDirectory();


        switch (labName.ToLower())
        {
            case "lab1":
                return Path.Combine(projectRoot, "Lab1");
            case "lab2":
                return Path.Combine(projectRoot, "Lab2");
            case "lab3":
                return Path.Combine(projectRoot, "Lab3");
            default:
                return null; 
        }
    }
}

