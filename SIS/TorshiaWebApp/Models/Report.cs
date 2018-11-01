namespace TorshiaWebApp.Models
{
    using Enums;

    using System;

    public class Report
    {
        public string Id { get; set; }

        public Status Status { get; set; }

        public DateTime ReportedOn { get; set; }

        public string TaskId { get; set; }
        public virtual Task Task { get; set; }

        public string ReporterId { get; set; }
        public virtual User Reporter { get; set; }
    }
}
