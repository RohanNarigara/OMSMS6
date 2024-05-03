using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

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
            ////SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);


            con.Open();
            //string uid = "1"; // Assuming the user ID is always "1"           
            int uid = (int)Session["uid"]; // Assuming the user ID is always "1"

           // int uid = 7;
            SqlCommand cmd = new SqlCommand("SELECT o.Orderid AS OrderId, u.Name AS CustomerName, MIN(p.imageName) AS Imagename, MIN(p.Name) AS ProductName, MIN(pd.Price) AS Price, SUM(op.Quantity) AS Quantity \r\nFROM tblOrder o \r\nINNER JOIN tblUsers u ON o.CustId = u.Id \r\nINNER JOIN tblOrderProduct op ON o.Orderid = op.Orderid \r\nINNER JOIN tblProduct p ON op.Pid = p.Id \r\nINNER JOIN tblProductDetail pd ON p.Id = pd.Pid AND pd.cid = op.cid AND pd.sid = op.sid WHERE u.Id = @uid GROUP BY o.Orderid, u.Name;", con);
            cmd.Parameters.AddWithValue("@uid", uid);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                viewprevOrders.DataSource = dt;
                viewprevOrders.DataBind();
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