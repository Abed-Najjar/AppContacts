    using API.Data;
    using API.Entities;
    using API.Repositories;
    using API.UoW;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using API.Interfaces;
    using API.Services;
    using API.Services.Contact;
using API.Services.Users;

namespace API.Extensions;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            // ✅ Add DbContext
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString));

            // ✅ Register Services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IContactsRepository<Contacts>, ContactsRepository<Contacts>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository<AppUser>, UserRepository<AppUser>>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // ✅ AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
