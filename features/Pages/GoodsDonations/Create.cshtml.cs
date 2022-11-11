using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace features.Pages.GoodsDonations
{
    public class CreateModel : PageModel
    {
        public GoodsDonationsInfo goodsDonationsInfo = new GoodsDonationsInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void onPost()
        {
            goodsDonationsInfo.name = Request.Form["name"];
            goodsDonationsInfo.donoDate = Request.Form["donoDate"];
            goodsDonationsInfo.noOfItems = Request.Form["noOfItems"];
            goodsDonationsInfo.category = Request.Form["category"];
            goodsDonationsInfo.donoDesc = Request.Form["donoDesc"];


            if (goodsDonationsInfo.name.Length == 0 || goodsDonationsInfo.donoDate.Length == 0 || goodsDonationsInfo.noOfItems.Length == 0 || goodsDonationsInfo.donoDesc.Length == 0 || goodsDonationsInfo.category.Length == 0)
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
                    String sql = "INSERT INTO GoodsDONO " + "(Name,DonoDate,noOfItems,category,DonoDesc) VALUES" + " (@name,@donoDate,@noOfItems,@category,@donoDesc);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", goodsDonationsInfo.name);
                        command.Parameters.AddWithValue("@donoDate", goodsDonationsInfo.donoDate);
                        command.Parameters.AddWithValue("@noOfItems", Int32.Parse(goodsDonationsInfo.noOfItems));
                        command.Parameters.AddWithValue("@category", goodsDonationsInfo.category);
                        command.Parameters.AddWithValue("@donoDesc", goodsDonationsInfo.donoDesc);
                       

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }

            goodsDonationsInfo.name = goodsDonationsInfo.donoDate = goodsDonationsInfo.noOfItems = goodsDonationsInfo.donoDesc = goodsDonationsInfo.category = "";
            successMessage = "Goods added correctly";

            Response.Redirect("GoodsDonations/Index");
        }
    }
}
