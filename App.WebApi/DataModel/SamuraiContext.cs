using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;


namespace SamuraiApp.Data {
  public class SamuraiContext : DbContext {

    public SamuraiContext (DbContextOptions<SamuraiContext> options) : base (options) {

    }
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Quote> Quotes { get; set; }

    protected override void OnModelCreating (ModelBuilder modelBuilder) {

      foreach (var entityType in modelBuilder.Model.GetEntityTypes ()) {
        //LastModified is a shadow property, not props in the classes
        modelBuilder.Entity (entityType.Name).Property<DateTime> ("LastModified");
        //IsDirty is for local tracking, not persisted in the database
        modelBuilder.Entity (entityType.Name).Ignore ("IsDirty");
      }
    //  modelBuilder.Entity<Samurai> ().OwnsOne (typeof (PersonName), "SecretIdentity");
    }

    public override int SaveChanges () {
      foreach (var entry in ChangeTracker.Entries ()
        .Where (e => e.State == EntityState.Added ||
          e.State == EntityState.Modified)) {
        entry.Property ("LastModified").CurrentValue = DateTime.Now;
      }
      return base.SaveChanges ();
    }
  }
}