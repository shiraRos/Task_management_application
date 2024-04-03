using BO;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PL;

/// <summary>
/// Class representing tasks available to assign to engineers
/// </summary>
internal class TasksToEngineer : IEnumerable
{
    static readonly IEnumerable<BO.TaskInEngineer> s_tsks =
(  BlApi.Factory.Get().Task.GetAvailableTasks() as IEnumerable<BO.TaskInEngineer>)!;
    /// <summary>
    /// Returns an enumerator that iterates through the collection of available tasks
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection</returns>
    public IEnumerator GetEnumerator() => s_tsks.GetEnumerator();
}


/// <summary>
/// Class representing engineers available for task assignment
/// </summary>
internal class EngineerToTask : IEnumerable
{
  static readonly IEnumerable<BO.EngineerInTask> s_engs =
(  BlApi.Factory.Get().Task.GetAllAvialbleEngineers(0,(EngineerExperience)0) as IEnumerable<BO.EngineerInTask>)!;
    /// <summary>
    /// Returns an enumerator that iterates through the collection of available engineers
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection</returns>
    public IEnumerator GetEnumerator() => s_engs.GetEnumerator();
}


/// <summary>
/// Class representing task dependencies options
/// </summary>
internal class TaksDependencies : IEnumerable
{
  static readonly IEnumerable<BO.TaskInList> s_tsks =
(  BlApi.Factory.Get().Task.GetAllDependenciesOptions() as IEnumerable<BO.TaskInList>)!;
    /// <summary>
    /// Returns an enumerator that iterates through the collection of task dependency options
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection</returns>
    public IEnumerator GetEnumerator() => s_tsks.GetEnumerator();
}


