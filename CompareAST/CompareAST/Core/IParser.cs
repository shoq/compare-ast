namespace CompareAst.Core
{
    public interface IParser
    {
        Node Parse(string code);
    }
}