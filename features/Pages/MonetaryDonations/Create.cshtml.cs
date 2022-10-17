using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace features.Pages.MonetaryDonations
{
    public class CreateModel : PageModel
    {
        public MonetaryDonationsInfo monetaryDonationsInfo = new MonetaryDonationsInfo();
        public string errorMessage="";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void onPost()
        {
            monetaryDonationsInfo.name = Request.Form["name"];
            monetaryDonationsInfo.donoDate = Request.Form["DonoDate"];
            monetaryDonationsInfo.donoAmount = Request.Form["DonoAmount"];

            if(monetaryDonationsInfo.name.Length == 0|| monetaryDonationsInfo.donoDate.Length == 0||monetaryDonationsInfo.donoAmount.Length == 0)
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
                    String sql = "INSERT INTO MONETARYDONO(Name,DonoDate,DonoAmount) VALUES(@name,@donoDate,@donoAmount)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name",monetaryDonationsInfo.name);
                        command.Parameters.AddWithValue("@donoDate",monetaryDonationsInfo.donoDate);
                        command.Parameters.AddWithValue("@donoAmount",Int32.Parse(monetaryDonationsInfo.donoAmount));
                        command.ExecuteNonQuery();
                    }
                }
                    }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            monetaryDonationsInfo.name = monetaryDonationsInfo.donoDate = monetaryDonationsInfo.donoAmount = "";
            successMessage = "Monetary amount added correctly";

            Response.Redirect("MonetaryDonations/Index");
        }
    }
}
