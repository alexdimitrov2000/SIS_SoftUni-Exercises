namespace TorshiaWebApp.Controllers
{
    using SIS.Framework.ActionResults;
    using SIS.Framework.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using TorshiaWebApp.Models.Enums;
    using TorshiaWebApp.Services.Contracts;
    using TorshiaWebApp.ViewModels;

    public class HomeController : Controller
    {
        private readonly ITaskService taskService;

        public HomeController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        public IActionResult Index()
        {
            if (this.Identity != null)
            {
                this.Model.Data["Username"] = this.Identity.Username;

                var indexTaskRowsHolder = new IndexTaskRowsHolderViewModel();

                List<IndexTaskViewModel> taskViewModels =
                    this.taskService.GetAllUnreportedTasks()
                        .Select(t => new IndexTaskViewModel
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Level = t.AffectedSectors.Count + 1
                        })
                        .ToList();

                List<IndexTaskRowViewModel> taskRowViewModels = new List<IndexTaskRowViewModel>();

                for (int i = 0; i < taskViewModels.Count; i++)
                {
                    if (i % 5 == 0)
                    {
                        taskRowViewModels.Add(new IndexTaskRowViewModel());
                    }

                    if (taskRowViewModels[taskRowViewModels.Count - 1].IndexTasks == null)
                    {
                        taskRowViewModels[taskRowViewModels.Count - 1].IndexTasks = new List<IndexTaskViewModel>();
                    }
                    
                    taskRowViewModels[taskRowViewModels.Count - 1].IndexTasks.Add(taskViewModels[i]);
                }

                indexTaskRowsHolder.IndexTaskRows = taskRowViewModels;

                taskRowViewModels[taskRowViewModels.Count - 1].EmptyTasks = new List<EmptyTaskViewModel>();

                int numberOfEmptyTasks = 5 - taskRowViewModels[taskRowViewModels.Count - 1].IndexTasks.Count;

                for (int i = 0; i < numberOfEmptyTasks; i++)
                {
                    taskRowViewModels[taskRowViewModels.Count - 1].EmptyTasks.Add(new EmptyTaskViewModel());
                }

                this.Model["IndexTaskRowsHolderViewModel"] = indexTaskRowsHolder;

                if (this.Identity.Roles.Contains(nameof(Role.Admin)))
                    return this.View("AdminIndex");

                return this.View("LoggedIndex");
            }

            return this.View();
        }
    }
}
