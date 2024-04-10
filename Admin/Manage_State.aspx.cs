using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Manage_State : System.Web.UI.Page
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
                    Delete_State();
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
            SqlCommand selectState = new SqlCommand("SELECT * FROM tblState WHERE id = @id", conn);
            selectState.Parameters.AddWithValue("@id", eid);
            SqlDataReader dr = selectState.ExecuteReader();
            if (dr.Read())
            {
                txtState.Text = dr["name"].ToString();
            }
            dr.Close();
            conn.Close();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            SqlCommand checkState = new SqlCommand("SELECT * FROM tblState WHERE name = @name", conn);
            checkState.Parameters.AddWithValue("@name", txtState.Text);
            SqlDataReader dr = checkState.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand updateState = new SqlCommand("UPDATE tblState SET name = @name WHERE id = @id", conn);
                updateState.Parameters.AddWithValue("@name", txtState.Text);
                updateState.Parameters.AddWithValue("@id", eid);
                int isUpdated = updateState.ExecuteNonQuery();
                if (isUpdated > 0)
                {
                    Response.Write("<script>alert('State Updated Successfully!'); window.location='../Admin/Other.aspx'</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Error in State Updation!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('State already exists!');</script>");
                dr.Close();
            }
            conn.Close();
        }

        protected void Delete_State()
        {
            conn.Close();
            conn.Open();
            SqlCommand checkState = new SqlCommand("SELECT * FROM tblCity WHERE sid = @id", conn);
            checkState.Parameters.AddWithValue("@id", did);
            SqlDataReader dr = checkState.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand deleteState = new SqlCommand("DELETE FROM tblState WHERE id = @id", conn);
                deleteState.Parameters.AddWithValue("@id", did);
                int isDeleted = deleteState.ExecuteNonQuery();
                if (isDeleted > 0)
                {
                    Response.Write("<script>alert('State Deleted Successfully!'); window.location='../Admin/Other.aspx';</script>");
                }
                else
                {
                    Response.Write("<script>alert('Error in State Deletion!'); window.location='../Admin/Other.aspx';</script>");
                }
            }
            else
            {
                dr.Close();
                Response.Write("<script>alert('State cannot be deleted as it is associated with City.'); window.location='../Admin/Other.aspx';</script>");
            }
            conn.Close();
        }
    }
}