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
                String connectionString = "Data Source=dafser.database.windows.net;Initial Catalog=AspNetUsers;User ID=dafad;Password=Sunshine123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
