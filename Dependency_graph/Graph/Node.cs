using Dependency_graph.Visitor;

namespace Dependency_graph.Graph;

public class Node 
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Version { get; set; } = null!;
    
    public void Accept(INodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}