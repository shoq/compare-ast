namespace CompareAst.CSharp
{
    using System.Linq;
    using Core;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class CSharpParser : IParser
    {
        public Node Parse(string code)
        {
            var ast = CSharpSyntaxTree.ParseText(code);
            var root = ast.GetRoot();
            return CreateNode(root);
        }

        public Node CreateNode(SyntaxNode node)
        {
            return new Node
            {
                Span = new Span
                {
                    Start = node.Span.Start,
                    Length = node.Span.Length
                },
                TypeId = (int) node.Kind(),
                Children = node.ChildNodes().Select(CreateNode).ToArray()
            };
        }
    }
}
