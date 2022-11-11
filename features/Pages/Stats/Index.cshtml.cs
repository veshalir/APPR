using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace features.Pages.Stats
{
    public class IndexModel : PageModel
    {
        public List<GoodsAllocationInfo> listGoods = new List<GoodsAllocationInfo>();
        public List<MonetaryAllocationInfo> listMonetarys = new List<MonetaryAllocationInfo>();

        public int goodsCount = 0;
        public int monetaryCount = 0;
        public void OnGet()
        {
            try
            {
                String connectionString = "Server=tcp:dafser.database.windows.net;Initial Catalog=AspNetUsers;Persist Security Info=False;User ID=dafad;Password=Sunshine123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM GoodsAllocations";
                    String sqlG = "SUM(noOfItems) FROM GoodsAllocations";
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

                    using (SqlCommand command = new SqlCommand(sqlG, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                goodsCount= reader.GetInt32(0);

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
                String connectionString = "Server=tcp:dafser.database.windows.net;Initial Catalog=AspNetUsers;Persist Security Info=False;User ID=dafad;Password=Sunshine123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM MONETARYALLOCATIONS";

                    String sqlM = "SUM(DONOAMOUNT) FROM MONETARYALLOCATIONS";
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

                    using (SqlCommand command = new SqlCommand(sqlM, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               monetaryCount+= reader.GetInt32(0);

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
