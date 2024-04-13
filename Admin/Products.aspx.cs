using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Products : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindProducts();
            }
        }

        protected void bindProducts()
        {
            conn.Close();
            conn.Open();
            //SqlCommand selectAdmins = new SqlCommand("SELECT * FROM tblUsers WHERE role = 0", conn);
            SqlCommand selectAdmins = new SqlCommand("SELECT p.*, b.name AS brand FROM tblProduct p INNER JOIN tblBrand b ON p.bid = b.id", conn);
            SqlDataReader dr = selectAdmins.ExecuteReader();
            if (dr.HasRows)
            {
                rptProducts.DataSource = dr;
                rptProducts.DataBind();
            }
            dr.Close();
            conn.Close();
        }
    }
}