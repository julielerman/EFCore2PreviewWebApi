 using System.Collections.Generic;
 using System.Linq;
 using System;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.EntityFrameworkCore;
 using SamuraiApp.Data;
 using SamuraiApp.Domain;

 [Route ("api/[controller]")]
 public class SamuraiController : Controller {
   SamuraiContext _context;
   public SamuraiController (SamuraiContext context) {
     _context = context;
   }

   [HttpGet]
   public IEnumerable<Samurai> Get () {
       return _context.Samurais
         .Include (s => s.Quotes)
         .Include (s => s.Entrance)
         .ToList ();
     }

  [HttpPut ("AddQuote")]
  public int AddQuoteToSamurai (int samuraiId, string quoteText) {
     var quote = new Quote { SamuraiId = samuraiId, Text = quoteText };
     _context.Quotes.Add (quote);
     return _context.SaveChangesAsync ().Result;
   }

   [HttpPost ("Create")]
   public int CreateSamurai (string samuraiName) {
     var samurai = new Samurai ();
     samurai.Name = samuraiName;
     _context.Samurais.Add (samurai);
     _context.SaveChanges ();
     return samurai.Id;
   }

   [HttpPost ("CreateWithEntrance")]
   public int CreateSamuraiWithEntrance (string samuraiName, int movieMinute, string scene, string description) {
     var samurai = new Samurai { Name = samuraiName };
     samurai.CreateEntrance (movieMinute, scene, description);
     _context.Samurais.Add (samurai);
     var affectedRowCount = _context.SaveChanges ();
     return affectedRowCount;
   }

 }