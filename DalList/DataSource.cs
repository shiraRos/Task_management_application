namespace Dal;


internal class DataSource
{
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
    internal static class Config
    {
        /// <summary>
        /// id for dependency entity
        /// </summary>
        internal const int startDepenId = 10;
        private static int nextDepenId = startDepenId;
        internal static int NextDepenId { get => nextDepenId++; }
        /// <summary>
        /// id for Task entity
        /// </summary>
        internal const int startTaskId = 200;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }
        /// <summary>
        /// start date
        /// </summary>
        internal static DateTime? startDate = null;
        /// <summary>
        /// complete date
        /// </summary>
        internal static DateTime? endDate = null;
    }


}
