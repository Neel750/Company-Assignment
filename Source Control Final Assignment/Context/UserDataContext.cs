using Source_Control_Final_Assignment.Models;
using System.Data.Entity;

namespace SourceaControlAssignment1.Context
{
    public class UserDataContext : DbContext
    {
        public DbSet<UserData> UserData { get; set; }
    }
}