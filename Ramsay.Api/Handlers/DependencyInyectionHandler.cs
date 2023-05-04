using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork;
using Infraestructure.Core.UnitOfWork.Interface;
using Ramsay.Domain.Services;
using Ramsay.Domain.Services.Interfaces;

namespace Ramsay.Api.Handlers
{
    public static class DependencyInyectionHandler
    {
        public static void DependencyInyectionConfig(IServiceCollection services)
        {
            // Repository await UnitofWork parameter ctor explicit
            services.AddScoped<CustomValidationFilterAttribute, CustomValidationFilterAttribute>();

            // Infrastructure
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Domain
            services.AddTransient<IStudenServices, StudenServices>();

        }
    }
}
