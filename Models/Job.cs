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

    /// <summary>
    /// Completes the job by setting its end time and updating its status.
    /// </summary>
    /// <param name="endTime">The timestamp when the job finished.</param>
    public void Complete(TimeSpan endTime)
    {
        if (Status == JobStatus.Running)
        {
            EndTime = endTime;
            Status = JobStatus.Completed;
        }
    }

    /// <summary>
    /// Provides a string representation of the job for debugging/display.
    /// </summary>
    /// <returns>A formatted string.</returns>
    public override string ToString()
    {
        string durationStr = Duration.HasValue ? Duration.Value.ToString("mm\\:ss") : "N/A";
        string endTimeStr = EndTime.HasValue ? EndTime.Value.ToString("hh\\:mm\\:ss") : "N/A";
        return $"PID: {ProcessId}, Desc: '{Description}', Start: {StartTime:hh\\:mm\\:ss}, End: {endTimeStr}, Duration: {durationStr}, Status: {Status}";
    }
}
