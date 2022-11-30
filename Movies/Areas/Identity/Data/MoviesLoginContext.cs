using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Areas.Identity.Data;

namespace Movies.Data;

public class MoviesLoginContext : IdentityDbContext<MoviesUser>
{
    public MoviesLoginContext(DbContextOptions<MoviesLoginContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        //builder.ApplyConfiguration(new MovieUsersEntityConfiguration());
    }
}

internal class MovieUsersEntityConfiguration : IEntityTypeConfiguration<MoviesUser>
{
    public void Configure(EntityTypeBuilder<MoviesUser> builder)
    {
        //builder.Property(u => u.Name).HasMaxLength(255);
    }
}