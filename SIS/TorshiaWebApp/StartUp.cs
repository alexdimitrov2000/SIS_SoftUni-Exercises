namespace TorshiaWebApp
{
    using SIS.Framework.Api;
    using SIS.Framework.Services;
    using TorshiaWebApp.Services;
    using TorshiaWebApp.Services.Contracts;

    public class StartUp : MvcApplication
    {
        public override void Configure()
        {
        }

        public override void ConfigureServices(IDependencyContainer dependencyContainer)
        {
            dependencyContainer.RegisterDependency<IHashService, HashService>();
            dependencyContainer.RegisterDependency<IUserService, UserService>();
            dependencyContainer.RegisterDependency<ITaskService, TaskService>();
            dependencyContainer.RegisterDependency<IReportService, ReportService>();
        }
    }
}
