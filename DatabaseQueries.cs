namespace EntegraCaseStudy;

public static class DatabaseQueries
{
    public const string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Product (
                name TEXT,
                productCode TEXT,
                quantity INTEGER
            )
    ";

    public const string insertQuery = @"
                INSERT INTO Product (name, productCode, quantity)
                VALUES (@name, @productCode, @quantity)
        ";

    public const string updateQuary = @"
        UPDATE Product
        SET name = @newName, quantity = @newQuantity
        WHERE productCode = @productCode
    ";

    public const string deleteQuery = @"
        DELETE FROM Product
        Where productCode = @productCode
    ";

    public const string searchQuery = @"
        SELECT *
        FROM Product
        WHERE name LIKE '%' || @searchTerm || '%' OR productCode = @searchTerm
    ";

    public const string selectQuery = "SELECT * FROM Product";
}