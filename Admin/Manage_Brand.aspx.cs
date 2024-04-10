using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Manage_Brand : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        int eid, did;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["eid"] != null)
            {
                eid = Convert.ToInt32(Request.QueryString["eid"]);
                fetchInfo();
            }
            else if (Request.QueryString["did"] != null)
            {
                did = Convert.ToInt32(Request.QueryString["did"]);
                Delete_Brand();
            }
            else
            {
                Response.Redirect("../Admin/Other.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            SqlCommand checkBrand = new SqlCommand("SELECT * FROM tblBrand WHERE name = @name", conn);
            checkBrand.Parameters.AddWithValue("@name", txtBrand.Text);
            SqlDataReader dr = checkBrand.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand updateBrand = new SqlCommand("UPDATE tblBrand SET name = @name WHERE id = @id", conn);
                updateBrand.Parameters.AddWithValue("@name", txtBrand.Text);
                updateBrand.Parameters.AddWithValue("@id", eid);
                int isUpdated = updateBrand.ExecuteNonQuery();
                if (isUpdated > 0)
                {
                    Response.Write("<script>alert('Brand Updated Successfully!'); window.location='../Admin/Other.aspx'</script>");
                }
                else
                {
                    Response.Write("<script>alert('Error in Brand Updation!');</script>");
                }
            }
            else
            {
                dr.Close();
                Response.Write("<script>alert('Brand Already Exists!');</script>");
            }
            conn.Close();
        }

        protected void fetchInfo()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectBrand = new SqlCommand("SELECT * FROM tblBrand WHERE id = @id", conn);
            selectBrand.Parameters.AddWithValue("@id", eid);
            SqlDataReader dr = selectBrand.ExecuteReader();
            if (dr.Read())
            {
                txtBrand.Text = dr["name"].ToString();
            }
            dr.Close();
            conn.Close();
        }

        protected void Delete_Brand()
        {
            conn.Close();
            conn.Open();
            SqlCommand checkBrand = new SqlCommand("SELECT * FROM tblProduct WHERE bid = @id", conn);
            checkBrand.Parameters.AddWithValue("@id", did);
            SqlDataReader dr = checkBrand.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand deleteBrand = new SqlCommand("DELETE FROM tblBrand WHERE id = @id", conn);
                deleteBrand.Parameters.AddWithValue("@id", did);
                int isDeleted = deleteBrand.ExecuteNonQuery();
                if (isDeleted > 0)
                {
                    Response.Write("<script>alert('Brand Deleted Successfully!'); window.location='../Admin/Other.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Error in Brand Deletion!'); window.location='../Admin/Other.aspx';</script>");
                }
            }
            else
            {
                dr.Close();
                Response.Write("<script>alert('Brand can not be delete as it is associated with Product!'); window.location='../Admin/Other.aspx';</script>");
            }
            conn.Close();
        }
    }
}