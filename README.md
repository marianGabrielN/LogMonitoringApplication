# LogMonitoringApplication

This project was developed by **Marian-Gabriel Nechifor** for **London Stock Exchange Group** as part of a coding challenge.

## Overview

This is a .NET 8 console application designed to process a `logs.log` file. Its primary function is to:

*   Parse log entries to identify the start and end times of various jobs.
*   Calculate the duration of each job.
*   Generate a report flagging jobs that exceed specified time thresholds (warnings for over 5 minutes, errors for over 10 minutes).

## Getting Started

To run the application:

1.  Ensure you have the .NET 8 SDK installed.
2.  Navigate to the project's root directory in your terminal.
3.  Execute the following command: `dotnet run`

The application expects a `logs.log` file to be present in the `Logs` directory relative to the project root.
