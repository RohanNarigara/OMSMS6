using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                countUser();
                countProduct();
                countOrder();
                countBrand();
            }
        }

        protected int countUser()
        {
            conn.Close();
            conn.Open();
            SqlCommand countUser = new SqlCommand("SELECT COUNT(*) FROM tblUsers", conn);
            int count = (int)countUser.ExecuteScalar();
            conn.Close();
            return count;
        }

        protected int countProduct()
        {
            conn.Close();
            conn.Open();
            SqlCommand countProduct = new SqlCommand("SELECT COUNT(*) FROM tblProduct", conn);
            int count = (int)countProduct.ExecuteScalar();
            conn.Close();
            return count;
        }

        protected int countOrder()
        {
            conn.Close();
            conn.Open();
            SqlCommand countOrder = new SqlCommand("SELECT COUNT(*) FROM tblOrder", conn);
            int count = (int)countOrder.ExecuteScalar();
            conn.Close();
            return count;
        }

        protected int countBrand()
        {
            conn.Close();
            conn.Open();
            SqlCommand countCategory = new SqlCommand("SELECT COUNT(*) FROM tblBrand", conn);
            int count = (int)countCategory.ExecuteScalar();
            conn.Close();
            return count;
        }
    }
}