using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Core.Entities.Identity
{
    public class IdentityContextSeed
    {
        public static async Task SeedUsersAsync(ILoggerFactory loggerFactory, UserManager<Author> userManager, RoleManager<AppRole> roleManager)
        {
            try
            {
            if (!roleManager.Roles.Any())
                {
                    var roles = new List<AppRole>
                    {
                        new AppRole{Name = "Author"},
                        new AppRole{Name = "AdminAuthor"},
                    };

                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
            if(!userManager.Users.Any())
            {
                var user = new Author
                {
                    UserName = "Chris",
                    Email = "Chris@test.com",
                    AuthorPseudonym = "Benjamin Franklin",
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user,"AdminAuthor");
            }

            if(!userManager.Users.Any(r => r.UserName == "DarthVader"))
            {
                var user = new Author
                {
                    UserName = "DarthVader",
                    Email = "Vader@test.com",
                    AuthorPseudonym = "Darth Vader",
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user,"Author");
            }

            if(!userManager.Users.Any(r => r.UserName == "ClarkKent"))
            {
                var user = new Author
                {
                    UserName = "ClarkKent",
                    Email = "Kent@test.com",
                    AuthorPseudonym = "SuperMan",
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user,"Author");
            }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<IdentityContextSeed>();
                logger.LogError(ex.Message);
            }

        }
    }
}