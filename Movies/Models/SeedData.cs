using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Areas.Identity.Data;
using Movies.Data;
using System;
using System.Linq;

namespace Movies.Models
{
    public class SeedData
    {
        public static void InitializeMovieData(IServiceProvider serviceProvider)
        {
            using(var context = new MoviesContext(
                serviceProvider.GetRequiredService<DbContextOptions<MoviesContext>>()
                ))
            {
                // Look for any movies
                if (context.MovieModel.Any())
                {
                    return; //Db has been seeded
                }

                context.MovieModel.AddRange(
                      new MovieModel
                      {
                          Title = "When Harry Met Sally",
                          ReleaseDate = DateTime.Parse("1989-2-12"),
                          Genre = "Romantic Comedy",
                          Rating = "R",
                          Price = 7.99M
                      },

                        new MovieModel
                        {
                            Title = "Ghostbusters ",
                            ReleaseDate = DateTime.Parse("1984-3-13"),
                            Genre = "Comedy",
                            Rating = "E",
                            Price = 8.99M
                        },

                        new MovieModel
                        {
                            Title = "Ghostbusters 2",
                            ReleaseDate = DateTime.Parse("1986-2-23"),
                            Genre = "Comedy",
                            Rating = "X",
                            Price = 9.99M
                        },

                        new MovieModel
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Genre = "Western",
                            Rating = "R",
                            Price = 3.99M
                        }
                    );
                context.SaveChanges();
            }
        }

        internal static void InitializeRolesData(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesLoginContext(
                serviceProvider.GetRequiredService<DbContextOptions<MoviesLoginContext>>()
                ))
            {
                if (context.Roles.Any())
                {
                    return; //Db has been seeded
                }
                context.Roles.AddRange(
                      new IdentityRole
                      {
                          Name = "Admin",
                          NormalizedName = "ADMIN",
                      },
                      new IdentityRole
                      {
                          Name = "Manager",
                          NormalizedName = "MANAGER",
                      },
                      new IdentityRole
                      {
                          Name = "User",
                          NormalizedName = "USER",
                      }
                      );
                context.SaveChanges();
            }
        }

        //internal static void InitializeUserRoles(IServiceProvider serviceProvider)
        //{
        //    using (var context = new MoviesLoginContext(
        //        serviceProvider.GetRequiredService<DbContextOptions<MoviesLoginContext>>()
        //        ))
        //    {
        //        if (context.UserRoles.Any())
        //        {
        //            return; //Db has been seeded
        //        }
        //        foreach (var user in context.Users)
        //        {
        //            Random rnd = new Random();
        //            int roleAmount = rnd.Next(1,3);
        //            var roleList = new List<string> { "Admin", "Manager", "User" };
        //            context.UserRoles.AddRange(new IdentityUserRole<string>{
        //                UserId = user.Id,
        //                RoleId = "Admin"
        //            });
                         
        //            context.UserRoles.AddRange(
        //              new IdentityRole
        //              {
        //                  Name = "Admin",
        //                  NormalizedName = "ADMIN",
        //              },
        //              new IdentityRole
        //              {
        //                  Name = "Manager",
        //                  NormalizedName = "MANAGER",
        //              },
        //              new IdentityRole
        //              {
        //                  Name = "User",
        //                  NormalizedName = "USER",
        //              }
        //              );
        //        }
                
        //        context.SaveChanges();
        //    }
        
    }
}
