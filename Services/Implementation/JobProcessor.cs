using LogMonitoringApplication.Enums;
using LogMonitoringApplication.Models;
using LogMonitoringApplication.Services.Interfaces;

namespace LogMonitoringApplication.Services.Implementation;

/// <summary>
/// Implements the IJobProcessor interface to track job start and end times
/// from a sequence of log entries.
/// </summary>
internal class JobProcessor : IJobProcessor
{
    private readonly TimeSpan _warningThreshold;
    private readonly TimeSpan _errorThreshold;

    public JobProcessor(int warningThresholdMinutes, int errorThresholdMinutes)
    {
        // Ensure error threshold is greater than or equal to warning threshold
        if (errorThresholdMinutes < warningThresholdMinutes)
        {
            throw new ArgumentException("Error threshold must be greater than or equal to warning threshold.");
        }

        _warningThreshold = TimeSpan.FromMinutes(warningThresholdMinutes);
        _errorThreshold = TimeSpan.FromMinutes(errorThresholdMinutes);
    }

    public IReadOnlyList<Job> ProcessLogEntries(IEnumerable<LogEntry> logEntries)
    {
        var runningJobs = new Dictionary<int, Job>();
        var processedJobs = new List<Job>();

        foreach (var entry in logEntries.OrderBy(e => e.ProcessId))
        {
            if (entry.Type == LogEntryType.START)
            {
                // Create a new Job instance and add it to running jobs
                var newJob = new Job(entry.ProcessId, entry.Description, entry.Timestamp);
                runningJobs.Add(entry.ProcessId, newJob);
            }
            else
            {
                if (runningJobs.TryGetValue(entry.ProcessId, out var runningJob))
                {
                    // Mark the job instance as complete and add it to processed jobs
                    runningJob.Complete(entry.Timestamp);
                    DetermineJobStatus(runningJob);
                    processedJobs.Add(runningJob);
                    runningJobs.Remove(entry.ProcessId);
                }
            }
        }

        foreach (var incompleteJob in runningJobs.Values)
        {
            processedJobs.Add(incompleteJob);
        }

        return processedJobs.AsReadOnly();
    }

    /// <summary>
    /// Determines and sets the final status of a completed job based on its duration and predefined thresholds.
    /// </summary>
    /// <param name="job">The job to evaluate.</param>
    private void DetermineJobStatus(Job job)
    {
        if (job.Duration.HasValue)
        {
            if (job.Duration.Value >= _errorThreshold)
            {
                job.Status = JobStatus.Error;
            }
            else if (job.Duration.Value >= _warningThreshold)
            {
                job.Status = JobStatus.Warning;
            }
            else
            {
                job.Status = JobStatus.Completed;
            }
        }
    }
}
