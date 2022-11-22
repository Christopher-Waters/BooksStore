using Core.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Services;
using Core.Interfaces;

namespace API.Extentions
{
    public static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {
            var builder = services.AddIdentityCore<Author>();
            
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRoles<AppRole>();
            builder.AddRoleManager<RoleManager<AppRole>>();
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddSignInManager<SignInManager<Author>>();
            builder.AddRoleValidator<RoleValidator<AppRole>>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<ITokenService,TokenService>();

            return services;
           
        }
    }
}