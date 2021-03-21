using Authentication.Domain.User.Services;
using Authentication.Domain.User.Validators;
using Authentication.Domain.User.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Domain
{
    public static class DomainConfiguration
    {
        public static IServiceCollection RegisterDomainDependencies(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddScoped<IValidator<Password>, PasswordValidator>()
                .AddScoped<IValidator<User.User>, UserValidator>()
                .AddScoped<UserService>();
    }
}
