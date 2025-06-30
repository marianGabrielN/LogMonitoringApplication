using LogMonitoringApplication.Enums;
using LogMonitoringApplication.Models;
using LogMonitoringApplication.Services.Interfaces;

namespace LogMonitoringApplication.Services.Implementation;

/// <summary>
/// Implements the IReportGenerator interface to produce a console-based report
/// of processed jobs, highlighting warnings and errors.
/// </summary>
internal class ReportGenerator : IReportGenerator
{
    public void GenerateReport(IReadOnlyList<Job> jobs)
    {
        Console.WriteLine("\n--- Job Processing Report ---");
        Console.WriteLine("-----------------------------");

        if (!jobs.Any())
        {
            Console.WriteLine("No jobs to report.");
            return;
        }

        // Sort jobs for better readability
        var sortedJobs = jobs.OrderBy(j => j.ProcessId).ThenBy(j => j.StartTime).ToList();

        foreach (var job in sortedJobs)
        {
            string statusIndicator = string.Empty;
            ConsoleColor originalColor = Console.ForegroundColor;

            switch (job.Status)
            {
                case JobStatus.Running:
                    statusIndicator = "[RUNNING]";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case JobStatus.Completed:
                    statusIndicator = "[OK]";
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case JobStatus.Warning:
                    statusIndicator = "[WARNING]";
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case JobStatus.Error:
                    statusIndicator = "[ERROR]";
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            string paddedStatus = statusIndicator.PadRight(16);
            Console.WriteLine($"{paddedStatus} PID: {job.ProcessId}, Desc: '{job.Description}'");

            Console.ForegroundColor = originalColor;
        }

        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Total jobs processed: {jobs.Count}");
        Console.WriteLine($"  Completed: {jobs.Count(j => j.Status == JobStatus.Completed)}");
        Console.WriteLine($"  Warnings: {jobs.Count(j => j.Status == JobStatus.Warning)}");
        Console.WriteLine($"  Errors: {jobs.Count(j => j.Status == JobStatus.Error)}");
        Console.WriteLine("-----------------------------");
    }
}
