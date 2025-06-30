namespace LogMonitoringApplication.Enums;

/// <summary>
/// Represents the type of a log entry, indicating whether a process started or ended.
/// </summary>
internal enum LogEntryType
{
    /// <summary>
    /// Indicates the start of a process.
    /// </summary>
    START,

    /// <summary>
    /// Indicates the end of a process.
    /// </summary>
    END
}
