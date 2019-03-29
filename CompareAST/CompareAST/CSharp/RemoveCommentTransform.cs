namespace CompareAst.CSharp
{
    using System.Linq;
    using Core;
    using Microsoft.CodeAnalysis.CSharp;

    public class RemoveCommentTransform : ITransform
    {
        private readonly int[] _commentNodes = new[]
        {
            (int)SyntaxKind.DocumentationCommentExteriorTrivia,
            (int)SyntaxKind.EndOfDocumentationCommentToken,
            (int)SyntaxKind.MultiLineCommentTrivia,
            (int)SyntaxKind.MultiLineDocumentationCommentTrivia,
            (int)SyntaxKind.SingleLineCommentTrivia,
            (int)SyntaxKind.SingleLineDocumentationCommentTrivia,
            (int)SyntaxKind.XmlComment,
            (int)SyntaxKind.XmlCommentEndToken,
            (int)SyntaxKind.XmlCommentStartToken
        }.ToArray();

        public bool ShouldTransform(Node subtree)
        {
            return _commentNodes.Contains(subtree.TypeId);
        }

        public Node Transform(Node subtree)
        {
            return null;
        }
    }
}
