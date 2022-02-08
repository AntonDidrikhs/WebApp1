using WebApp1.Models;
using WebApp1.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebApp1.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@admin.com");
                await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

                SeedDBStudio(context, testUserPw);
                SeedDBGames(context, testUserPw);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            if (userManager == null)
            {
                throw new Exception("userManager null");
            }

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                              string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            if (userManager == null)
            {
                throw new Exception("userManager is null");
            }

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        public static void SeedDBStudio(ApplicationDbContext context, string adminID)
        {
            if (context.Studio.Any())
            {
                return;
            }
            context.Studio.AddRange(
                new Studio
                {
                    StudioName = "Placeholder",
                    StudioDescription = "This is placeholder for studio value"
                }
                );
            context.SaveChanges();
        }
        public static void SeedDBGames(ApplicationDbContext context, string adminID)
        {
            if (context.Game.Any())
            {
                return;
            }
            context.Game.AddRange(
                    new Game
                    {
                        Title = "Among Us",
                        Descriprion = "A Mafia-like social game based on deseption and deduction",
                        ReleaseDate = DateTime.Parse("2018-11-13"),
                        Genre = "Social Deduction",
                        Price = 19.99M,
                        StudioId = 2
                    },
                    new Game
                    {
                        Title = "CS:GO",
                        Descriprion = "A popular tactical FPS",
                        ReleaseDate = DateTime.Parse("2016-7-13"),
                        Genre = "FPS",
                        Price = 39.99M,
                        StudioId = 2
                    },
                    new Game
                    {
                        Title = "Rust",
                        Descriprion = "The only aim in Rust is to survive. Everything wants you to die - the island’s wildlife and other inhabitants, the environment, other survivors. Do whatever it takes to last another night.",
                        ReleaseDate = DateTime.Parse("2018-2-8"),
                        Genre = "Survival",
                        Price = 39.99M,
                        StudioId = 2
                    },
                    new Game
                    {
                        Title = "The Forest",
                        Descriprion = "As the lone survivor of a passenger jet crash, you find yourself in a mysterious forest battling to stay alive against a society of cannibalistic mutants.",
                        ReleaseDate = DateTime.Parse("2018-4-30"),
                        Genre = "Survival",
                        Price = 29.99M,
                        StudioId = 2
                    },
                    new Game
                    {
                        Title = "Dragon Age Inqusition",
                        Descriprion = "When the sky opens up and rains down chaos, the world needs heroes. Become the savior of Thedas in Dragon Age: Inquisition. You are the Inquisitor, tasked with saving the world from itself. But the road ahead is paved with difficult decisions. Thedas is a land of strife.",
                        ReleaseDate = DateTime.Parse("2014-11-14"),
                        Genre = "RPG",
                        Price = 49.99M,
                        StudioId = 2
                    },
                    new Game
                    {
                        Title = "The Witcher 3",
                        Descriprion = "As war rages on throughout the Northern Realms, you take on the greatest contract of your life — tracking down the Child of Prophecy, a living weapon that can alter the shape of the world.",
                        ReleaseDate = DateTime.Parse("2015-5-18"),
                        Genre = "RPG",
                        Price = 29.99M,
                        StudioId = 2
                    }
                    );
            context.SaveChanges();
        }
    }
}
