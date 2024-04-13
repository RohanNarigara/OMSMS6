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
    public partial class AD_Product : System.Web.UI.Page
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
                    Activate_Product();
                }
                else if (Request.QueryString["daid"] != null)
                {
                    daid = Convert.ToInt32(Request.QueryString["daid"]);
                    Deactivate_Product();
                }
                else
                {
                    Response.Redirect("../Admin/Products.aspx");
                }
            }
        }

        protected void Activate_Product()
        {
            conn.Close();
            conn.Open();
            SqlCommand activateProduct = new SqlCommand("UPDATE tblProduct SET status = 1 WHERE id = @id", conn);
            activateProduct.Parameters.AddWithValue("@id", aid);
            int isActivated = activateProduct.ExecuteNonQuery();
            if (isActivated > 0)
            {
                Response.Write("<script>window.location='../Admin/Products.aspx'</script>");
                //Response.Write("<script>alert('Product Activated Successfully!'); window.location='../Admin/Products.aspx'</script>");
            }
            else
            {
                Response.Write("<script>alert('Error in Product Activation!'); window.location='../Admin/Products.aspx'</script>");
            }
            conn.Close();
        }

        protected void Deactivate_Product()
        {
            conn.Close();
            conn.Open();
            SqlCommand deactivateProduct = new SqlCommand("UPDATE tblProduct SET status = 0 WHERE id = @id", conn);
            deactivateProduct.Parameters.AddWithValue("@id", daid);
            int isDeactivated = deactivateProduct.ExecuteNonQuery();
            if (isDeactivated > 0)
            {
                Response.Write("<script>window.location='../Admin/Products.aspx'</script>");
            }
            else
            {
                Response.Write("<script>alert('Error in Product Deactivation!'); window.location='../Admin/Products.aspx'</script>");
            }
            conn.Close();
        }

    }
}