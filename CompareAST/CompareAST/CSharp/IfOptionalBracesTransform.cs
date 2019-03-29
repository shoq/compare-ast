namespace CompareAst.CSharp
{
    using Core;
    using Microsoft.CodeAnalysis.CSharp;

    public class IfOptionalBracesTransform : ITransform
    {
        public bool ShouldTransform(Node subtree)
        {
            return subtree.TypeId == (int) SyntaxKind.IfStatement
                   && subtree.Children[1].TypeId == (int) SyntaxKind.Block
                   && subtree.Children[1].Children.Length == 1;
        }

        public Node Transform(Node subtree)
        {
            var oldSpan = subtree.Children[1].Span;
            subtree.Children[1] = subtree.Children[1].Children[0];
            subtree.Children[1].Span = oldSpan;
            return subtree;
        }
    }
}
