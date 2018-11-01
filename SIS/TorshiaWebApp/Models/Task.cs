namespace TorshiaWebApp.Models
{
    using Enums;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Task
    {
        public Task()
        {
            this.AffectedSectors = new HashSet<Sector>();
            this.Reports = new List<Report>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsReported { get; set; }

        public string Description { get; set; }

        public string Participants { get; set; }

        [NotMapped]
        public virtual ICollection<Sector> AffectedSectors { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
