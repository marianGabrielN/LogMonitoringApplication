using LogMonitoringApplication.Enums;

namespace LogMonitoringApplication.Models;

/// <summary>
/// Represents a tracked job, including its start time, end time, and calculated duration.
/// </summary>
internal class Job(int processId, string description, TimeSpan startTime)
{
    /// <summary>
    /// The Process ID (PID) associated with the job.
    /// </summary>
    public int ProcessId { get; } = processId;

    /// <summary>
    /// The description of the job or task.
    /// </summary>
    public string Description { get; } = description ?? string.Empty;

    /// <summary>
    /// The timestamp when the job started.
    /// </summary>
    public TimeSpan StartTime { get; private set; } = startTime;

    /// <summary>
    /// The timestamp when the job finished. Null if the job is still running or incomplete.
    /// </summary>
    public TimeSpan? EndTime { get; private set; } = null;

    /// <summary>
    /// The duration of the job from start to finish. Null if the job is not yet completed.
    /// </summary>
    public TimeSpan? Duration => EndTime.HasValue ? EndTime.Value - StartTime : (TimeSpan?)null;

    /// <summary>
    /// The current status of the job (Running, Completed, Warning, Error, Incomplete, OrphanedEnd).
    /// </summary>
    public JobStatus Status { get; set; } = JobStatus.Running;
}
