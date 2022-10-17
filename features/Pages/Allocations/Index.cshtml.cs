using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace features.Pages.Allocations
{
    public class IndexModel : PageModel
    {
        public List<GoodsAllocationInfo> listGoods = new List<GoodsAllocationInfo>();
        public List<MonetaryAllocationInfo> listMonetarys = new List<MonetaryAllocationInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=dafser.database.windows.net;Initial Catalog=AspNetUsers;User ID=dafad;Password=Sunshine123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM GoodsAllocations";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GoodsAllocationInfo goodsAllocationInfo = new GoodsAllocationInfo();
                                goodsAllocationInfo.name = reader.GetString(0);
                                goodsAllocationInfo.donoDate = reader.GetDateTime(1).ToString();
                                goodsAllocationInfo.noOfItems = reader.GetInt32(2).ToString();
                                goodsAllocationInfo.category = reader.GetString(3);
                                goodsAllocationInfo.donoDesc = reader.GetString(4);
                                goodsAllocationInfo.allocatedTo = reader.GetString(5);
                                listGoods.Add(goodsAllocationInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            try
            {
                String connectionString = "Data Source=dafser.database.windows.net;Initial Catalog=AspNetUsers;User ID=dafad;Password=Sunshine123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM MONETARYALLOCATIONS";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MonetaryAllocationInfo monetaryAllocationInfo = new MonetaryAllocationInfo();
                                monetaryAllocationInfo.name = reader.GetString(0);
                                monetaryAllocationInfo.donoDate = reader.GetDateTime(1).ToString();
                                monetaryAllocationInfo.donoAmount = "$" + reader.GetInt32(2);
                                monetaryAllocationInfo.allocatedTo = reader.GetString(3);
                                listMonetarys.Add(monetaryAllocationInfo);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class GoodsAllocationInfo
    {
        public string? name;
        public string? donoDesc;
        public string? donoDate;
        public string? noOfItems;
        public string? category;
        public string? allocatedTo;
    }

    public class MonetaryAllocationInfo
    {
        public string? name;
        public string? donoDate;
        public string? donoAmount;
        public string? allocatedTo;
    }
}
