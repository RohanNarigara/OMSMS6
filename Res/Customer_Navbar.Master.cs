using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Res
{
    public partial class Customer_Navbar : System.Web.UI.MasterPage
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCartCount();
                //bindBrand();
            }
        }

        private void getCartCount()
        {
            if (Session["uid"] != null)
            {
                int custid = Convert.ToInt32(Session["uid"]);
                //string constr = "Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;";
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

        //protected void bindBrand()
        //{
        //    conn.Close();
        //    conn.Open();
        //    SqlCommand select = new SqlCommand("SELECT * FROM tblBrand", conn);
        //    SqlDataReader dr = select.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        selectBrand.InnerHtml = "";

        //        // Add a default option
        //        selectBrand.InnerHtml += "<option value=''>Select a Product</option>";

        //        // Add products to the select list
        //        while (dr.Read())
        //        {
        //            string productId = dr["id"].ToString();
        //            string productName = dr["name"].ToString();
        //            selectBrand.InnerHtml += "<option value='" + productId + "'>" + productName + "</option>";
        //        }
        //    }
        //    dr.Close();
        //    conn.Close();
        //}

        //protected void RedirectToPage()
        //{
        //    string selectedBrandId = selectBrand.Value;
        //    if (!string.IsNullOrEmpty(selectedBrandId))
        //    {
        //        Response.Redirect("View_Brand_Product.aspx?bid=" + selectedBrandId);
        //    }
        //}
    }
}
