using Dependency_graph.Graph;

namespace Dependency_graph.Visitor;

public class MyNodeVisitor : INodeVisitor
{
    public void Visit(Node node)
    {
        Console.WriteLine($"Visiting node: {node.Name} ({node.Id})");
    }
}