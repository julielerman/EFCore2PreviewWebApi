This is the final solution from my EF Core: Open Source Data Access session in DevTeach 2017.
It uses ASP.NET Core Preview2-final and EF Core Preview2-final.
You can open it in Visual Studio Code or Visual Studio althogh keep in mind that it doesn't currently have a Visua Studio sln file.

The SQL Server connection string used by some of the Test.WebApi tests and by the web api itself (in startup.cs) point to localhost.
I was using a Docker container with SQL Server for Linux in my demo.
(See MSDN Mag Data points column for more info about that: bit.ly/DataPoints-sqldocker)

Besides running the tests, I also ran the web api and then connected to 
it from PostMan or Fiddler. You'll find a json file in this repo that you can import
into Postman to run the requests.
I've also included screenshots of the requests so you can recreate them as needed
elsewhere.


Julie Lerman
thedatafarm.com
github.com/julielerman
twitter.com/julielerman