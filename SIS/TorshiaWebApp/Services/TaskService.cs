namespace TorshiaWebApp.Services
{
    using Data;
    using Models;
    using Contracts;
    using System;
    using TorshiaWebApp.Models.Enums;
    using System.Collections.Generic;
    using System.Linq;

    public class TaskService : ITaskService
    {
        private readonly TorshiaDbContext context;

        public TaskService(TorshiaDbContext context)
        {
            this.context = context;
        }

        public void CreateTask(string title, DateTime dueDate, string description, string participants, Sector affectedSectors)
        {
            var task = new Task
            {
                Id = Guid.NewGuid().ToString(),
                Title = title,
                DueDate = dueDate,
                Description = description,
                Participants = participants
            };

            task.AffectedSectors.Add(affectedSectors);

            this.context.Tasks.Add(task);
            this.context.SaveChanges();
        }

        public Task GetById(string taskId)
        {
            return this.context.Tasks.Find(taskId);
        }

        public Task[] GetAllUnreportedTasks()
            => this.context.Tasks.Where(t => !t.IsReported && t.Reports.Count == 0).ToArray();
    }
}
