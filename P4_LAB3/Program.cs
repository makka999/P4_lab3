using System;
using System.Data.SqlClient;
using Dapper;
namespace P4_LAB3
{
   
    class Program
    {
        static void Main(string[] args)
        {

            var cstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var conn = new SqlConnection(cstring);

            var regionToInsert = new Region()
            {
                RegionID = 9,
                RegionDescription = "polska"
            };

            var firstL = "B";
            var joinResult = conn.Query<Territories, Region, Territories>($"SELECT * FROM Territories t JOIN Region r ON t.RegionID = r.RegionID WHERE TerritoryDescription LIKE '{firstL}%'",
                (territories, region) =>
                {
                    territories.Region = region;
                    return territories;
                },
                splitOn: "RegionID");
            foreach (var item in joinResult)
            {
                Console.WriteLine($"{item.TerritoryDescription} {item.Region.RegionDescription}");
            }
        }
    }
}
