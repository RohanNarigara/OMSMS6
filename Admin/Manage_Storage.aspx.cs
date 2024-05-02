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
    public partial class Manage_Storage : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        int eid, did;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["eid"] != null)
                {
                    eid = Convert.ToInt32(Request.QueryString["eid"]);
                    fetchInfo();
                }
                else if (Request.QueryString["did"] != null)
                {
                    did = Convert.ToInt32(Request.QueryString["did"]);
                    Delete_Storage();
                }
                else
                {
                    Response.Redirect("../Admin/Other.aspx");
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            SqlCommand checkStorage = new SqlCommand("SELECT * FROM tblStorage WHERE storage = @storage", conn);
            checkStorage.Parameters.AddWithValue("@storage", txtStorage.Text);
            SqlDataReader dr = checkStorage.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                int eid = Convert.ToInt32(Request.QueryString["eid"]);
                SqlCommand updateStorage = new SqlCommand("UPDATE tblStorage SET storage = @storage WHERE id = @id", conn);
                updateStorage.Parameters.AddWithValue("@storage", txtStorage.Text);
                updateStorage.Parameters.AddWithValue("@id", eid);
                int isUpdated = updateStorage.ExecuteNonQuery();
                if (isUpdated > 0)
                {
                    Response.Write("<script>alert('Storage Updated Successfully!'); window.location='../Admin/Other.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Error in Storage Updation!');</script>");
                }
            }
            else
            {
                dr.Close();
                Response.Write("<script>alert('Storage already exists!'); window.location='../Admin/Other.aspx';</script>");
            }
            conn.Close();
        }

        protected void fetchInfo()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectStorage = new SqlCommand("SELECT * FROM tblStorage WHERE id = @id", conn);
            selectStorage.Parameters.AddWithValue("@id", eid);
            SqlDataReader dr = selectStorage.ExecuteReader();
            if (dr.Read())
            {
                txtStorage.Text = dr["storage"].ToString();
            }
            dr.Close();
            conn.Close();
        }

        protected void Delete_Storage()
        {
            conn.Close();
            conn.Open();
            SqlCommand checkStorage = new SqlCommand("SELECT * FROM tblProductDetails WHERE sid = @id", conn);
            checkStorage.Parameters.AddWithValue("@id", did);
            SqlDataReader dr = checkStorage.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand deleteStorage = new SqlCommand("DELETE FROM tblStorage WHERE id = @id", conn);
                int isDeleted = deleteStorage.ExecuteNonQuery();
                if (isDeleted > 0)
                {
                    Response.Write("<script>alert('Storage Deleted Successfully!'); window.location='../Admin/Other.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Error in Storage Deletion!'); window.location='../Admin/Other.aspx';</script>");
                }
            }
            else
            {
                dr.Close();
                Response.Write("<script>alert('Storage cannot be deleted as it is associated with Product.');  window.location='../Admin/Other.aspx';</script>");
            }
            conn.Close();
        }
    }
}