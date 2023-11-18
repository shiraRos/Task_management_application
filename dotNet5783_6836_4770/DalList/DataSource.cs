namespace Dal;

internal class DataSource
{
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
    internal static class Config
    {
        //id for dependency entity
        internal const int startDepenId = 10;
        private static int nextDepenId = startDepenId;
        internal static int NextDepenId { get => nextDepenId++; }
        //id for Task entity
        internal const int startTaskId = 200;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
    }


}
