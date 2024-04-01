using BO;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PL;

internal class TasksToEngineer : IEnumerable
{
    static readonly IEnumerable<BO.TaskInEngineer> s_tsks =
(  BlApi.Factory.Get().Task.GetAvailableTasks() as IEnumerable<BO.TaskInEngineer>)!;

    public IEnumerator GetEnumerator() => s_tsks.GetEnumerator();
}

internal class EngineerToTask : IEnumerable
{
  static readonly IEnumerable<BO.EngineerInTask> s_engs =
(  BlApi.Factory.Get().Task.GetAllAvialbleEngineers(0,(EngineerExperience)0) as IEnumerable<BO.EngineerInTask>)!;

    public IEnumerator GetEnumerator() => s_engs.GetEnumerator();
}


internal class TaksDependencies : IEnumerable
{
  static readonly IEnumerable<BO.TaskInList> s_tsks =
(  BlApi.Factory.Get().Task.GetAllDependenciesOptions() as IEnumerable<BO.TaskInList>)!;

    public IEnumerator GetEnumerator() => s_tsks.GetEnumerator();
}


