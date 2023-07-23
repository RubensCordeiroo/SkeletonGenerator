using ExtractPublicMethods;
using SkeletonGenerator.Domain;
using System.Reflection.Metadata;

class Program
{
    public static void Main(string[] args)
    {
        var directoryTS = @"D:\OneDrive\GitHub\SneburBR\Snebur.TS\src\Snebur.TS\src\Utilidade";
        var directoryDotnet = @"D:\OneDrive\GitHub\SneburBR\Snebur.Framework\src\Core\Utilidade";
        
        var destTs = @"D:\OneDrive\GitHub\SneburBR\Snebur.TS\src\Snebur.TS\src\Utilidade\generated";
        var destDotnet = @"D:\OneDrive\GitHub\SneburBR\Snebur.Framework\src\Core\Utilidade\generated";

        if (!Directory.Exists(directoryTS))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Diretório não encontrado: " + directoryTS);
            return;
        }

        if (!Directory.Exists(directoryDotnet))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Diretório não encontrado: " + directoryDotnet);
            return;
        }

        Console.ForegroundColor = ConsoleColor.White;


        if (!Directory.Exists(directoryTS))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Diretório não encontrado: " + directoryTS);
            return;
        }

        if (!Directory.Exists(directoryDotnet))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Diretório não encontrado: " + directoryDotnet);
            return;
        }

        Console.ForegroundColor = ConsoleColor.White;


        var filesTS = Directory.GetFiles(directoryTS, "*.ts", SearchOption.AllDirectories)
                               .Select(x => new FileInfo(x));

        var filesCore = Directory.GetFiles(directoryDotnet, "*.cs", SearchOption.AllDirectories)
                                 .Select(x => new FileInfo(x));

        var allFiles = filesTS.Concat(filesCore).ToArray();
        var groups = allFiles.GroupBy(x => Path.GetFileNameWithoutExtension(x.Name).ToUpper());

        foreach (var group in groups)
        {
            var fileTs = group.FirstOrDefault(x => x.Extension.ToUpper() == ".TS");
            var fileCs = group.FirstOrDefault(x => x.Extension.ToUpper() == ".CS");

            var sources = new SourcesModel
            {
                SourceTs = fileTs != null ? TsParser.Parse(fileTs) : null,
                SourceCSharp = fileCs != null ? CSharpParser.Parse(fileCs) : null
            };

            //var fileName = Path.GetFileNameWithoutExtension(fileTs.Name);
            //var dest = Path.Combine(destTs, fileTs.Name);
            //var skeleton = SkeletonParser.Parse(sources);
            throw new NotImplementedException();


        }

    }

    public static Dictionary<string, SourcesModel> GetAllPublicsClass(IEnumerable<IGrouping<string, FileInfo>> groups)
    {
        var result = new Dictionary<string, SourcesModel>();
      
        return result;
    }

    
}

