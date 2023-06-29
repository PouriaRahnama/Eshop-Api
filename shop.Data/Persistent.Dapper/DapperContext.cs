using System.Data;
using Microsoft.Data.SqlClient;

namespace shop.Data.Persistent.Dapper;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);

    public string ProductCategory => "[dbo].ProductCategory";
    public string Inventories => "[dbo].SellerInventory";
    public string Products => "[dbo].Product";
    public string Sellers => "[dbo].Seller";

}