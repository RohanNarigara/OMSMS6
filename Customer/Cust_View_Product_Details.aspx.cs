using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Customer
{
    public partial class CUst_View_All_Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        protected void btnOrderNow_Click(object sender, EventArgs e)
        {

            Response.Write("<script>alert('Quantity must be a positive integer.');</script>");

        }


        protected void btnAdd_To_Cart_Click(object sender, EventArgs e)
        {
            int productId = 3; // Placeholder for product ID

            int quantity = Convert.ToInt32(lblCount.Text);
            int price = 1000; // Placeholder for product price
            int customerId = 2; // Assuming the customer ID is 1
            int total = price * quantity;

            try
            {

                String constr = "Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string query = "INSERT INTO tblCartProduct (Pid, Quantity, Total, Custid) VALUES (@Pid, @Quantity, @Total, @Custid)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Pid", productId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@Total", total);
                    cmd.Parameters.AddWithValue("@Custid", customerId);

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        // Show success alert and redirect
                        Response.Write("<script>alert('Product added to cart successfully!'); window.location = 'Cart.aspx';</script>");
                    }
                    else
                    {
                        // Show error alert
                        Response.Write("<script>alert('An error occurred while adding the product to cart');</script>");
                    }
                    con.Close(); // Close connection after executing the command
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Response.Write("<script>alert('An error occurred while adding the product to cart: " + ex.Message + "');</script>");
            }

        }










    }
}