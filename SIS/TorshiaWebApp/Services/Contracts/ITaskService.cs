namespace TorshiaWebApp.Services.Contracts
{
    using System;
    using TorshiaWebApp.Models;
    using TorshiaWebApp.Models.Enums;

    public interface ITaskService
    {
        Task GetById(string taskId);

        void CreateTask(string title, DateTime dueDate, string description, string participants, Sector affectedSectors);

        Task[] GetAllUnreportedTasks();
    }
}
