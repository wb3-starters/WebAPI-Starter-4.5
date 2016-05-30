using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using WebAPI.Components.User;
using WebAPI.Components.UserAccount;

namespace WebAPI.Core
{
    public class WebAPIDbContext : DbContext
    {
        public WebAPIDbContext() : base() { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserAccount>()
                .Ignore(e => e.Password);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                // TODO: Find a better way to get the name for a user
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = Thread.CurrentPrincipal.Identity.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }

            return base.SaveChanges();
        }
    }
}
