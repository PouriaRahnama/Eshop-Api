using Microsoft.EntityFrameworkCore.Migrations;
namespace shop.Data.Extension
{
    public static class Extension
    {
        public static void GenrateSP(this MigrationBuilder migrationBuilder)
        {
            //var sp = @"CREATE PROCEDURE [dbo].[GetCustomers] AS BEGIN Select * From Customers END";
            var sp2 = "CREATE PROCEDURE GetAllProduct @Id int AS Begin Select * From Product where Id=@Id END";
            migrationBuilder.Sql(sp2);
        }


    }
}
