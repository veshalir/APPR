using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace features.Pages.Disasters
{
    public class IndexModel : PageModel
    {
        public List<DisastersInfo> listDisasters=new List<DisastersInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Server=tcp:dafser.database.windows.net;Initial Catalog=AspNetUsers;Persist Security Info=False;User ID=dafad;Password=Sunshine123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Disasters";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisastersInfo disastersInfo = new DisastersInfo();
                                disastersInfo.requiredAid = reader.GetString(0);
                                disastersInfo.startDate = reader.GetDateTime(1).ToString();
                                disastersInfo.endDate = reader.GetDateTime(2).ToString();
                                disastersInfo.location = reader.GetString(3);
                                disastersInfo.disasterDesc = reader.GetString(4);
                                listDisasters.Add(disastersInfo);

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

    public class DisastersInfo
        {
        public string requiredAid;
        public string startDate;
        public string endDate;


        public string location;

        public string disasterDesc;

            }
}
