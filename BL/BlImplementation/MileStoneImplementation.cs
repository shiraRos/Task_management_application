

namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Net.NetworkInformation;

internal class MileStoneImplementation : IMileStone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(MileStone item)
    {
        ////// Group dependencies by DependenTask
        ////var groupedDependencies = _dal.Dependency.ReadAll().GroupBy(d => d.DependenTask)
        ////    .ToDictionary(g => g.Key, g => g.Select(d => d.DependensOnTask).ToList());

        ////// Order the groups by DependenTask
        ////var sortedGroup = groupedDependencies.OrderBy(kv => kv.Key)
        ////                              .Select(kv => (kv.Key, kv.Value.Select(d => d.).ToList()));
        //// Group dependencies by DependenTask
        //// Step 1: Create a grouped list
        //var groupedDependencies = _dal.Dependency.ReadAll().GroupBy(d => d.DependenTask)
        //                                      .ToDictionary(g => g.Key, g => g.Select(d => d.DependensOnTask).ToList());

        //// Step 2: Order the groups by DependenTask and transform to the desired format
        //var sortedGroups = groupedDependencies.OrderBy(kv => kv.Key)
        //                                      .Select(kv => new KeyValuePair<int, IEnumerable<int>>(kv.Key, kv.Value))
        //                                      .ToList();
        //var filteredGroups = groupedDependencies.SelectMany(g => g.Value).Distinct();

        //var milestonesWithPaths = filteredGroups.Select(value =>
        //{
        //    DO.Task? currentTask = _dal.Task.Read(value);
        //    // Replace this logic with your actual MileStone creation logic
        //    return new BO.MileStone
        //    {
        //        Id = currentTask?.Id ?? 0, // Set the milestone ID based on the value, handling nullability
        //        Alias = currentTask?.Alias,
        //        Description = currentTask?.Description,
        //        CreateAtDate = currentTask?.CreateAtDate ?? DateTime.MinValue,
        //        Status = (Status?)1,
        //        ForecastDate = null,
        //        DeadlineDate = currentTask?.DeadlineDate,
        //        CompleteDate = currentTask?.CompleteDate,
        //        completionPercentage = null, // Adjust this according to your logic
        //        Remarks = currentTask?.Remarks,
        //        Dependencies = sortedGroups
        //            .Where(group => group.Value.Contains(value))
        //            .Select(group => new TaskInList
        //            {
        //                Id = group.Key,
        //                Description = currentTask?.Description,
        //                Alias = currentTask?.Alias,
        //                Status = (Status?)1
        //            })
        //            .ToList()
        //    };
        //}).ToList();

        return 0;

    }


    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public MileStone? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
