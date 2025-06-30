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
    public string Description { get; } = description ?? throw new ArgumentNullException(nameof(description));

    /// <summary>
    /// The type of the log entry (START or END).
    /// </summary>
    public LogEntryType Type { get; } = type;

    /// <summary>
    /// The Process ID (PID) associated with the job.
    /// </summary>
    public int ProcessId { get; } = processId;
}
