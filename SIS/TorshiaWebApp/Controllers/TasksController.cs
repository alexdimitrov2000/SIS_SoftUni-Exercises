namespace TorshiaWebApp.Controllers
{
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Attributes.Method;
    using SIS.Framework.Controllers;
    using SIS.HTTP.Exceptions;
    using System;
    using System.Linq;
    using System.Net;
    using TorshiaWebApp.Models.Enums;
    using TorshiaWebApp.Services.Contracts;
    using TorshiaWebApp.ViewModels;

    public class TasksController : Controller
    {
        private readonly ITaskService taskService;

        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        [Authorize(nameof(Role.Admin))]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(nameof(Role.Admin))]
        [HttpPost]
        public IActionResult Create(TaskCreateInputModel model)
        {
            var title = model.Title;
            var dueDate = model.DueDate;
            var description = WebUtility.UrlDecode(model.Description);
            var participants = WebUtility.UrlDecode(model.Participants);
            var affectedSectors = Enum.Parse<Sector>(model.AffectedSectors);

            try
            {
                this.taskService.CreateTask(title, dueDate, description, participants, affectedSectors);
            }
            catch (System.Exception e)
            {
                throw new BadRequestException(e.Message);
            }

            return this.RedirectToAction("/");
        }

        [Authorize]
        public IActionResult Details(TaskDetailsInputModel model)
        {
            var taskId = model.id;

            var task = this.taskService.GetById(taskId);

            var taskViewModel = new TaskViewModel
            {
                Title = task.Title,
                Level = task.AffectedSectors.Count,
                DueDate = task.DueDate.ToString("dd/MM/yyyy"),
                Participants = task.Participants,
                AffectedSectors = string.Join(", ", task.AffectedSectors) + " Random Sector",
                Description = task.Description
            };

            this.Model.Data["TaskViewModel"] = taskViewModel;

            return this.View();
        }
    }
}
