using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Customer
{
    public partial class Customer_Checkout : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCart();
            /*  bindCityState();*/


        }


        protected void LoadCart()
        {
            SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");

            con.Open();
            string uid = "1"; // Assuming the user ID is always "1"
            SqlCommand cmd = new SqlCommand("SELECT CP.Id, P.Name AS ProductName, P.ImageName, PD.Price, CP.Quantity FROM tblCartProduct CP JOIN tblProduct P ON CP.Pid = P.Id JOIN tblProductDetail PD ON CP.Pid = PD.Pid WHERE CP.Custid = 1", con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                viewcartlist.DataSource = dt;
                viewcartlist.DataBind();
                decimal totalAmount = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Price"]) * row.Field<int>("Quantity"));
                lbltotal.Text = string.Format("{0:C}", totalAmount);
            }
            else
            {
                // If cart is empty, show message or handle accordingly
                ScriptManager.RegisterStartupScript(this, GetType(), "showToastdanget", "showToastdanget('Empty Cart !!!');", true);
                /*lbltotal.Visible = false; // Hide total amount label
                viewcartlist.Visible = false; // Hide repeater*/

            }
            con.Close();
        }

        protected void Cancel_order(object sender, EventArgs e)
        {

            /*  Response.Write("<script>alert('Order has been cancelled!');  </script>");*/
            Response.Redirect("Default.aspx");
        }
        protected void Confirm_order(object sender, EventArgs e)
        {
            // Alert the user that the order has been confirmed
            Response.Write("<script>alert('Order has been confirmed!');</script>");
            // String address = txt_cust_address.Text;






        }

      

    }
}