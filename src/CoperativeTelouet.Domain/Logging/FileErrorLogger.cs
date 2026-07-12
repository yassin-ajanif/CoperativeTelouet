using System.Text;

namespace CoperativeTelouet.Domain.Logging;

public sealed class FileErrorLogger : IErrorLogger
{
    private readonly string _filePath;
    private readonly object _gate = new();

    public FileErrorLogger(string filePath)
    {
        _filePath = filePath;
        var dir = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);
    }

    public static string ResolvePathFromConnectionString(string connectionString)
    {
        var dataSource = connectionString;
        const string prefix = "Data Source=";
        var idx = connectionString.IndexOf(prefix, StringComparison.OrdinalIgnoreCase);
        if (idx >= 0)
            dataSource = connectionString[(idx + prefix.Length)..].Trim().Trim('"');

        var dbDir = Path.GetDirectoryName(Path.GetFullPath(dataSource));
        if (string.IsNullOrEmpty(dbDir))
            dbDir = AppContext.BaseDirectory;

        return Path.Combine(dbDir, "app-errors.log");
    }

    public void LogError(Exception exception, AppLayer layer, string context)
        => Write(layer, context, FormatException(exception));

    private void Write(AppLayer layer, string context, string details)
    {
        var block = new StringBuilder()
            .AppendLine("------------------------------------------------------------")
            .AppendLine($"UTC: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}")
            .AppendLine($"Layer: {layer}")
            .AppendLine($"Context: {context}")
            .AppendLine(details)
            .AppendLine()
            .ToString();

        lock (_gate)
        {
            File.AppendAllText(_filePath, block, Encoding.UTF8);
        }
    }

    private static string FormatException(Exception exception)
    {
        var sb = new StringBuilder();
        var current = exception;
        var depth = 0;
        while (current is not null)
        {
            if (depth > 0)
                sb.AppendLine($"--- Inner ({depth}) ---");
            sb.AppendLine($"{current.GetType().FullName}: {current.Message}");
            if (!string.IsNullOrWhiteSpace(current.StackTrace))
                sb.AppendLine(current.StackTrace);
            current = current.InnerException;
            depth++;
        }

        return sb.ToString();
    }
}
