using JimmyBogardRocks;

namespace SamuraiApp.Domain
{
  public class PersonName : ValueObject<PersonName>  
  {
   
    public static PersonName Create(string first, string last)    {
     return new PersonName(first, last);
    }
    public PersonName() {  }

    private PersonName(string first, string last) {
      First = first;
      Last = last;
    }
     
    public string First { get; private set; }
    public string Last { get; private set; }
    public string FullName()=>First+" "+Last;


  }
}