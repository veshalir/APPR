using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace features.Pages.MonetaryDonations
{
    public class IndexModel : PageModel
    {
        public List<MonetaryDonationsInfo> listMonetarys = new List<MonetaryDonationsInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Server=tcp:dafser.database.windows.net;Initial Catalog=AspNetUsers;Persist Security Info=False;User ID=dafad;Password=Sunshine123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                { 
                    connection.Open();
                    String sql = "SELECT * FROM MONETARYDONO";
                    using (SqlCommand command= new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                MonetaryDonationsInfo monetaryDonationsInfo = new MonetaryDonationsInfo();
                                monetaryDonationsInfo.name= reader.GetString(0);
                                monetaryDonationsInfo.donoDate = reader.GetDateTime(1).ToString();
                                monetaryDonationsInfo.donoAmount = "$" + reader.GetInt32(2);
                                listMonetarys.Add(monetaryDonationsInfo);

                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: "+ex.ToString());
            }
        }
    }
    public class MonetaryDonationsInfo
    {
        public string? name;
        public string? donoDate;
        public string? donoAmount;
    }
}
