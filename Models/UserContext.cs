// Main class for coordinating Entity Framework functionality for a data model
// Dependency object is a object on which other objects depend upon
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class UserContext: DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {}
    // Db set properties for each entity in the model
    // DbSet<TEntity>
    public DbSet<User> Users {get; set;} = null!; 

}