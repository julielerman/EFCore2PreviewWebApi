using System;
using Xunit;
using SamuraiApp.Data;
using SamuraiApp.Domain;


namespace Testing
{
    public class  DomainClassTests
  {
    [Fact]
    public void CanCreateNewSamuraiWithEntrance()
    {
      var description="A great entrance";
      var samurai = new Samurai();
      samurai.CreateEntrance(1,"S1",description);
      Assert.Equal(description, samurai.Entrance.ActionDescription);
    }
    [Fact]
    public void CanCreateNewIdentityForSamurai()
    {
      var samurai = new Samurai();
      samurai.Identify("Julie", "Lerman");
      Assert.Equal("Julie Lerman", samurai.RevealSecretIdentity());
    }
  }
}
