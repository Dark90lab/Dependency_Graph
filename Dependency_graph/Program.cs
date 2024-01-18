using Dependency_graph.Graph;
using Dependency_graph.Visitor;

var nodePackagesToProcess = new List<NodePackage>()
{
    new NodePackage()
    {
        Name = "package-1",
        Version = "1.0.0",
        InternalUsedNode = new List<NodePackage>()
        {
            new NodePackage()
            {
                Name = "components",
                Version = "2.0.0"
            },
            new NodePackage()
            {
                Name = "sdk",
                Version = "1.2.0"
            },
        }
    },
    new NodePackage()
    {
        Name = "components",
        Version = "2.0.0",
        InternalUsedNode = new List<NodePackage>()
        {
            new NodePackage()
            {
                Name = "sdk",
                Version ="1.2.0"
            }
        }
    },
    new NodePackage()
    {
        Name = "sdk",
        Version = "1.2.0",
        
        InternalUsedNode = new List<NodePackage>()
        {
            new NodePackage()
            {
                Name = "package-1",
                Version ="1.0.0"
            }
        }
    }
};

INodeVisitor visitor = new MyNodeVisitor();

var graph = new Graph(nodePackagesToProcess);

graph.DepthFirstSearch(visitor);

    


