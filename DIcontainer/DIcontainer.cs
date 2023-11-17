using Microsoft.Extensions.DependencyInjection;

namespace InsuranceManagement
{
    /// <summary>
    /// A shorthand access class to get dependency injection services
    /// </summary>
    public static class DIcontainer
    {

        /// <summary>
        /// The scoped instance of the <see cref="ApplicationDbContext"/>
        /// </summary>
        public static DapperDbContext? DapperDbContext => Container.Provider?.GetService<DapperDbContext>();
    }

    /// <summary>
    /// The dependency injection container takes advantage of the built-in .Net Core service provider
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// The service provider for this application
        /// </summary>
        public static ServiceProvider? Provider { get; set; }
    }
}
