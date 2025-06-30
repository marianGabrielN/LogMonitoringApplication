using LogMonitoringApplication.Enums;

namespace LogMonitoringApplication.Models;

/// <summary>
/// Represents a single entry parsed from the log file.
/// </summary>
internal class LogEntry(TimeSpan timestamp, string description, LogEntryType type, int processId)
{
    /// <summary>
    /// The timestamp of the log entry (HH:MM:SS), represented as a TimeSpan from midnight.
    /// </summary>
    public TimeSpan Timestamp { get; } = timestamp;

    /// <summary>
    /// The description of the job or task.
    /// </summary>
    public string Description { get; } = description ?? string.Empty;

    /// <summary>
    /// The type of the log entry (START or END).
    /// </summary>
    public LogEntryType Type { get; } = type;

    /// <summary>
    /// The Process ID (PID) associated with the job.
    /// </summary>
    public int ProcessId { get; } = processId;

    /// <summary>
    /// Provides a string representation of the log entry for debugging/display.
    /// </summary>
    /// <returns>A formatted string.</returns>
    public override string ToString()
    {
        return $"[{Timestamp:hh\\:mm\\:ss}] PID: {ProcessId}, Type: {Type}\t, Desc: {Description}";
    }
}
