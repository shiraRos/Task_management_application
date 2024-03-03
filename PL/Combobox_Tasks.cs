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


