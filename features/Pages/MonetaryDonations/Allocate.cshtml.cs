using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace features.Pages.MonetaryDonations
{
    public class AllocateModel : PageModel
    {
        public MonetaryDonationsInfo monetaryDonationsInfo = new MonetaryDonationsInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public List<SelectListItem> disasterOptions = new List<SelectListItem>();
        public string disasterPicked = "";



        public void OnGet()
        {
            String name=Request.Query["name"];
            try
            {
                String connectionString = "Server=tcp:dafser.database.windows.net;Initial Catalog=AspNetUsers;Persist Security Info=False;User ID=dafad;Password=Sunshine123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM MONETARYDONO WHERE name=@name";
                    String sql2 = "SELECT * FROM DISASTERS";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            
                            while (reader.Read())
                            {
                                
                                monetaryDonationsInfo.name = reader.GetString(0);
                                monetaryDonationsInfo.donoDate = reader.GetDateTime(1).ToString();
                                monetaryDonationsInfo.donoAmount =  reader.GetInt32(2).ToString();
                               

                            }
                        }
                    }

                    // Retrieving items for dropdown list
                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                              
                                 
                                
                                disasterOptions.Add(new SelectListItem {Value=reader.GetString(4),Text=reader.GetString(4) });

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;  

                throw;
            }
        }

        public void OnPost()
        {
            monetaryDonationsInfo.name = Request.Form["name"];
            monetaryDonationsInfo.donoDate = Request.Form["DonoDate"];
            monetaryDonationsInfo.donoAmount = Request.Form["DonoAmount"];
            disasterPicked = Request.Form["Disaster"];
            if (disasterPicked.Length==0 || monetaryDonationsInfo.name.Length == 0 || monetaryDonationsInfo.donoDate.Length == 0 || monetaryDonationsInfo.donoAmount.Length == 0)
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
                    String sql = "INSERT INTO MONETARYALLOCATIONS(Name,DonoDate,DonoAmount,AllocatedTo) VALUES(@name,@donoDate,@donoAmount,@disasterAllo)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", monetaryDonationsInfo.name);
                        command.Parameters.AddWithValue("@donoDate", monetaryDonationsInfo.donoDate);
                        command.Parameters.AddWithValue("@donoAmount", Int32.Parse(monetaryDonationsInfo.donoAmount));
                        command.Parameters.AddWithValue("@disasterAllo", disasterPicked);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            monetaryDonationsInfo.name = monetaryDonationsInfo.donoDate = monetaryDonationsInfo.donoAmount = "";
            successMessage = "Monetary amount allocates correctly";

            Response.Redirect("/MonetaryDonations/Index");
        }
    }
}
