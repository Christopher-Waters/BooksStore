using System.Text.Json;
using Microsoft.Extensions.Logging;
using Core.Entities;

namespace Infrastructure.Data
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Books.Any())
                {
                    var bookData = File.ReadAllText("../Infrastructure/Data/SeedData/books.json");

                    var books = JsonSerializer.Deserialize<List<Book>>(bookData);

                    foreach (var book in books)
                    {
                        context.Books.Add(book);                      
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                
                var logger = loggerFactory.CreateLogger<DataContextSeed>();
                logger.LogError(ex.Message);
            }
        }
        
    }
}