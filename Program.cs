using LogMonitoringApplication.Models;
using LogMonitoringApplication.Services.Implementation;
using LogMonitoringApplication.Services.Interfaces;

Console.WriteLine("Log Monitoring Application starting...");

string currentExecutableDirectory = AppContext.BaseDirectory;
string solutionRootDirectory = Path.GetFullPath(Path.Combine(currentExecutableDirectory, "..", "..", ".."));
string logFilePath = Path.Combine(solutionRootDirectory, "Logs", "logs.log");

Console.WriteLine($"Attempting to parse log file at: '{logFilePath}'");

var parser = new LogParser();

try
{
    // Parse the log file
    IEnumerable<LogEntry> entries = parser.Parse(logFilePath);

    Console.WriteLine("\n--- Successfully Parsed Log Entries ---");
    foreach (var entry in entries)
    {
        Console.WriteLine(entry.ToString());
    }
    Console.WriteLine("---------------------------------------");
    Console.WriteLine($"Total parsed entries: {entries.Count()}");
}
catch (Exception ex)
{
    Console.Error.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
}

Console.WriteLine("\nLog Monitoring Application finished.");
