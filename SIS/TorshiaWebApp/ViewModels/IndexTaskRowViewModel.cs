using System.Collections.Generic;

namespace TorshiaWebApp.ViewModels
{
    public class IndexTaskRowViewModel
    {
        public List<IndexTaskViewModel> IndexTasks { get; set; }

        public List<EmptyTaskViewModel> EmptyTasks { get; set; }
    }
}
