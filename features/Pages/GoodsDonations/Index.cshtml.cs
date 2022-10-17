using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace features.Pages.GoodsDonations
{
    public class IndexModel : PageModel
    {
        public List<GoodsDonationsInfo> listGoods = new List<GoodsDonationsInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=dafser.database.windows.net;Initial Catalog=AspNetUsers;User ID=dafad;Password=Sunshine123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM GoodsDONO";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GoodsDonationsInfo goodsDonationsInfo = new GoodsDonationsInfo();
                                goodsDonationsInfo.name = reader.GetString(0);
                                goodsDonationsInfo.donoDate = reader.GetDateTime(1).ToString();
                                goodsDonationsInfo.noOfItems =  reader.GetInt32(2).ToString();
                                goodsDonationsInfo.category =  reader.GetString(3);
                                goodsDonationsInfo.donoDesc =  reader.GetString(4);
                                listGoods.Add(goodsDonationsInfo);

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

    public class GoodsDonationsInfo
    {
        public string name;
        public string donoDesc;
        public string donoDate;
        public string noOfItems;
        public string category;
    }
}
