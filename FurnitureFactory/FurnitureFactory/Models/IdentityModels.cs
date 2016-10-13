using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactory.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            Files = new List<File>();
        }
        public string FullName { get; set; }
        public string Adress { get; set; }
        public string Bulstat { get; set; }
        public bool DDSReg { get; set; }
        public string MOL { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=FurnitureFactoryDB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<FurnitureOrder> FurnitureOrder { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<File> File { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
  
            modelBuilder.Entity<FurnitureOrder>()
                .HasKey(t => new { t.FurnitureId, t.OrderId });

            modelBuilder.Entity<File>()
                .HasRequired(t => t.User)
                .WithMany(t => t.Files)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Furniture>().ToTable("Furniture");

            modelBuilder.Entity<Furniture>()
                .Property(t => t.BarCode)
                .HasMaxLength(30)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_BarCode", 1) { IsUnique = true }));

            modelBuilder.Entity<Furniture>()
                .Property(t => t.Name)
                .HasMaxLength(255);

            modelBuilder.Entity<Order>()
                .HasRequired(t => t.Client)
                .WithMany(t => t.Orders)
                .HasForeignKey(d => d.ClientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(t => t.Date)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_Date", 1) { IsUnique = true }));

            modelBuilder.Entity<Order>()
                .Property(t => t.RecieptNumber)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                new IndexAnnotation(new IndexAttribute("IX_RecieptNumber", 1) { IsUnique = true }));

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.Adress)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.FullName)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.MOL)
                .HasMaxLength(255);

            modelBuilder.Entity<ApplicationUser>()
                .Property(t => t.DDSReg)
                .IsRequired();
        }
    }
}