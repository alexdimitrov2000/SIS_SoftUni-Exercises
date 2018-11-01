namespace TorshiaWebApp.Services
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TorshiaWebApp.Data;
    using TorshiaWebApp.Models;
    using TorshiaWebApp.Models.Enums;

    public class ReportService : IReportService
    {
        private readonly TorshiaDbContext context;
        private readonly IUserService userService;
        private readonly ITaskService taskService;

        public ReportService(TorshiaDbContext context, IUserService userService, ITaskService taskService)
        {
            this.context = context;
            this.userService = userService;
            this.taskService = taskService;
        }

        public void CreateReport(string taskId, string userUsername)
        {
            var task = this.taskService.GetById(taskId);
            var reporter = this.userService.GetByUsername(userUsername);

            var statusChance = new Random().Next(0, 100);

            var status = statusChance <= 25 ? Status.Archived : Status.Completed;

            task.IsReported = true;
            this.context.SaveChanges();

            var report = new Report()
            {
                Id = Guid.NewGuid().ToString(),
                ReportedOn = DateTime.UtcNow,
                ReporterId = reporter.Id,
                Status = status,
                TaskId = taskId
            };

            this.context.Reports.Add(report);
            this.context.SaveChanges();
        }

        public List<Report> GetAllReports()
            => this.context.Reports.ToList();

        public Report GetById(string reportId)
            => this.context.Reports.Find(reportId);
    }
}
