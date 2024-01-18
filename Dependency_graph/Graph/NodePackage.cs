namespace Dependency_graph.Graph;

public class NodePackage
{
    public string Name { get; set; } = null!;
    public string Version { get; set; } = null!;
    public int Id { get; set; } = -1;
    public List<NodePackage> InternalUsedNode { get; set; } = new List<NodePackage>();
}