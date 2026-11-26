using Casino.Application.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Casino.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IRegisterUseCase, RegisterUseCase>();

        return services;
    }
}
