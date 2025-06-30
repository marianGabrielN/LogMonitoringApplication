using LogMonitoringApplication.Models;

namespace LogMonitoringApplication.Services.Interfaces;

/// <summary>
/// Defines the contract for processing log entries to track job start/end times and durations.
/// </summary>
internal interface IJobProcessor
{
    /// <summary>
    /// Processes a collection of log entries to identify and track jobs.
    /// It matches START and END entries by Process ID (PID) and calculates job durations.
    /// </summary>
    /// <param name="logEntries">An enumerable collection of parsed LogEntry objects.</param>
    /// <returns>A list of all processed jobs, including completed, incomplete, and orphaned end entries.</returns>
    /// 
    IReadOnlyList<Job> ProcessLogEntries(IEnumerable<LogEntry> logEntries);
}
