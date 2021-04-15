using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using User.Domain.Permission.Factories;
using User.Domain.Permission.Repositories;
using User.Domain.Role.Factories;
using User.Domain.Role.Repositories;
using User.Domain.User.Factories;
using User.Domain.User.Repositories;
using User.Domain.User.Services;
using User.Infrastructure.Authorization;

namespace User.Infrastructure.Persistence
{
    internal static class DataSeeder
    {
        private const string AdminId = "8a650643-195d-4fd8-892a-c4197f6503ca";
        
        internal static IApplicationBuilder Seed(this IApplicationBuilder applicationBuilder,
            IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetService<IUserRepository>();
            
            var adminId = Guid.Parse(AdminId);
            var admin = userRepository
                .GetAsync(adminId)
                .GetAwaiter()
                .GetResult();
            
            var hasBeenSeeded = admin is not null;

            return hasBeenSeeded
                ? applicationBuilder
                : applicationBuilder.SeedData(serviceProvider);
        }

        private static IApplicationBuilder SeedData(this IApplicationBuilder applicationBuilder,
            IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetService<IUserRepository>();
            var roleRepository = serviceProvider.GetService<IRoleRepository>();
            var permissionRepository = serviceProvider.GetService<IPermissionRepository>();
            var encryptionService = serviceProvider.GetService<IEncryptionService>();

            var admin = UserFactory.Create(
                Guid.Parse(AdminId),
                "admin",
                encryptionService.EncodePassword("1234ABCd?"),
                "Aleksander",
                "Stelmach",
                "alstelmach@outlook.com");

            var adminRole = RoleFactory.Create(
                Guid.Parse(AuthorizationConstants.AdminRoleId),
                "Administrator");

            var roleManagementPermission = PermissionFactory.Create(
                Guid.Parse(AuthorizationConstants.RoleManagementPermissionId),
                "Permission required for role management");
            
            adminRole.AssignPermission(roleManagementPermission);
            admin.AssignRole(adminRole);

            permissionRepository.CreateAsync(roleManagementPermission).Wait();
            roleRepository.CreateAsync(adminRole).Wait();
            userRepository.CreateAsync(admin).Wait();

            return applicationBuilder;
        }
    }
}
