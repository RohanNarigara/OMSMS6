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
    public partial class Add_Brand : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            string name = txtBrand.Text;

            SqlCommand checkBrand = new SqlCommand("SELECT * FROM tblBrand WHERE name = @name", conn);
            checkBrand.Parameters.AddWithValue("@name", name);
            SqlDataReader dr = checkBrand.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand insertBrand = new SqlCommand("INSERT INTO tblBrand (name) VALUES (@name)", conn);
                insertBrand.Parameters.AddWithValue("@name", name);
                int isInserted = insertBrand.ExecuteNonQuery();
                if (isInserted > 0)
                {
                    Response.Write("<script>alert('Brand Added Successfully!'); window.location='../Admin/Other.aspx'</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Error in Brand Addition!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Brand Already Exists!');</script>");
                dr.Close();
            }
        }
    }
}