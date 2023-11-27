﻿namespace DalTest;

using Dal;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;

/// <summary>
/// class initialization of all the entities
/// </summary>
public static class Initialization
{
    //declaration  the objects of their entities
    private static IEngineer? e_dalEngineer;
    private static IDependency? d_dalDependency;
    private static ITask? t_dalTask;
    //Declaration random object in order to initializ by random way
    private static readonly Random s_rand = new();
    //create objects Engineer type
    private static void createEngineer()
    {
        //create array for the names of engineer
        string[] EngineerNames =
        {
            "Kewin Klein", "Dan Segal", "Dave Jos","noya go","kodi hh",
            "Barbara Sol", "Alice Weiss", "Sara Finlan","jonatan von",
        };
        //initialize all the details of engineer by goes through the array
        foreach (var _name in EngineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(200000000, 400000000);
            while (e_dalEngineer!.Read(_id) != null);
            EngineerExperience _level = (EngineerExperience)s_rand.Next(0, 4);
            double? _cost = s_rand.Next(1000, 400000);
            string? _email = _name + "@gmail.com";
            //creating a new object
            Engineer newEng = new(_id, _level, _cost, _name, _email);
            //Add to data by calling create operation
            _id = e_dalEngineer!.Create(newEng);
        }
    }
    //create objects Dependenct type
    private static void createDependency()
    {
        int otomatId = 0, _dependenTask, nextRanTask;
        int dep1 = GetRanTask().Id, dep2 = GetRanTask().Id;
        //creating the first dependency outside the loop
        _dependenTask = GetRanTask().Id;
        nextRanTask = GetRanTask().Id;
        //loop for checking the dependency
        while (nextRanTask == _dependenTask || d_dalDependency!.ReadAll().Exists(dep => dep.DependenTask == _dependenTask && dep.DependensOnTask == nextRanTask) || d_dalDependency!.ReadAll().Exists(dep => dep.DependenTask == nextRanTask && dep.DependensOnTask == _dependenTask))
            nextRanTask = GetRanTask().Id;
        int _dependensOnTask = nextRanTask;
        //creating a new object
        Dependency newDpn = new(otomatId, _dependenTask, _dependensOnTask);
        //Add to data by calling  cerate operation
        otomatId = d_dalDependency!.Create(newDpn);
        //a loop for crating 2 different dependencies with the same depend on task
        for (int i = 0; i < 3; i++)
        {
            nextRanTask = GetRanTask().Id;
            //loop for checking the dependency
            while (nextRanTask == dep1 || nextRanTask == dep2 || d_dalDependency!.ReadAll().Exists(dep => dep.DependenTask == dep1 && dep.DependensOnTask == nextRanTask) || d_dalDependency!.ReadAll().Exists(dep => dep.DependenTask == dep2 && dep.DependensOnTask == nextRanTask) || d_dalDependency!.ReadAll().Exists(dep => dep.DependenTask == nextRanTask && dep.DependensOnTask == dep1) || d_dalDependency!.ReadAll().Exists(dep => dep.DependenTask == nextRanTask && dep.DependensOnTask == dep2))
                nextRanTask = GetRanTask().Id;
            //creating a new object
            Dependency newDpn1 = new(otomatId, dep1, nextRanTask);
            Dependency newDpn2 = new(otomatId, dep2, nextRanTask);
            //Add to data by calling  cerate operation
            otomatId = d_dalDependency!.Create(newDpn1);
            otomatId = d_dalDependency!.Create(newDpn2);

        }
        //a loop for the rest of the regular deendencies
        for (int i = 0; i < 33; i++)
        {
            //initialize the dependendTasks by random id -that exist in the task list
            _dependenTask = GetRanTask().Id;
            nextRanTask = GetRanTask().Id;
            //loop for checking the dependency
            while (nextRanTask == _dependenTask || d_dalDependency!.ReadAll().Exists(dep => dep.DependenTask == _dependenTask && dep.DependensOnTask == nextRanTask) || d_dalDependency!.ReadAll().Exists(dep => dep.DependenTask == nextRanTask && dep.DependensOnTask == _dependenTask))
                nextRanTask = GetRanTask().Id;
            //creating a new object
            Dependency newDpn3 = new(otomatId, _dependenTask, nextRanTask);
            //Add to data by calling  cerate operation
            otomatId = d_dalDependency!.Create(newDpn3);
        }

    }
    //External helper function to draw a random dependency assignment
    private static Task GetRanTask()
    {
        //declaretioN of Task arr in size of existing tasks
        Task[] tempArr = new Task[20];
        //converting the list to array
        tempArr = t_dalTask!.ReadAll().ToArray();
        //Return random index that contains item
        int temp = s_rand.Next(0, 19);
        return tempArr[temp];
    }
    //Create objects Task type
    private static void createTask()
    {

        for (int i = 0; i < 20; i++)
        {
            int otamtId = 0;
            //get ENgineer id from the random function 
            int _idEngineer = GetRanEng().Id;
            bool? _isMileston = false;
            //Lottery several days to add
            int daysToAdd = s_rand.Next(0, 10);
            DateTime? _startDate = null;
            //Defining a deadline according to the number of days drawn
            DateTime? _deadlineDate = DateTime.Today.AddDays(daysToAdd + 1);
            //Definition that the project ended a day before the deadline for each project
            DateTime? _completeDate = null;
            DateTime? _scheduledDate = null;
            TimeSpan? _requiredEffortTime = null;
            string? _alias = "tsk" + s_rand.Next(32, 122);
            string? _deliverables = null;
            string? _remarks = null;
            EngineerExperience? _complexityLevel = (EngineerExperience)s_rand.Next(0, 4);
            string? _description = null;
            //Creating a new object
            Task newTsk = new(otamtId, _idEngineer, _isMileston, _startDate, _deadlineDate, _completeDate, _scheduledDate, _requiredEffortTime, _deliverables, _remarks, _complexityLevel, _description, _alias);
            //Add to data by calling an external operation
            otamtId = t_dalTask!.Create(newTsk);
        }
    }
    //External helper function to draw a random programmer number
    private static Engineer GetRanEng()
    {
        //Declaretion of  Engineer arr  in size of existing engineers
        Engineer[] tempArr = new Engineer[9];
        //converting the list to array
        tempArr = e_dalEngineer!.ReadAll().ToArray();
        //Return random index that contains item
        return tempArr[s_rand.Next(0, 8)];
    }
    //A public method for calling all private methods
    public static void DO(IEngineer? dalEngineer, IDependency? dalDependency, ITask? dalTask)
    {
        //Throwing exception if the objects are null
        e_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        d_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        t_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //Calling all the initializiation function of the different entitites
        createEngineer();
        createTask();
        createDependency();
    }
}
