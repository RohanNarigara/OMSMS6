using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Customer
{
    public partial class Cust_View_All_Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FetchProductsFromDatabase();
            }
        }

        protected void FetchProductsFromDatabase()
        {
            string connectionString = "Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT p.Id, p.Name, p.ImageName, pd.Price FROM tblProduct p INNER JOIN tblProductDetail pd ON p.Id = pd.Pid", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        int productId = rdr.GetInt32(0);
                        string productName = rdr.GetString(1);
                        string imageName = rdr.GetString(2);
                        int productPrice = rdr.GetInt32(3);

                        // Dynamically create HTML elements for product display
                        string productHtml = $@"
                <div class='w-72 bg-white shadow-md rounded-xl duration-500 hover:scale-105 hover:shadow-xl'>
                    <a href='Cust_View_Product_Details.aspx?id={productId}'>
                        <img src='../Res/Images/{imageName}' alt='Product' class='h-80 w-72 object-cover rounded-t-xl' />
                        <div class='px-4 py-3 w-72'>
                            <p class='text-lg font-bold text-black truncate block capitalize'>{imageName}</p>
                            <p class='text-lg font-bold text-black truncate block capitalize'>Price: ${productPrice}</p>
                        </div>
                    </a>
                </div>";

                        // Add the product HTML to the placeholder control or any other container
                        ProductContainer.Controls.Add(new LiteralControl(productHtml));
                    }
                }
                rdr.Close();
            }
        }



        protected void Add_to_Cart(object sender, EventArgs e)
        {
            // alert the user that the product has been added to the cart
            Response.Write("<script>alert('Product has been added to the cart!');</script>");

        }
    }
}