namespace CompareAst.Ui
{
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    public class SimilarityMapper
    {
        public SimilarityVm Map(List<Match> matches, string lhsCode, string rhsCode)
        {
            var numberedMatches = matches.Select((m, i) => new
            {
                Lhs = m.Lhs.Span,
                Rhs = m.Rhs.Span,
                SimilarityKey = i + 1
            }).ToList();

            var lhsMissings = ResolveMissingSpans(numberedMatches.Select(m => m.Lhs), lhsCode.Length).Select(miss => new
            {
                Lhs = miss,
                SimilarityKey = 0
            }).OrderBy(x=>x.Lhs.Start).ToArray();
            var lhsMatches = numberedMatches
                .Select(m => new
                {
                    m.Lhs,
                    m.SimilarityKey
                }).OrderBy(x => x.Lhs.Start).ToArray();

            var lhsBlocks = lhsMatches
                .Union(lhsMissings)
                .OrderBy(x => x.Lhs.Start)
                .Select(x => new SimilarityBlock
                {
                    SimilarityKey = x.SimilarityKey,
                    Content = lhsCode.Substring(x.Lhs.Start, x.Lhs.Length)
                }).ToArray();

            var rhsMissings = ResolveMissingSpans(numberedMatches.Select(m => m.Rhs), rhsCode.Length);
            var rhsBlocks = numberedMatches
                .Select(m => new
                {
                    m.Rhs,
                    m.SimilarityKey
                }).Union(rhsMissings.Select(miss => new
                {
                    Rhs = miss,
                    SimilarityKey = 0
                }))
                .OrderBy(x => x.Rhs.Start)
                .Select(x => new SimilarityBlock
                {
                    SimilarityKey = x.SimilarityKey,
                    Content = rhsCode.Substring(x.Rhs.Start, x.Rhs.Length)
                }).ToArray();

            return new SimilarityVm
            {
                Lhs = lhsBlocks,
                Rhs = rhsBlocks
            };
        }

        public IEnumerable<Span> ResolveMissingSpans(IEnumerable<Span> existingSpans, int maxLength)
        {
            var ordered = existingSpans.OrderBy(s => s.Start).ToList();
            if (!ordered.Any())
            {
                yield break;
            }

            if (ordered.First().Start != 0)
            {
                yield return new Span {Start = 0, Length = ordered.First().Start};
            }

            for (int i = 1; i < ordered.Count; i++)
            {
                yield return new Span
                {
                    Start = ordered[i-1].Start + ordered[i - 1].Length,
                    Length = ordered[i].Start - (ordered[i - 1].Start + ordered[i - 1].Length)
                };
            }

            var last = ordered.Last();
            var possibleNextStart = last.Start + last.Length;
            if (possibleNextStart < maxLength)
            {
                yield return new Span {Start = possibleNextStart, Length = maxLength - possibleNextStart};
            }
        } 
    }

    public class SimilarityVm
    {
        public SimilarityBlock[] Lhs;
        public SimilarityBlock[] Rhs;
    }

    public class SimilarityBlock
    {
        public int SimilarityKey = 0;
        public string Content;
    }
}