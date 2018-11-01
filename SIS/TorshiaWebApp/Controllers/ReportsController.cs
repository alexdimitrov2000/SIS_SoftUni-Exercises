namespace TorshiaWebApp.Controllers
{
    using SIS.Framework.ActionResults;
    using SIS.Framework.Attributes.Action;
    using SIS.Framework.Controllers;
    using SIS.HTTP.Exceptions;
    using System.Linq;
    using TorshiaWebApp.Models.Enums;
    using TorshiaWebApp.Services.Contracts;
    using TorshiaWebApp.ViewModels;

    public class ReportsController : Controller
    {
        private readonly IReportService reportService;

        public ReportsController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [Authorize(nameof(Role.Admin))]
        public IActionResult All()
        {
            var reportViewModels = this.reportService.GetAllReports()
                .Select(r => new ReportAllViewModel
                {
                    Id = r.Id,
                    Level = r.Task.AffectedSectors.Count,
                    Status = r.Status.ToString(),
                    Title = r.Task.Title
                }).ToArray();

            for (int i = 0; i < reportViewModels.Count(); i++)
            {
                reportViewModels[i].Number = i + 1;
            }

            var reportCollection = new ReportCollectionViewModel
            {
                Reports = reportViewModels
            };

            this.Model.Data["ReportCollectionViewModel"] = reportCollection;

            return this.View();
        }

        [Authorize]
        public IActionResult Report()
        {
            var taskId = this.Request.QueryData["id"].ToString();
            var userUsername = this.Identity.Username;

            try
            {
                this.reportService.CreateReport(taskId, userUsername);
            }
            catch (System.Exception e)
            {
                throw new BadRequestException(e.Message);
            }

            return this.RedirectToAction("/");
        }

        [Authorize(nameof(Role.Admin))]
        public IActionResult Details()
        {
            var reportId = this.Request.QueryData["id"].ToString();

            var report = this.reportService.GetById(reportId);

            var reportViewModel = new ReportDetailsViewModel
            {
                Id = reportId,
                TaskTitle = report.Task.Title,
                AffectedSectors = string.Join(", ", report.Task.AffectedSectors) + " Random Sector",
                Description = report.Task.Description,
                DueDate = report.Task.DueDate.ToString("dd/MM/yyyy"),
                Level = report.Task.AffectedSectors.Count,
                ReportedOn = report.ReportedOn.ToString("dd/MM/yyyy"),
                Participants = report.Task.Participants,
                ReporterName = report.Reporter.Username,
                Status = report.Status.ToString()
            };

            this.Model.Data["ReportDetailsViewModel"] = reportViewModel;

            return this.View();
        }
    }
}
