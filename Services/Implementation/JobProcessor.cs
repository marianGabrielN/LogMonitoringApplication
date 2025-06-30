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
                    processedJobs.Add(runningJob);
                    runningJobs.Remove(entry.ProcessId);
                }
            }
        }

        return processedJobs.AsReadOnly();
    }
}
