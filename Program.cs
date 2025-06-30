using LogMonitoringApplication.Models;
using LogMonitoringApplication.Services.Implementation;

Console.WriteLine("Log Monitoring Application starting...");

string currentExecutableDirectory = AppContext.BaseDirectory;
string solutionRootDirectory = Path.GetFullPath(Path.Combine(currentExecutableDirectory, "..", "..", ".."));
string logFilePath = Path.Combine(solutionRootDirectory, "Logs", "logs.log");

Console.WriteLine($"Attempting to parse log file at: '{logFilePath}'");

var logParser = new LogParser();
var jobProcessor = new JobProcessor();

try
{
    // Task 1: Parse the log file
    IEnumerable<LogEntry> entries = logParser.Parse(logFilePath);

    // Task 2: Identify and track jobs
    IReadOnlyList<Job> jobs = jobProcessor.ProcessLogEntries(entries);

    Console.WriteLine("\n--- Processed Jobs ---");
    foreach (var job in jobs)
    {
        Console.WriteLine(job);
    }
    Console.WriteLine("----------------------");
    Console.WriteLine($"Total processed jobs: {jobs.Count}");
}
catch (Exception ex)
{
    Console.Error.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
}

Console.WriteLine("\nLog Monitoring Application finished.");
