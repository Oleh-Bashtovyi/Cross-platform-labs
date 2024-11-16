using LabLibrary;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Commands;


[Command(Name = "run", Description = "Run a specific lab")]
class RunCommand
{
    [Argument(0, "lab", "Specify lab to run (lab1)")]
    public string? Lab { get; set; }

    [Option("-I|--input", "Input file", CommandOptionType.SingleValue)]
    public string? InputFile { get; set; }

    [Option("-o|--output", "Output file", CommandOptionType.SingleValue)]
    public string? OutputFile { get; set; }


    const string DEFAULT_INPUT_FILE = "INPUT.TXT";
    const string DEFAULT_OUTPUT_FILE = "OUTPUT.TXT";
    const string ENVIRONMENT_LAB_PATH = "LAB_PATH";


    private void OnExecute(CommandLineApplication app, IConsole console)
    {
        //var envLabPath = Environment.GetEnvironmentVariable(ENVIRONMENT_LAB_PATH, EnvironmentVariableTarget.Machine);
        var envLabPath = Environment.GetEnvironmentVariable(ENVIRONMENT_LAB_PATH);

        Console.WriteLine($"Running Lab: {Lab}");
        //Console.WriteLine($"Running {Lab} with input: {InputFile}, output: {OutputFile}, LAB_PATH: {envLabPath}");

        if (string.IsNullOrEmpty(Lab))
        {
            console.WriteLine("No lab specified.");
            app.ShowHelp();
            return;
        }

        string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        string inputPath = InputFile ?? Path.Combine(envLabPath ?? homeDirectory, DEFAULT_INPUT_FILE);
        string outputPath = OutputFile ?? Path.Combine(envLabPath ?? homeDirectory, DEFAULT_OUTPUT_FILE);



        //if (string.IsNullOrEmpty(Lab))
        //{
        //    console.WriteLine("Error: No lab specified.");
        //    app.ShowHelp();
        //    return;
        //}

        //var labPath = GetLabDirectory(Lab);

        //if (labPath == null)
        //{
        //    Console.WriteLine($"Unknown lab '{Lab}'. Available labs: lab1, lab2, lab3");
        //    return;
        //}

        //string inputPath;

        //// Assign the input file path with a priority to InputFile
        //if (string.IsNullOrEmpty(InputFile))
        //{
        //    inputPath = Environment.GetEnvironmentVariable("LAB_PATH") ??
        //                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "INPUT.txt");

        //}
        //else
        //{
        //    inputPath = InputFile;
        //}

        //var outputPath = !string.IsNullOrEmpty(OutputFile) ? OutputFile : Path.Combine(labPath, "OUTPUT.txt");

        Console.WriteLine($"Environment variable {ENVIRONMENT_LAB_PATH}: {envLabPath}");
        Console.WriteLine($"INPUT: '{inputPath}'");
        Console.WriteLine($"OUTPUT: '{outputPath}'");


        if (!File.Exists(inputPath))
        {
            console.WriteLine($"Error: Input file not found at path '{inputPath}'");
            return;
        }


        try
        {
            switch (Lab.ToLower())
            {
                case "lab1":
                    Lab1.Run(inputPath, outputPath);
                    break;
                case "lab2":
                    Lab2.Run(inputPath, outputPath);
                    break;
                case "lab3":
                    Lab3.Run(inputPath, outputPath);
                    break;
                default:
                    Console.WriteLine("Unknown lab specified.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occured: {ex}");
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
