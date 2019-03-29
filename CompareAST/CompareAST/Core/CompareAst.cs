namespace CompareAst.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class CompareAst
    {
        public ComparisonResult Compare(IParser parser, string lhsCode, string rhsCode, ITransform[] transforms, int minSubtreeSize)
        {
            var hasher = new Hasher();
            var transformer = new Transformer();
            var comparer = new Comparer();
            var resolver = new Resolver();

            var lhsAst = transformer.Transform(parser.Parse(lhsCode), transforms);
            var rhsAst = transformer.Transform(parser.Parse(rhsCode), transforms);

            var lhsHashedAst = hasher.Hash(lhsAst);
            var rhsHashedAst = hasher.Hash(rhsAst);

            var matches = comparer.Compare(lhsHashedAst, rhsHashedAst);
            var distinctMatches = resolver.Resolve(matches, minSubtreeSize);

            var lhsNodesMatched = distinctMatches.Sum(m => m.Lhs.Bfs().Count);
            var lhsNodes = lhsHashedAst.Bfs().Count;
            var rhsNodesMatched = distinctMatches.Sum(m => m.Rhs.Bfs().Count);
            var rhsNodes = rhsHashedAst.Bfs().Count;

            var lhsSimilarity = 1.0*lhsNodesMatched/lhsNodes;
            var rhsSimilarity = 1.0*rhsNodesMatched/rhsNodes;

            return new ComparisonResult
            {
                LhsSimilarity = lhsSimilarity,
                RhsSimilarity = rhsSimilarity,
                Matches = distinctMatches
            };
        }
    }

    public class ComparisonResult
    {
        public List<Match> Matches { get; set; } 
        public double LhsSimilarity { get; set; } 
        public double RhsSimilarity { get; set; } 
    }
}