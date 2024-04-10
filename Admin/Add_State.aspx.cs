using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Add_State : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            string name = txtState.Text;

            SqlCommand checkState = new SqlCommand("SELECT * FROM tblState WHERE name = @name", conn);
            checkState.Parameters.AddWithValue("@name", name);
            SqlDataReader dr = checkState.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand insertState = new SqlCommand("INSERT INTO tblState (name) VALUES (@name)", conn);
                insertState.Parameters.AddWithValue("@name", name);
                int isInserted = insertState.ExecuteNonQuery();
                if (isInserted > 0)
                {
                    Response.Write("<script>alert('State Added Successfully!'); window.location='../Admin/Other.aspx'</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Error in State Addition!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('State Already Exists!');</script>");
                dr.Close();
            }
        }

    }
}