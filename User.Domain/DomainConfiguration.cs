using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using User.Domain.User.Services;
using User.Domain.User.Validators;
using User.Domain.User.ValueObjects;

namespace User.Domain
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
