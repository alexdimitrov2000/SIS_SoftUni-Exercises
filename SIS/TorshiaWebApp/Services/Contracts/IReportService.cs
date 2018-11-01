using System.Collections.Generic;
using TorshiaWebApp.Models;

namespace TorshiaWebApp.Services.Contracts
{
    public interface IReportService
    {
        void CreateReport(string taskId, string userUsername);

        List<Report> GetAllReports();

        Report GetById(string reportId);
    }
}
