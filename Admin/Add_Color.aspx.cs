using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Add_Color : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            string name = txtColor.Text;

            SqlCommand checkColor = new SqlCommand("SELECT * FROM tblColor WHERE name = @name", conn);
            checkColor.Parameters.AddWithValue("@name", name);
            SqlDataReader dr = checkColor.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand insertColor = new SqlCommand("INSERT INTO tblColor (name) VALUES (@name)", conn);
                insertColor.Parameters.AddWithValue("@name", name);
                int isInserted = insertColor.ExecuteNonQuery();
                if (isInserted > 0)
                {
                    Response.Write("<script>alert('Color Added Successfully!'); window.location='../Admin/Other.aspx'</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Error in Color Addition!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Color Already Exists!');</script>");
                dr.Close();
            }
        }
    }
}