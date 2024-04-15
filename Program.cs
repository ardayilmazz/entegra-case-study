// See https://aka.ms/new-console-template for more information

using EntegraCaseStudy;
using Microsoft.Data.Sqlite;

Console.WriteLine("Started!");

var token = await BusinessLogic.GetAccessToken(ConstString.Email, ConstString.Password);

Console.WriteLine($"access token: {token}");

var products = await BusinessLogic.GetProducts(token);

await using var connection = new SqliteConnection(ConstString.DbConnectionString);
await connection.OpenAsync();
await DatabaseOperations.Initial(connection);
Console.WriteLine("Inital DB");
foreach (var product in products.Take(5))
{
    await DatabaseOperations.Insert(product, connection);
}
Console.WriteLine("Inserted DB");
var updatedProduct = products.First();
updatedProduct.Quantity += 1;
updatedProduct.Name = $"New {updatedProduct.Name}";

await DatabaseOperations.Update(updatedProduct, connection);
Console.WriteLine("Updated DB");


await DatabaseOperations.Select(connection);
Console.WriteLine("Selected DB");
await DatabaseOperations.Delete(updatedProduct.ProductCode, connection);
Console.WriteLine($"Deleted Product {updatedProduct.ProductCode}");
await DatabaseOperations.Search("Classic", connection);
Console.WriteLine($"Searched Product");

// Delete All Product
foreach (var product in products.Take(5))
{
    await DatabaseOperations.Delete(product.ProductCode, connection);
}
Console.WriteLine($"Deleted All Product");