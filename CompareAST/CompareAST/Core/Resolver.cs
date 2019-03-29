namespace CompareAst.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class Resolver
    {
        public const int MinimumMatchSize = 3;

        public List<Match> Resolve(List<Match> allFoundMatches, int minMatchSize)
        {
            // assert that LHS and RHS sizes are equal
            var matches = allFoundMatches
                .Where(m => m.Lhs.Size >= minMatchSize)
                .OrderByDescending(m => m.Lhs.Size)
                .ToList();

            var resolvedMatches = new List<Match>();

            while (matches.Any())
            {
                var nextMatch = matches.First();
                // assert lhs and rhs hashes are equal
                var subTreeHashes = nextMatch.Lhs.Bfs().Union(nextMatch.Rhs.Bfs()).ToList();
                matches.RemoveAll(m => subTreeHashes.Contains(m.Lhs) || subTreeHashes.Contains(m.Rhs));
                resolvedMatches.Add(nextMatch);
            }

            return resolvedMatches;
        } 
    }
}