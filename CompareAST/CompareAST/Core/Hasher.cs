namespace CompareAst.Core
{
    using System.Linq;

    public class Hasher
    {
        public HashedNode Hash(Node tree)
        {
            if (tree.IsLeaf)
            {
                return new HashedNode
                {
                    Hash = tree.TypeId,
                    Size = 1,
                    Span = tree.Span
                };
            }
            else
            {
                var children = tree.Children.Select(Hash).ToArray();
                unchecked
                {
                    long hash = 0;
                    foreach (var child in children)
                    {
                        hash +=  31* child.Hash;
                    }
                    hash += tree.TypeId;

                    return new HashedNode
                    {
                        Children = children,
                        Hash = hash,
                        Size = children.Sum(c => c.Size) + 1,
                        Span = tree.Span
                    };
                }
            }
        }
    }
}