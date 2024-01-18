using Dependency_graph.Exception;
using Dependency_graph.Helpers;
using Dependency_graph.Visitor;

namespace Dependency_graph.Graph;

public class Graph
{
    private readonly Dictionary<Node, List<Node>> _adjacencyList;
    
    public Graph(List<NodePackage> nodePackages)
    {
        _adjacencyList = new Dictionary<Node, List<Node>>();
        GraphHelpers.GetNodePackagesDictionary(nodePackages);
        BuildGraph(nodePackages);
    }

    private void BuildGraph(List<NodePackage> nodePackages, Node? parent = null )
    {
        foreach (var nodePackage in nodePackages)
        {
            if (!NodeExists(nodePackage.Id))
            {
                AddNode(new Node() { Id = nodePackage.Id, Name = nodePackage.Name, Version = nodePackage.Version });
            }

            var currentNode = GetNodeById(nodePackage.Id);

            if (parent != null)
            {
                AddEdge(parent, currentNode);
            }
            
            BuildGraph(nodePackage.InternalUsedNode, currentNode);
        }
    }
    
    
    public void DepthFirstSearch(INodeVisitor visitor)
    {
        HashSet<Node> visited = new HashSet<Node>();
        HashSet<Node> recursionStack = new HashSet<Node>();

        foreach (var node in _adjacencyList.Keys)
        {
            if (!visited.Contains(node))
            {
                DFSHelper(node, visited, recursionStack, visitor);
            }
        }
    }

    private void DFSHelper(Node currentNode, HashSet<Node> visited, HashSet<Node> recursionStack, INodeVisitor visitor)
    {
        visited.Add(currentNode);
        recursionStack.Add(currentNode);

        currentNode.Accept(visitor);

        foreach (var neighbor in _adjacencyList[currentNode])
        {
            if (!visited.Contains(neighbor))
            {
                DFSHelper(neighbor, visited, recursionStack, visitor);
            }
            else if (recursionStack.Contains(neighbor))
            {
                throw new CycleDetectedException($"Cycle detected in the graph involving nodes {currentNode.Name} and {neighbor.Name}");
            }
        }

        recursionStack.Remove(currentNode);
    }
    
    
   private Node GetNodeById(int nodeId)
    {
        foreach (var node in _adjacencyList.Keys)
        {
            if (node.Id == nodeId)
            {
                return node;
            }
        }

        return null; // Node with the specified Id not found
    }
    
    
    public bool NodeExists(int Id)
    {
        foreach (var node in _adjacencyList.Keys)
        {
            if (node.Id == Id)
            {
                return true;
            }
        }

        return false;
    }
    
    private void AddNode(Node node)
    {
        if (!_adjacencyList.ContainsKey(node))
        {
            _adjacencyList[node] = new List<Node>();
        }
    }

    private void AddEdge(Node source, Node destination)
    {
        if (!_adjacencyList.ContainsKey(source) || !_adjacencyList.ContainsKey(destination))
        {
            throw new ArgumentException("Source or destination node not found in the graph.");
        }

        _adjacencyList[source].Add(destination);
    }
    
}