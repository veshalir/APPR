using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace features.Pages.Disasters
{
    public class CreateModel : PageModel
    {
        public DisastersInfo DisastersInfo = new DisastersInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void onPost()
        {
            DisastersInfo.requiredAid = Request.Form["RequiredAid"];
            DisastersInfo.startDate = Request.Form["startDate"];
            DisastersInfo.endDate = Request.Form["endDate"];
            DisastersInfo.location = Request.Form["LOCATION"];
            DisastersInfo.disasterDesc = Request.Form["disasterDesc"];


            if (DisastersInfo.requiredAid.Length == 0 || DisastersInfo.startDate.Length == 0 || DisastersInfo.endDate.Length == 0 || DisastersInfo.location.Length == 0 || DisastersInfo.disasterDesc.Length == 0)
            {
                errorMessage = "All Fields are required";
                return;
            }

            try
            {
                String connectionString = "Server=tcp:dafser.database.windows.net;Initial Catalog=AspNetUsers;Persist Security Info=False;User ID=dafad;Password=Sunshine123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Disasters(REQUIREDAID,STARTDate,ENDDate,LOCATION,DISASTERDesc) VALUES(@requiredAid,@startDate,@endDate,@location,@disasterDesc)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@requiredAid", DisastersInfo.requiredAid);
                        command.Parameters.AddWithValue("@startDate", DisastersInfo.startDate);
                        command.Parameters.AddWithValue("@@endDate", DisastersInfo.endDate);
                        command.Parameters.AddWithValue("@location", DisastersInfo.location);
                        command.Parameters.AddWithValue("@disasterDesc", DisastersInfo.disasterDesc);


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            DisastersInfo.requiredAid = DisastersInfo.startDate = DisastersInfo.endDate = DisastersInfo.location = DisastersInfo.disasterDesc = "";
            successMessage = "Disaster added correctly";

            Response.Redirect("Disasters/Index");
        }
    }
}
