using SourceControlAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SourceaControlAssignment1.Context
{
    public class UserDataContext : DbContext
    {
        public DbSet<UserData> UserData { get; set; }
    }
}