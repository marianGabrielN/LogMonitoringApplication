using LogMonitoringApplication.Models;
using LogMonitoringApplication.Services.Implementation;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();
builder
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();

Console.WriteLine("Log Monitoring Application starting...");

string currentExecutableDirectory = AppContext.BaseDirectory;
string solutionRootDirectory = Path.GetFullPath(Path.Combine(currentExecutableDirectory, "..", "..", ".."));
string logFilePath = Path.Combine(solutionRootDirectory, "Logs", "logs.log");

Console.WriteLine($"Attempting to parse log file at: '{logFilePath}'");

var logParser = new LogParser();
var jobProcessor = new JobProcessor(
    int.Parse(config["JobThresholds:WarningThresholdMinutes"]),
    int.Parse(config["JobThresholds:ErrorThresholdMinutes"]));
var reportGenerator = new ReportGenerator();

try
{
    // Task 1: Parse the log file
    IEnumerable<LogEntry> entries = logParser.Parse(logFilePath);

    // Task 2: Identify and track jobs, calculate durations, and apply thresholds
    IReadOnlyList<Job> jobs = jobProcessor.ProcessLogEntries(entries);

    // Task 3: Produce the report
    reportGenerator.GenerateReport(jobs);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
}

Console.WriteLine("\nLog Monitoring Application finished.");
