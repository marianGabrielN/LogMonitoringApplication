using LogMonitoringApplication.Enums;
using LogMonitoringApplication.Models;
using LogMonitoringApplication.Services.Interfaces;

namespace LogMonitoringApplication.Services.Implementation;

internal class LogParser : ILogParser
{
    public IEnumerable<LogEntry> Parse(string logFilePath)
    {
        if (!File.Exists(logFilePath))
        {
            Console.WriteLine($"Warning: The log file was not found at: '{logFilePath}'");
            return [];
        }

        var logEntries = new List<LogEntry>();
        IEnumerable<string> lines = File.ReadLines(logFilePath);

        int lineNumber = 0;
        foreach (string line in lines)
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split(',');

            // Skip line if it doesn't have exactly 4 parts
            if (parts.Length != 4)
            {
                Console.WriteLine($"Warning: Line {lineNumber}: Expected 4 parts (Timestamp, Description, Type, PID), but found {parts.Length}. Skipping line: '{line}'");
                continue;
            }

            // Trim whitespace from each part
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();
            }

            try
            {
                // Parse Timestamp (HH:MM:SS)
                TimeSpan timestamp = TimeSpan.ParseExact(parts[0], "hh\\:mm\\:ss", null);

                // Description
                string description = parts[1];

                // Parse LogEntryType (case-insensitive for "START" or "END")
                LogEntryType type = (LogEntryType)Enum.Parse(typeof(LogEntryType), parts[2], ignoreCase: true);

                // Parse ProcessId (integer)
                int processId = int.Parse(parts[3]);

                logEntries.Add(new LogEntry(timestamp, description, type, processId));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Line {lineNumber}: An error occurred during parsing. Skipping line: '{line}'. Error: {ex.Message}");
                continue;
            }
        }

        return logEntries;
    }
}
