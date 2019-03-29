namespace CompareAst.Core
{
    using System.Collections.Generic;

    public class Comparer
    {
        public List<Match> Compare(HashedNode lhsTree, HashedNode rhsTree)
        {
            var lhsSubtrees = lhsTree.Bfs();
            var rhsSubtrees = rhsTree.Bfs();

            var matches = new List<Match>();
            foreach (var lhs in lhsSubtrees)
            {
                foreach (var rhs in rhsSubtrees)
                {
                    if (lhs.Hash == rhs.Hash)
                    {
                        matches.Add(new Match {Lhs = lhs, Rhs = rhs});
                    }
                }
            }

            return matches;
        } 
    }
}