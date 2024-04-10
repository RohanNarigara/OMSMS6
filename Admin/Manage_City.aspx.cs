using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Admin
{
    public partial class Manage_City : System.Web.UI.Page
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
                    Delete_City();
                }
                else
                {
                    Response.Redirect("../Admin/Other.aspx");
                }
            }
        }

        protected void fetchInfo()
        {
            int sid = 0;
            // selecting State
            conn.Close();
            conn.Open();
            SqlCommand selectState = new SqlCommand("SELECT s.id FROM tblState s INNER JOIN tblCity c ON s.id = c.sid WHERE c.id = @id", conn);
            selectState.Parameters.AddWithValue("@id", eid);
            SqlDataReader dr = selectState.ExecuteReader();
            if (dr.Read())
            {
                sid = Convert.ToInt32(dr["id"]);
            }
            dr.Close();

            // Binding States
            SqlCommand states = new SqlCommand("SELECT * FROM tblState", conn);
            SqlDataAdapter da = new SqlDataAdapter(states);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ddlState.DataSource = dt;
                ddlState.DataBind();
            }
            ddlState.SelectedValue = sid.ToString();

            // fetch city name
            SqlCommand selectCity = new SqlCommand("SELECT * FROM tblCity WHERE id = @id", conn);
            selectCity.Parameters.AddWithValue("@id", eid);
            SqlDataReader drCity = selectCity.ExecuteReader();
            if (drCity.Read())
            {
                txtCity.Text = drCity["name"].ToString();
            }
            drCity.Close();

            conn.Close();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            string name = txtCity.Text;
            int sid = Convert.ToInt32(ddlState.SelectedValue);
            int eid = Convert.ToInt32(Request.QueryString["eid"]);

            SqlCommand checkCity = new SqlCommand("SELECT * FROM tblCity WHERE name = @name AND sid = @sid", conn);
            checkCity.Parameters.AddWithValue("@name", name);
            checkCity.Parameters.AddWithValue("@sid", sid);
            SqlDataReader dr = checkCity.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand updateCity = new SqlCommand("UPDATE tblCity SET name = @name, sid = @sid WHERE id = @id", conn);
                updateCity.Parameters.AddWithValue("@name", name);
                updateCity.Parameters.AddWithValue("@sid", sid);
                updateCity.Parameters.AddWithValue("@id", eid);
                int isUpdated = updateCity.ExecuteNonQuery();
                if (isUpdated > 0)
                {
                    Response.Write("<script>alert('City Updated Successfully!'); window.location='../Admin/Other.aspx'</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Error in City Updation!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('City Already Exists!');</script>");
                dr.Close();
            }

            conn.Close();
        }

        protected void Delete_City()
        {
            conn.Close();
            conn.Open();
            SqlCommand checkCity = new SqlCommand("SELECT * FROM tblUsers WHERE cityid = @id", conn);
            checkCity.Parameters.AddWithValue("@id", did);
            SqlDataReader dr = checkCity.ExecuteReader();
            if (!dr.Read())
            {
                dr.Close();
                SqlCommand deleteCity = new SqlCommand("DELETE FROM tblCity WHERE id = @id", conn);
                deleteCity.Parameters.AddWithValue("@id", did);
                int isDeleted = deleteCity.ExecuteNonQuery();
                if (isDeleted > 0)
                {
                    Response.Write("<script>alert('City Deleted Successfully!'); window.location='../Admin/Other.aspx';</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Error in City Deletion!'); window.location='../Admin/Other.aspx';</script>");
                }
            }
            else
            {
                dr.Close();
                Response.Write("<script>alert('City can not be delete as it is associated with User!'); window.location='../Admin/Other.aspx';</script>");
            }
            conn.Close();
        }
    }
}