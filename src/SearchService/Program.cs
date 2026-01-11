using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Data;
using SearchService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// // Initialize the DB
// await DB.InitAsync(
//     "SearchDb",
//     MongoClientSettings.FromConnectionString(
//         builder.Configuration.GetConnectionString("MongoDbConnection")
//     )
// );

// // Get the database instance
// var db = DB.Instance("SearchDb");

// // Create text index
// await db.Index<Item>()
//     .Key(x => x.Make, KeyType.Text)
//     .Key(x => x.Model, KeyType.Text)
//     .Key(x => x.Color, KeyType.Text)
//     .CreateAsync();

try
{
    await DbInitializer.InitDb(app);
}catch(Exception e)
{
    Console.WriteLine(e);
}


app.Run();
