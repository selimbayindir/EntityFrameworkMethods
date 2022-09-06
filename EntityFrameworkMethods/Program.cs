// See https://aka.ms/new-console-template for more information
using EntityFrameworkMethods;

Console.WriteLine("Entit Frameworks All Methods");

NortwindContext context = new();

Person person = new();

await context.AddAsync(person);
context.SaveChangesAsync();

Console.WriteLine("Merhaba");