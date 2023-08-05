using SkeletonGenerator.Domain;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace SkeletonGenerator;

class TsParser
{
    internal static SourceModel Parse(FileInfo fileTs)
    {
        var nodePath = @"C:\Program Files\nodejs\node.exe";
        var tsParserPath = Path.GetFullPath("../../../../TsParser/dist/main.js");
        var arguments = $"\"{fileTs.FullName}\"";

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = nodePath,
                Arguments = tsParserPath + " " + arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        var output = new StringBuilder();
        process.OutputDataReceived += (sender, args) => output.AppendLine(args.Data);
        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit();

        var result = output.ToString();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var source = JsonSerializer.Deserialize<SourceModel>(result, options);
        if(source is null)
        {
            throw new Exception("Failed to parse TypeScript file");
        }
        return source;
    }
}

