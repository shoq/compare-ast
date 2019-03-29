namespace CompareAst.Ui
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VisualPresenter
    {
        public const ConsoleColor DefaultBackground = ConsoleColor.White;
        public const ConsoleColor DefaultForeground = ConsoleColor.Black;
        public const int ConsoleSize = 45;

        private class ColorChange
        {
            public int Position;
            public ConsoleColor Color;
        }

        public void Present(SimilarityVm similarityVm)
        {
            Console.BackgroundColor = DefaultBackground;

            int lPos = 0;
            int rPos = 0;

            var lhs = similarityVm.Lhs.ToList();
            var rhs = similarityVm.Rhs.ToList();
            string lCode = GetCode(lhs);
            string rCode = GetCode(rhs);
            var lChanges = GetChanges(lhs);
            var rChanges = GetChanges(rhs);
                
            while (lPos < lCode.Length || rPos < rCode.Length)
            {
                Console.ForegroundColor = DefaultForeground;
                Console.Write("| ");
                lPos = WriteSome(lPos, lChanges, lCode);
                Console.ForegroundColor = DefaultForeground;
                Console.Write(" | ");
                rPos = WriteSome(rPos, rChanges, rCode);
                Console.ForegroundColor = DefaultForeground;
                Console.WriteLine(" |");
            }
        }

        private List<ColorChange> GetChanges(List<SimilarityBlock> blocks)
        {
            var lCode = 0;
            var changes = new List<ColorChange>();
            foreach (var lh in blocks)
            {
                changes.Add(new ColorChange
                {
                    Color = GetColorForKey(lh.SimilarityKey),
                    Position = lCode
                });
                lCode += GetCode(new List<SimilarityBlock> {lh}).Length;
            }

            return changes;
        }

        private string GetCode(List<SimilarityBlock> blocks)
        {
            return string.Join("", blocks.Select(b => b.Content))
                .Replace("\r\n", new string((char)1, 1))
                .Replace("\t", "    ");
        }
        
        private int WriteSome(int textPosition, List<ColorChange> colorChanges, string code)
        {
            int linePosition = 0;
            while (linePosition < ConsoleSize)
            {
                var currentColor = colorChanges.Last(c => c.Position <= textPosition);
                var nextColor = colorChanges.FirstOrDefault(c => c.Position> textPosition)?.Position?? int.MaxValue;

                Console.ForegroundColor = currentColor.Color;
                while (linePosition < ConsoleSize && textPosition < nextColor)
                {
                    if (textPosition >= code.Length)
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        if (code[textPosition] == (char)1)
                        {
                            Console.Write(new string(' ', ConsoleSize - linePosition));
                            linePosition = ConsoleSize;
                        }
                        else
                        {
                            Console.Write(code[textPosition]);
                        }
                        textPosition++;
                    }
                    linePosition++;
                }
            }
            return textPosition;
        }

        private ConsoleColor GetColorForKey(int key)
        {
            if (key == 0) return DefaultForeground;

            var colors = new Dictionary<int, ConsoleColor>
            {
                [1] = ConsoleColor.DarkGreen,
                [2] = ConsoleColor.DarkCyan,
                [3] = ConsoleColor.DarkRed,
                [4] = ConsoleColor.DarkMagenta,
                [5] = ConsoleColor.DarkYellow,
                [6] = ConsoleColor.Gray,
                [7] = ConsoleColor.DarkGray,
                [8] = ConsoleColor.Blue,
                [9] = ConsoleColor.Green,
                [10] = ConsoleColor.Cyan,
                [11] = ConsoleColor.Red,
                [12] = ConsoleColor.Magenta,
                [13] = ConsoleColor.Yellow,
                [14] = ConsoleColor.DarkBlue,
            };

            return colors[key%14 + 1];
        }
    }
}