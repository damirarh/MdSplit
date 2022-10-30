using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MdSplit;

[Command(Description = "Split a markdown file into multiple files.")]
public class AppCommand
{
    [Required]
    [Option(Description = "Required. The markdown file to split.")]
    public string Input { get; set; } = null!;

    [Required]
    [Option(Description = "Required. The output directory.")]
    public string Output { get; set; } = null!;

    public int OnExecute(IConsole console)
    {
        try
        {
            var lines = File.ReadAllLines(this.Input);

            string? filename = null;
            StringBuilder contents = new();
            foreach (var line in lines)
            {
                if (line.StartsWith("# "))
                {
                    if (filename != null)
                    {
                        File.WriteAllText(Path.Combine(this.Output, filename), contents.ToString());
                        contents.Clear();
                    }
                    filename = string.Concat(line.AsSpan(2), ".md");
                }
                else
                {
                    contents.AppendLine(line);
                }
            }
            if (filename != null)
            {
                File.WriteAllText(Path.Combine(this.Output, filename), contents.ToString());
            }

            return 0;
        }
        catch (Exception ex)
        {
            console.WriteLine(ex);
            return 1;
        }
    }
}
