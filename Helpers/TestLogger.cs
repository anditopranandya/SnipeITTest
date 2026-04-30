using NUnit.Framework;

namespace SnipeItTest.Helpers;

public class TestLogger
{
    private readonly string _filePath;
    private readonly List<string> _lines = new();

    public TestLogger()
    {
        // create a logs folder if it doesn't exist
        var logsDir = Path.Combine(AppContext.BaseDirectory, "Logs");
        Directory.CreateDirectory(logsDir);

        // file name format: Test Run - DDMMYYYY HhMmSs
        var timestamp = DateTime.Now.ToString("ddMMyyyy Hhmmss");
        _filePath = Path.Combine(logsDir, $"Test Run - {timestamp}.txt");
    }

    public void Log(string message)
    {
        var line = $"[{DateTime.Now:HH:mm:ss}] {message}";
        _lines.Add(line);
        TestContext.WriteLine(line);
    }

    public void LogSeparator()
    {
        _lines.Add(new string('-', 60));
    }

    public void Save()
    {
        File.WriteAllLines(_filePath, _lines);
        TestContext.WriteLine($"Log saved to: {_filePath}");
    }
}
