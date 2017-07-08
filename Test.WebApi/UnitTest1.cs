using System;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using Xunit;

namespace WebApiTests {
  public class UnitTest1 {
    DbContextOptionsBuilder<SamuraiContext> _sqlDbOptionsBuilder;
    DbContextOptionsBuilder<SamuraiContext> _inMemoryOptionsBuilder;

    public UnitTest1 () {
        _inMemoryOptionsBuilder = new DbContextOptionsBuilder<SamuraiContext> ();
        _sqlDbOptionsBuilder = new DbContextOptionsBuilder<SamuraiContext> ();
        _sqlDbOptionsBuilder.UseSqlServer (
          "Server=localhost;Database=SamuraiTest;User ID=sa;Password=Passw0rd;MultipleActiveResultSets=true;Connect Timeout=30;");
        using (var context = new SamuraiContext (_sqlDbOptionsBuilder.Options)) {
          context.Database.EnsureDeleted ();
          context.Database.EnsureCreated ();
        }

      }
      [Fact]
    public void CanInsertSamurai_InMemory () {
        _inMemoryOptionsBuilder.UseInMemoryDatabase ("InsertSamurai");
        var context = new SamuraiContext (_inMemoryOptionsBuilder.Options);
        var controller = new SamuraiController (context);
        controller.CreateSamurai ("JulieSan");
        Assert.NotNull (context.Samurais.FirstOrDefaultAsync (s => s.Name == "JulieSan").Result);

      }
      [Fact]
    public void CanInsertSamuraiWithEntrance_InMemory () {
        _inMemoryOptionsBuilder.UseInMemoryDatabase ("InsertSamuraiWithEntrance");
        var context = new SamuraiContext (_inMemoryOptionsBuilder.Options);
        var controller = new SamuraiController (context);
        controller.CreateSamuraiWithEntrance ("JulieSan", 1, "S1", "A great entrance");
        var samurai = context.Samurais.FirstOrDefaultAsync (s => s.Name == "JulieSan").Result;
        Assert.Equal ("A great entrance", samurai.Entrance.ActionDescription);
      }
      [Fact]
    public void CanInsertSamurai_Database () {

      var context = new SamuraiContext (_sqlDbOptionsBuilder.Options);
      var controller = new SamuraiController (context);
      controller.CreateSamurai ("JulieSan");

      Assert.Equal ("JulieSan",
        context.Samurais.FirstOrDefaultAsync ().Result.Name);

    }

    [Fact]
    public void CanInsertSamuraiWithEntrance_Database () {
      var context = new SamuraiContext (_sqlDbOptionsBuilder.Options);
      var controller = new SamuraiController (context);
      controller.CreateSamuraiWithEntrance ("SampsonSan", 1, "S2", "A drooly entrance");
      Assert.Equal ("A drooly entrance",
        context.Samurais
        .FirstOrDefaultAsync (s => s.Name == "SampsonSan")
        .Result.Entrance.ActionDescription);
    }

    [Fact]
    public void CanAddQuoteToExistingSamurai_InMemory () {
      _inMemoryOptionsBuilder.UseInMemoryDatabase ("InsertQuote");
      var context = new SamuraiContext (_inMemoryOptionsBuilder.Options);
      var controller = new SamuraiController (context);
      var samuraiId = controller.CreateSamurai ("JulieSan");
      controller.AddQuoteToSamurai (samuraiId, "Time to sharpen the sword");
      Assert.Equal ("Time to sharpen the sword", context.Quotes.FirstOrDefaultAsync (q => q.SamuraiId == samuraiId).Result.Text);

    }
  }
}