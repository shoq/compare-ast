namespace CompareAst.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class Transformer
    {
        public Node Transform(Node tree, ITransform[] transforms)
        {
            // The root of the tree will not be transformed
            var newChildren = new List<Node>();
            foreach (var child in tree.Children)
            {
                var transform = transforms.FirstOrDefault(t => t.ShouldTransform(child));
                if (transform != null)
                {
                    var newChild = transform.Transform(child);
                    if (newChild != null)
                    {
                        newChildren.Add(Transform(newChild, transforms));
                    }
                }
                else
                {
                    newChildren.Add(Transform(child, transforms));
                }
            }

            tree.Children = newChildren.ToArray();
            return tree;
        }
    }
}