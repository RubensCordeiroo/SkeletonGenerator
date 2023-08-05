using SkeletonGenerator.Domain;
using System.IO;
using System.Reflection.Metadata;

namespace SkeletonGenerator;

class Program
{
    public static void Main(string[] args)
    {
        var directoryTS = @"D:\OneDrive\GitHub\SneburBR\Snebur.TS\src\Snebur.TS\src\Utilidade";
        var directoryDotnet = @"D:\OneDrive\GitHub\SneburBR\Snebur.Framework\src\Core\Utilidade";

        var destTs = @"D:\OneDrive\GitHub\SneburBR\snebur-ts\packages\core\src\util\";
        //var destDotnet = @"D:\OneDrive\GitHub\SneburBR\Snebur.Net\src\Core\src\Util";

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

        var dictionaryClasse = new Dictionary<string, List<TypeModel>>();

        foreach (var group in groups)
        {
            Console.WriteLine($"Processando: {String.Join(", ", group.Select(x => x.Name))}");

            var fileTs = group.FirstOrDefault(x => x.Extension.ToUpper() == ".TS");
            var fileCs = group.FirstOrDefault(x => x.Extension.ToUpper() == ".CS");

            var sources = new SourcesModel
            {
                SourceTs = fileTs != null ? TsParser.Parse(fileTs) : null,
                SourceCSharp = fileCs != null ? CSharpParser.Parse(fileCs) : null
            };


            foreach (var type in sources.SourceTypes)
            {
                dictionaryClasse.TryAdd(type.Name, new List<TypeModel>());
                dictionaryClasse[type.Name].Add(type);
            }
        }

        var skeletonsTypescript = SkeletonModelFactory.GetSkeletons(dictionaryClasse,
                                                                    Language.TypeScript,
                                                                    Language.CSharp);

        foreach (var skeleton in skeletonsTypescript)
        { 
            var skeletonString = SkeletonModelParser.Parse(skeleton);
            var fileDest = Path.Combine(destTs, skeleton.Name + ".skl");
            File.WriteAllText(fileDest, skeletonString);
            Console.WriteLine($"Arquivo gerado: {fileDest}");
        }
    }

    public static Dictionary<string, SourcesModel> GetAllPublicsClass(IEnumerable<IGrouping<string, FileInfo>> groups)
    {
        var result = new Dictionary<string, SourcesModel>();

        return result;
    }


}

