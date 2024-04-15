using Microsoft.Data.Sqlite;

namespace EntegraCaseStudy;

public static class DatabaseOperations
{
    public static async Task Initial(SqliteConnection connection)
    {
        await using var cmdCreate = new SqliteCommand(DatabaseQueries.createTableQuery, connection); 
        cmdCreate.ExecuteNonQuery(); 
    }

    public static async Task Select(SqliteConnection connection)
    {
        await using var command = new SqliteCommand(DatabaseQueries.selectQuery, connection); 

        await using var dataReader = await command.ExecuteReaderAsync(); 

        while (await dataReader.ReadAsync())
        {
            Console.WriteLine($"Name: {dataReader["name"]}, Product Code: {dataReader["productCode"]}, Quantity: {dataReader["quantity"]}");
        }
    }

    public static async Task Insert(Product product, SqliteConnection connection)
    {
        await using var command = new SqliteCommand(DatabaseQueries.insertQuery, connection);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@productCode", product.ProductCode);
        command.Parameters.AddWithValue("@quantity", product.Quantity);
        command.ExecuteNonQuery(); 
    }
    
    public static async Task Update(Product product, SqliteConnection connection)
    {
        await using var command = new SqliteCommand(DatabaseQueries.updateQuary, connection);
        command.Parameters.AddWithValue("@newName", product.Name);
        command.Parameters.AddWithValue("@newQuantity", product.Quantity);
        command.Parameters.AddWithValue("@productCode", product.ProductCode);
        command.ExecuteNonQuery(); 
    }
    
    public static async Task Delete(string productCode, SqliteConnection connection)
    {
        await using var command = new SqliteCommand(DatabaseQueries.deleteQuery, connection);
        command.Parameters.AddWithValue("@productCode", productCode);
        await command.ExecuteNonQueryAsync();
    }
    
    public static async Task Search(string searchTerm, SqliteConnection connection)
    {
        await using var command = new SqliteCommand(DatabaseQueries.searchQuery, connection); 
        command.Parameters.AddWithValue("@searchTerm", searchTerm);
        await using var dataReader = await command.ExecuteReaderAsync(); 

        while (await dataReader.ReadAsync())
        {
            Console.WriteLine($"Name: {dataReader["name"]}, Product Code: {dataReader["productCode"]}, Quantity: {dataReader["quantity"]}");
        }
    }
}