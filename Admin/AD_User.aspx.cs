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
    public partial class AD_User : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        int aid, daid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["aid"] != null)
                {
                    aid = Convert.ToInt32(Request.QueryString["aid"]);
                    Activate_User();
                }
                else if (Request.QueryString["daid"] != null)
                {
                    daid = Convert.ToInt32(Request.QueryString["daid"]);
                    Deactivate_User();
                }
                else
                {
                    Response.Redirect("../Admin/Users.aspx");
                }
            }
        }

        protected void Activate_User()
        {
            conn.Close();
            conn.Open();
            SqlCommand activateUser = new SqlCommand("UPDATE tblUsers SET status = 1 WHERE id = @id", conn);
            activateUser.Parameters.AddWithValue("@id", aid);
            int isActivated = activateUser.ExecuteNonQuery();
            if (isActivated > 0)
            {
                Response.Write("<script>window.location='../Admin/Users.aspx'</script>");
                //Response.Write("<script>alert('User Activated Successfully!'); window.location='../Admin/Users.aspx'</script>");
            }
            else
            {
                Response.Write("<script>alert('Error in User Activation!'); window.location='../Admin/Users.aspx'</script>");
            }
        }

        protected void Deactivate_User()
        {
            conn.Close();
            conn.Open();
            SqlCommand deactivateUser = new SqlCommand("UPDATE tblUsers SET status = 0 WHERE id = @id", conn);
            deactivateUser.Parameters.AddWithValue("@id", daid);
            int isDeactivated = deactivateUser.ExecuteNonQuery();
            if (isDeactivated > 0)
            {
                Response.Write("<script>window.location='../Admin/Users.aspx'</script>");
                //Response.Write("<script>alert('User Deactivated Successfully!'); window.location='../Admin/Users.aspx'</script>");
            }
            else
            {
                Response.Write("<script>alert('Error in User Deactivation!'); window.location='../Admin/Users.aspx'</script>");
            }
        }
    }
}