using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Manage_Color : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
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
                    Delete_Color();
                }
                else
                {
                    Response.Redirect("../Admin/Other.aspx");
                }
            }
        }

        protected void fetchInfo()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectColor = new SqlCommand("SELECT * FROM tblColor WHERE id = @id", conn);
            selectColor.Parameters.AddWithValue("@id", eid);
            SqlDataReader dr = selectColor.ExecuteReader();
            if (dr.Read())
            {
                txtColor.Text = dr["name"].ToString();
            }
            dr.Close();
            conn.Close();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            SqlCommand checkColor = new SqlCommand("SELECT * FROM tblColor WHERE name = @name", conn);
            checkColor.Parameters.AddWithValue("@name", txtColor.Text);
            SqlDataReader dr = checkColor.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                int eid = Convert.ToInt32(Request.QueryString["eid"]);
                SqlCommand updateColor = new SqlCommand("UPDATE tblColor SET name = @name WHERE id = @id", conn);
                updateColor.Parameters.AddWithValue("@name", txtColor.Text);
                updateColor.Parameters.AddWithValue("@id", eid);
                int isUpdated = updateColor.ExecuteNonQuery();
                if (isUpdated > 0)
                {
                    Response.Write("<script>alert('Color Updated Successfully!'); window.location='../Admin/Other.aspx'</script>");
                }
                else
                {
                    Response.Write("<script>alert('Error in Color Updation!');</script>");
                }
            }
            else
            {
                dr.Close();
                Response.Write("<script>alert('Color Already Exists!');</script>");
            }
            conn.Close();
        }

        protected void Delete_Color()
        {
            conn.Close();
            conn.Open();
            SqlCommand checkColor = new SqlCommand("SELECT * FROM tblProductDetail WHERE cid = @id", conn);
            checkColor.Parameters.AddWithValue("@id", did);
            SqlDataReader dr = checkColor.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand deleteColor = new SqlCommand("DELETE FROM tblColor WHERE id = @id", conn);
                deleteColor.Parameters.AddWithValue("@id", did);
                int isDeleted = deleteColor.ExecuteNonQuery();
                if (isDeleted > 0)
                {
                    Response.Write("<script>alert('Color Deleted Successfully!'); window.location='../Admin/Other.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Error in Color Deletion!'); window.location='../Admin/Other.aspx';</script>");
                }
            }
            else
            {
                dr.Close();
                Response.Write("<script>alert('Color can not be delete as it is associated with Product!'); window.location='../Admin/Other.aspx';</script>");
            }
            conn.Close();
        }
    }
}