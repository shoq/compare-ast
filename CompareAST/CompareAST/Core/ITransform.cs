namespace CompareAst.Core
{
    public interface ITransform
    {
        bool ShouldTransform(Node subtree);
        Node Transform(Node subtree);
    }
}