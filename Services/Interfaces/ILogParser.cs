using LogMonitoringApplication.Models;

namespace LogMonitoringApplication.Services.Interfaces;

internal interface ILogParser
{
    /// <summary>
    /// Parses the log file at the specified path and returns a collection of LogEntry objects.
    /// </summary>
    /// <param name="logFilePath">The full path to the log file.</param>
    /// <returns>An enumerable collection of parsed LogEntry objects.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the log file does not exist.</exception>
    IEnumerable<LogEntry> Parse(string logFilePath);
}
