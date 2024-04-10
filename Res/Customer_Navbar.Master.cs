using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Res
{
    public partial class Customer_Navbar : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCartCount();
            }
        }

        private void getCartCount()
        {
            if (Session["Custid"] != null)
            {
                int custid = Convert.ToInt32(Session["Custid"]);
                string constr = "Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM tblCartProduct WHERE Custid = @Custid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Custid", custid);
                    int count = (int)cmd.ExecuteScalar();
                    lblCartCount.Text = count.ToString(); // Set the cart count retrieved from the database
                }
            }
        }

        //protected void btnSignout_Click(object sender, EventArgs e)
        //{
        //    Session.RemoveAll();
        //    Response.Write("<script>alert('Logged Out Successfully!'); window.location='../Customer/Default.aspx';</script>");
        //    //Response.Redirect("../Customer/Default.aspx");
        //}
        //}
    }
}
