using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace features.Pages.GoodsDonations
{
    public class AllocateModel : PageModel
    {
        public String errorMessage = "";
        public String successMessage = "";
        public String disasterPicked = "";
        public List<SelectListItem> disasterOptions = new List<SelectListItem>();
        public GoodsDonationsInfo goodsDonationsInfo = new GoodsDonationsInfo();
        public void OnGet()
        {
            String name=Request.Query["name"];
            try
            {
                String connectionString = "Data Source=dafser.database.windows.net;Initial Catalog=AspNetUsers;User ID=dafad;Password=Sunshine123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM GoodsDONO WHERE name=@name";
                    String sql2 = "SELECT * FROM DISASTERS";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               
                                goodsDonationsInfo.name = reader.GetString(0);
                                goodsDonationsInfo.donoDate = reader.GetDateTime(1).ToString();
                                goodsDonationsInfo.noOfItems = reader.GetInt32(2).ToString();
                                goodsDonationsInfo.category = reader.GetString(3);
                                goodsDonationsInfo.donoDesc = reader.GetString(4);
                               

                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {



                                disasterOptions.Add(new SelectListItem { Value = reader.GetString(4), Text = reader.GetString(4) });

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
                String connectionString = "Data Source=dafser.database.windows.net;Initial Catalog=AspNetUsers;User ID=dafad;Password=Sunshine123!;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO GoodsAllocations " + "(Name,DonoDate,noOfItems,category,DonoDesc,AllocatedTo) VALUES" + " (@name,@donoDate,@noOfItems,@category,@donoDesc,@disasterAllo);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", goodsDonationsInfo.name);
                        command.Parameters.AddWithValue("@donoDate", goodsDonationsInfo.donoDate);
                        command.Parameters.AddWithValue("@noOfItems", Int32.Parse(goodsDonationsInfo.noOfItems));
                        command.Parameters.AddWithValue("@category", goodsDonationsInfo.category);
                        command.Parameters.AddWithValue("@donoDesc", goodsDonationsInfo.donoDesc);
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

            goodsDonationsInfo.name = goodsDonationsInfo.donoDate = goodsDonationsInfo.noOfItems = goodsDonationsInfo.donoDesc = goodsDonationsInfo.category = "";
            successMessage = "Goods added correctly";

            Response.Redirect("GoodsDonations/Index");
        }
    }
}

