using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QA.Models;

namespace QA.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
//            -المعادن والفيتامينات
//٢-المكملات الغذائيه
//٣-الاعشاب
//٤-الجهاز الهضمي
//٥-الجهاز العصبي
//٦-أنظمة غذائية
//٧-أمراض النسائية
//٨-امراض الذكورة
//٩-الغدد
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
      new Category { Id = 1, Name = "المعادن والفيتامينات" },
      new Category { Id = 2, Name = "المكملات الغذائيه" },
      new Category { Id = 3, Name = "الاعشاب" },
      new Category { Id = 4, Name = "الجهاز الهضمي" },
      new Category { Id = 5, Name = "الجهاز العصبي" },
      new Category { Id = 6, Name = "أنظمة غذائية" },
      new Category { Id = 7, Name = "أمراض النسائية" },
      new Category { Id = 8, Name = "امراض الذكورة" },
      new Category { Id = 9, Name = "الغدد" }
        // Add more categories as needed
            );
            SeedRole(modelBuilder, "Admin", "create", "update", "delete");
           

            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = "1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "adminUser@example.com",
                PhoneNumber = "1234567890",
                NormalizedEmail = "adminUser@EXAMPLE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin!23");
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
             new IdentityUserRole<string>
             {
                 RoleId = "admin",
                 UserId = adminUser.Id
             });
        }
        private int nextId = 1;
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);

            var roleClaims = permissions.Select(permission =>
            new IdentityRoleClaim<string>
            {
                Id = nextId++,
                RoleId = role.Id,
                ClaimType = "permissions",
                ClaimValue = permission
            }).ToArray();
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }


        public DbSet<Question> Question { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
