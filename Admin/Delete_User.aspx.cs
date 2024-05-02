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
    public partial class Delete_User : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["did"] == null)
                {
                    Response.Redirect("../Admin/Users.aspx");
                }
                DeleteUser();
            }
        }

        protected void DeleteUser()
        {
            conn.Close();
            conn.Open();
            SqlCommand checkUser = new SqlCommand("SELECT * FROM tblOrders WHERE uid = @id", conn);
            checkUser.Parameters.AddWithValue("@id", Convert.ToInt32(Request.QueryString["did"]));
            SqlDataReader dr = checkUser.ExecuteReader();
            if (!dr.Read())
            {
                SqlCommand selectUser = new SqlCommand("DELETE FROM tblUsers WHERE id = @id", conn);
                selectUser.Parameters.AddWithValue("@id", Convert.ToInt32(Request.QueryString["did"]));
                int isDeleted = selectUser.ExecuteNonQuery();
                if (isDeleted > 0)
                {
                    Response.Write("<script>alert('User Deleted Successfully!'); window.location='../Admin/Users.aspx'</script>");
                }
                else
                {
                    Response.Write("<script>alert('Error in User Deletion!'); window.location='../Admin/Users.aspx'</script>");
                }
            } else
            {
                Response.Write("<script>alert('User cannot be deleted as he/she has orders!'); window.location='../Admin/Users.aspx'</script>");
            }
        }
    }
}