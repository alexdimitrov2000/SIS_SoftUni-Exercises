using System;
using TorshiaWebApp.Models.Enums;

namespace TorshiaWebApp.ViewModels
{
    public class TaskCreateInputModel
    {
        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }

        public string Participants { get; set; }

        public string AffectedSectors { get; set; }
    }
}
