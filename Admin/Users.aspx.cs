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
    public partial class Users : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindAdmins();
                bindCustomers();
            }
        }

        protected void bindAdmins()
        {
            conn.Close();
            conn.Open();
            //SqlCommand selectAdmins = new SqlCommand("SELECT * FROM tblUsers WHERE role = 0", conn);
            SqlCommand selectAdmins = new SqlCommand("SELECT u.*, c.name AS cname from tblUsers u INNER JOIN tblCity c ON u.cityid = c.id WHERE role = 0", conn);
            SqlDataReader dr = selectAdmins.ExecuteReader();
            if (dr.HasRows)
            {
                rptAdmins.DataSource = dr;
                rptAdmins.DataBind();
            }
            dr.Close();
            conn.Close();
        }

        protected void bindCustomers()
        {
            conn.Close();
            conn.Open();
            //SqlCommand selectCustomers = new SqlCommand("SELECT * FROM tblUsers WHERE role = 1", conn);
            SqlCommand selectCustomers = new SqlCommand("SELECT u.*, c.name AS cname FROM tblUsers u INNER JOIN tblCity c ON u.cityid = c.id WHERE role = 1", conn);
            SqlDataReader dr = selectCustomers.ExecuteReader();
            if (dr.HasRows)
            {
                rptCustomers.DataSource = dr;
                rptCustomers.DataBind();
            }
            dr.Close();
            conn.Close();
        }
    }
}