namespace CompareAst.Core
{
    public class Node
    {
        public int TypeId;
        public Node[] Children = new Node[0];
        public bool IsLeaf => Children.Length == 0;
        public Span Span;
    }

    public class Span
    {
        public int Start;
        public int Length;
    }
}