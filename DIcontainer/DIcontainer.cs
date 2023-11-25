using Microsoft.Extensions.DependencyInjection;

namespace InsuranceManagement.API
{
    /// <summary>
    /// A shorthand access class to get dependency injection services
    /// </summary>
    public static class DIcontainer
    {

        /// <summary>
        /// The scoped instance of the <see cref="DapperDbContext"/>
        /// </summary>
        public static DapperDbContext? DapperDbContext => Container.Provider?.GetService<DapperDbContext>();

        /// <summary>
        /// The scoped instance of the <see cref="EFDbContext"/>
        /// </summary>
        public static EFDbContext? EFDbContext => Container.Provider?.GetService<EFDbContext>();
    }

    /// <summary>
    /// The dependency injection container takes advantage of the built-in .Net Core service provider
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// The service provider for this application
        /// </summary>
        public static ServiceProvider Provider { get; set; }

        /// <summary>
        /// The configuration manager for the application
        /// </summary>
        public static IConfiguration Configuration { get; set; }
    }
}
