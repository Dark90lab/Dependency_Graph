using Dependency_graph.Graph;

namespace Dependency_graph.Helpers;

public static class GraphHelpers
{
    private static void TraverseNodePackages(List<NodePackage> nodePackages, Dictionary<string,int> uniquePackages)
    {
        foreach (var nodePackage in nodePackages)
        {
            TraverseNodePackages(nodePackage.InternalUsedNode, uniquePackages);
         

            if (!uniquePackages.ContainsKey($"{nodePackage.Name}@{nodePackage.Version}"))
            {
                var maxValue = uniquePackages.Count() == 0 ? 0 : uniquePackages.Values.Max() + 1;
                uniquePackages.Add($"{nodePackage.Name}@{nodePackage.Version}",maxValue);
                nodePackage.Id = maxValue;
            }
            else
            {
                nodePackage.Id = uniquePackages[$"{nodePackage.Name}@{nodePackage.Version}"];
            }
        }
    }

    public static Dictionary<string, int> GetNodePackagesDictionary(List<NodePackage> nodePackages)
    {
        Dictionary<string,int> uniquePackages = new Dictionary<string, int>();

        TraverseNodePackages(nodePackages, uniquePackages);

        return uniquePackages;
    }
}