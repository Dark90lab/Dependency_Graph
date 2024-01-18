namespace Dependency_graph.Exception;

class CycleDetectedException : System.Exception
{
    public CycleDetectedException(string message) : base(message) { }
}