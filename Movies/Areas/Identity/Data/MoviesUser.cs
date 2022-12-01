using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Movies.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MoviesUser class
public class MoviesUser : IdentityUser
{
    //public string UserName { get; set; }
}

public class Roles : IdentityRole
{

}