namespace SoftUni.SimpleApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class SimpleAppDbContext : IdentityDbContext
    {
        public SimpleAppDbContext(DbContextOptions options)
            : base(options) { }
    }
}