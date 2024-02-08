using Newtonsoft.Json.Linq;
public class Program
{
    public static void Main(string[] args)
    {
        string directoryPath = @"INPUT_PATH"; // Update with the path to your directory

        foreach (string filePath in Directory.GetFiles(directoryPath, "*.json"))
        {
            Console.WriteLine("Doing: " + filePath);
            ProcessJsonFile(filePath);
            Console.WriteLine("Done: " + filePath);
        }
        Console.WriteLine("Already Done");
    }

    public static void ProcessJsonFile(string filePath)
    {
        string json = File.ReadAllText(filePath);

        var jsonObjectArray = JArray.Parse(json);

        var distinctProperties = GetDistinctProperties(jsonObjectArray);

        string className = Path.GetFileNameWithoutExtension(filePath);
        string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), className + ".cs");

        GenerateCSharpClass(distinctProperties, outputFilePath, className);
    }

    public static List<string> GetDistinctProperties(JArray jsonArray)
    {
        var distinctProperties = new List<string>();

        foreach (JObject obj in jsonArray)
        {
            foreach (var property in obj.Properties())
            {
                if (!distinctProperties.Contains(property.Name))
                {
                    distinctProperties.Add(property.Name);
                }
            }
        }

        return distinctProperties;
    }

    public static void GenerateCSharpClass(List<string> properties, string filePath, string className)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"public class {className}");
            writer.WriteLine("{");

            foreach (var property in properties)
            {
                writer.WriteLine($"    public string {property} {{ get; set; }}");
            }

            writer.WriteLine("}");
        }
    }
}
namespace JSONtoCsharpConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
