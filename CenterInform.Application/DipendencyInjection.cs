using Microsoft.Extensions.DependencyInjection;

namespace CenterInform.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //configure services application layer here
            return services;
        }
    }
}
