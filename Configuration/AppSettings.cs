namespace LogMonitoringApplication.Configuration;

/// <summary>
/// Represents the application settings, including job duration thresholds.
/// </summary>
internal class AppSettings
{
    /// <summary>
    /// The section name for JobThresholds in configuration files.
    /// </summary>
    public const string JobThresholdsSectionName = "JobThresholds";

    /// <summary>
    /// Gets or sets the duration (in minutes) after which a job is considered to have a warning.
    /// </summary>
    public int WarningThresholdMinutes { get; set; }

    /// <summary>
    /// Gets or sets the duration (in minutes) after which a job is considered to have an error.
    /// </summary>
    public int ErrorThresholdMinutes { get; set; }
}
