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
    public partial class Add_Storage : System.Web.UI.Page
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
            string storage = txtStorage.Text;

            SqlCommand checkStorage = new SqlCommand("SELECT * FROM tblStorage WHERE storage = @storage", conn);
            checkStorage.Parameters.AddWithValue("@storage", storage);
            SqlDataReader dr = checkStorage.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand insertStorage = new SqlCommand("INSERT INTO tblStorage (storage) VALUES (@storage)", conn);
                insertStorage.Parameters.AddWithValue("@storage", storage);
                int isInserted = insertStorage.ExecuteNonQuery();
                if (isInserted > 0)
                {
                    Response.Write("<script>alert('Storage Added Successfully!'); window.location='../Admin/Other.aspx'</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Error in Storage Addition!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Storage Already Exists!');</script>");
                dr.Close();
            }
        }
    }
}