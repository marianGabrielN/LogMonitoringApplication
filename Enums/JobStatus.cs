namespace LogMonitoringApplication.Enums;

/// <summary>
/// Represents the current status of a tracked job.
/// </summary>
internal enum JobStatus
{
    /// <summary>
    /// The job has started but not yet finished.
    /// </summary>
    Running,

    /// <summary>
    /// The job has completed successfully within thresholds.
    /// </summary>
    Completed,

    /// <summary>
    /// The job started but did not have a corresponding END entry.
    /// </summary>
    Incomplete,

    /// <summary>
    /// The job completed, but took longer than the warning threshold.
    /// </summary>
    Warning,

    /// <summary>
    /// The job completed, but took longer than the error threshold.
    /// </summary>
    Error,

    /// <summary>
    /// An 'END' log entry was found without a preceding 'START' for the same PID.
    /// </summary>
    OrphanedEnd
}
