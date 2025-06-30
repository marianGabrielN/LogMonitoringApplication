using LogMonitoringApplication.Models;

namespace LogMonitoringApplication.Services.Interfaces;

/// <summary>
/// Defines the contract for generating reports based on processed job data.
/// </summary>
internal interface IReportGenerator
{
    /// <summary>
    /// Generates and outputs a report for the given collection of jobs.
    /// </summary>
    /// <param name="jobs">A read-only list of processed Job objects.</param>
    void GenerateReport(IReadOnlyList<Job> jobs);
}
