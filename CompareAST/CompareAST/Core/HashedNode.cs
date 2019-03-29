namespace CompareAst.Core
{
    using System.Collections.Generic;

    public class HashedNode
    {
        public HashedNode[] Children = new HashedNode[0];
        public long Hash;
        public int Size;
        public Span Span;

        public List<HashedNode> Bfs()
        {
            var list = new List<HashedNode> { this };
            int currentIndex = 0;

            while (currentIndex < list.Count)
            {
                var node = list[currentIndex];
                var children = node.Children;
                list.AddRange(children);
                ++currentIndex;
            }

            return list;
        }
    }
}