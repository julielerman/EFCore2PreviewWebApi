using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;


namespace Testing
{
  public class DataModelTests
  {
    [Fact]
    public void CanInsertSamuraiWithNoSecretIdentity_InMemory()
    {
      var optionsBuilder = new DbContextOptionsBuilder<SamuraiContext>();
      optionsBuilder.UseInMemoryDatabase("CanInsertSamuraiWithNoSecretIdentity");

      var context = new SamuraiContext(optionsBuilder.Options);

      context.Samurais.Add(new Samurai { Name = "HubbieSan" });
      context.SaveChanges();
      Assert.Equal("HubbieSan", context.Samurais.FirstOrDefaultAsync().Result.Name);
  
    }

     [Fact]
     public void CanInsertSamuraiWithSecretIdentity_InMemory()
    {
      var optionsBuilder = new DbContextOptionsBuilder<SamuraiContext>();
      optionsBuilder.UseInMemoryDatabase("CanInsertSamuraiWithSecretIdentity");
      var context = new SamuraiContext(optionsBuilder.Options);
      var samurai=new Samurai { Name = "HubbieSan" };
      samurai.Identify("Late","Todinner");
      context.Samurais.Add(samurai);
      context.SaveChanges();
      Assert.Equal("Late Todinner", context.Samurais
                                    .FirstOrDefaultAsync(s=>s.Name=="HubbieSan")
                                    .Result.RevealSecretIdentity());
  
    }
     //[Fact]
     public void CanInsertSamuraiWithSecretIdentity_SqlDb()
    {
      //ADD OWNED ENTITIES DOES NOT WORK YET WITH DB, KNOWN BUG & BEING FIXED
       var optionsBuilder = new DbContextOptionsBuilder<SamuraiContext>();
       optionsBuilder.UseSqlServer(
          "Server=localhost;Database=SamuraiTest;User ID=sa;Password=Passw0rd;MultipleActiveResultSets=true;Connect Timeout=30;");
 
      var context = new SamuraiContext(optionsBuilder.Options);
      context.Database.EnsureCreated();
      var samurai=new Samurai { Name = "HubbieSan" };
      samurai.Identify("Late","Todinner");
      context.Samurais.Add(samurai);
      context.SaveChanges();
      Assert.Equal("Late Todinner", context.Samurais
                                    .FirstOrDefaultAsync(s=>s.Name=="HubbieSan")
                                    .Result.RevealSecretIdentity());
  
    }
    [Fact]
    public void CanInsertSamuraiWithNoEntrance_InMemory()
    {
      var optionsBuilder = new DbContextOptionsBuilder<SamuraiContext>();
      optionsBuilder.UseInMemoryDatabase("CanInsertSamuraiWithNoEntrance");

      var context = new SamuraiContext(optionsBuilder.Options);

      context.Samurais.Add(new Samurai { Name = "JulieSan" });
      context.SaveChanges();
      Assert.Equal("JulieSan", context.Samurais.FirstOrDefaultAsync().Result.Name);
    }
    [Fact]
    public void CanInsertSamuraiWithEntrance_InMemory()
    {
      var optionsBuilder = new DbContextOptionsBuilder<SamuraiContext>();
      optionsBuilder.UseInMemoryDatabase("CanInsertSamuraiWithEntrance");

      using(var context = new SamuraiContext(optionsBuilder.Options))
      {
        var description = "A great entrance";
        var samurai = new Samurai();
        samurai.CreateEntrance(1, "S1", description);
        context.Samurais.Add(samurai);
        context.SaveChanges();
        Assert.Equal(description, 
         context.Samurais.Include(s => s.Entrance)
         .FirstOrDefaultAsync().Result.Entrance.ActionDescription);
    }}
  }

}
