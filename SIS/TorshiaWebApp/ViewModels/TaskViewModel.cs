namespace TorshiaWebApp.ViewModels
{
    public class TaskViewModel
    {
        public string Title { get; set; }

        public int Level { get; set; } = 0;

        public string DueDate { get; set; }

        public string Participants { get; set; }

        public string AffectedSectors { get; set; }

        public string Description { get; set; }
    }
}
