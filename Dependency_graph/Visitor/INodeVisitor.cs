using Dependency_graph.Graph;

namespace Dependency_graph.Visitor;

public interface INodeVisitor
{
    void Visit(Node node);
}