namespace CompareAst.Ui
{
    using System;
    using System.IO;
    using Core;
    using CSharp;

    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 2 || args.Length > 5)
            {
                Console.WriteLine("Niewłaściwa liczba parametrów.");
                Console.WriteLine();
                Console.WriteLine("Użycie:");
                Console.WriteLine();
                Console.WriteLine("compare-ast plik1 plik2 [-w [-s n]]");
                Console.WriteLine();
                Console.WriteLine("plik1\tścieżka do pierwszego pliku do porównania");
                Console.WriteLine("plik2\tścieżka do drugiego pliku do porównania");
                Console.WriteLine("-w\tWyświetlenie podobieństw do wizualnej analizy");
                Console.WriteLine("-s n\tUżycie zadanego minimalnego rozmiaru dopasowanego poddrzewa");
                return;
            }

            var lhsCode = ReadFile(args[0]);
            var rhsCode = ReadFile(args[1]);
            bool displayVisual = args.Length > 2 && args[2] == "-w";

            bool withSubtreeSize = args.Length == 5 && args[3] == "-s";
            int minSubtreeSize = withSubtreeSize ? int.Parse(args[4]) : Resolver.MinimumMatchSize;

            var result = new CompareAst().Compare(new CSharpParser(), lhsCode, rhsCode, new ITransform[]
            {
                new IfOptionalBracesTransform(),
                new RemoveCommentTransform()
            }, minSubtreeSize);

            Console.WriteLine("Stopień podobieństwa w pierwszym pliku wynosi {0:f2}%", 100 * result.LhsSimilarity);
            Console.WriteLine("Stopień podobieństwa w drugim pliku wynosi {0:f2}%", 100 * result.RhsSimilarity);

            if (displayVisual)
            {
                Console.WriteLine();
                var similarities = new SimilarityMapper().Map(result.Matches, lhsCode, rhsCode);
                new VisualPresenter().Present(similarities);
            }
        }

        private static string ReadFile(string name)
        {
            try
            {
                return File.ReadAllText(name);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Nie można znaleźć pliku " + name);
                Environment.Exit(100);
            }

            throw new InvalidOperationException();
        }
    }
}
