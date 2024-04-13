using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Customer
{
    public partial class Cust_viiew_prev_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["uid"] == null)
                {
                    Response.Write("<script>alert('Please Login First'); window.location='../Customer/Default.aspx'</script>");
                }
                else
                {
                    LoadCart();
                }
            }

        }


        protected void LoadCart()
        {
            //SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
            con.Open();
            //string uid = "1"; // Assuming the user ID is always "1"           
            //int uid = (int)Session["uid"]; // Assuming the user ID is always "1"
            int uid = 1;
            SqlCommand cmd = new SqlCommand("SELECT o.Id AS OrderId, u.name AS CustomerName, p.name AS ProductName, pd.price, op.Quantity FROM tblOrder o INNER JOIN tblUsers u ON o.CustId = u.id INNER JOIN tblOrderProduct op ON o.Id = op.Oid INNER JOIN tblProduct p ON op.Pid = p.id INNER JOIN tblProductDetail pd ON p.id = pd.pid WHERE u.id = @uid;", con);
            cmd.Parameters.AddWithValue("@uid", uid);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                viewcartlist.DataSource = dt;
                viewcartlist.DataBind();
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

    }
}