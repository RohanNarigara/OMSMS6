using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Res
{
    public partial class Edit_Profile : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        int cid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
            {
                Response.Redirect("../Customer/Default.aspx");
            }
            if (!IsPostBack)
            {
                FetchInformation();
                BindDropDownLists();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            SqlCommand updateUser = new SqlCommand("UPDATE tblUsers SET name = @name, contact = @contact, gender = @gender, cityid = @cid, address = @address WHERE id = @uid", conn);
            updateUser.Parameters.AddWithValue("@name", txtName.Text);
            updateUser.Parameters.AddWithValue("@contact", txtContact.Text);
            if (rbMale.Checked)
            {
                updateUser.Parameters.AddWithValue("@gender", "Male");
            } else if (rbFemale.Checked)
            {
                updateUser.Parameters.AddWithValue("@gender", "Female");
            }
            else
            {
                updateUser.Parameters.AddWithValue("@gender", "Other");
            }
            updateUser.Parameters.AddWithValue("@cid", ddlCity.SelectedValue);
            updateUser.Parameters.AddWithValue("@address", txtAddress.Text);
            updateUser.Parameters.AddWithValue("@uid", Session["uid"]);
            updateUser.ExecuteNonQuery();
            conn.Close();
            Response.Write("<script>alert('Profile Updated Successfully!'); window.location='../Customer/Default.aspx'</script>");
            //Response.Redirect("../Customer/Default.aspx");

        }

        protected void BindDropDownLists()
        {
            int sid = 0;
            // selecting State
            conn.Close();
            conn.Open();
            SqlCommand selectState = new SqlCommand("SELECT s.id FROM tblState s INNER JOIN tblCity c ON s.id = c.sid INNER JOIN tblUsers u ON u.cityid = c.id", conn);
            SqlDataReader dr = selectState.ExecuteReader();
            if(dr.Read())
            {
                sid = Convert.ToInt32(dr["id"]);
            }
            dr.Close();

            // Binding States
            SqlCommand states = new SqlCommand("SELECT * FROM tblState", conn);
            SqlDataAdapter da = new SqlDataAdapter(states);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                ddlState.DataSource = dt;
                ddlState.DataBind();
            }
            ddlState.SelectedValue = sid.ToString();

            // Binding City
            SqlCommand city = new SqlCommand("SELECT * FROM tblCity WHERE sid = @sid", conn);
            city.Parameters.AddWithValue("@sid", sid);
            SqlDataAdapter daCity = new SqlDataAdapter(city);
            DataTable dtCity = new DataTable();
            daCity.Fill(dtCity);
            if(dtCity.Rows.Count > 0)
            {
                ddlCity.DataSource = dtCity;
                ddlCity.DataBind();
            }
            ddlCity.SelectedValue = cid.ToString();

            conn.Close();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(ddlState.SelectedValue);
            if (sid > 0)
            {
                conn.Close();
                conn.Open();
                SqlCommand selectCity = new SqlCommand("SELECT * FROM tblCity WHERE sid = " + sid, conn);
                SqlDataAdapter da = new SqlDataAdapter(selectCity);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    ddlCity.DataSource = dt;
                    ddlCity.DataBind();
                }
                conn.Close();
            }
        }

        protected void FetchInformation()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectUser = new SqlCommand("SELECT * FROM tblUsers WHERE id = @id", conn);
            selectUser.Parameters.AddWithValue("@id", Session["uid"]);
            SqlDataReader dr = selectUser.ExecuteReader();
            if (dr.Read())
            {
                // name
                txtName.Text = dr["name"].ToString();

                // email
                txtEmail.Text = dr["email"].ToString();

                // contact
                txtContact.Text = dr["contact"].ToString();

                // gender
                if (dr["gender"].Equals("Male"))
                {
                    rbMale.Checked = true;
                } else if (dr["gender"].Equals("Female"))
                {
                    rbFemale.Checked = true;
                } else
                {
                    rbOther.Checked = true;
                }

                // address
                txtAddress.Text = dr["address"].ToString();

                // City id
                cid = Convert.ToInt32(dr["cityid"]);
                dr.Close();
            }
            else
            {
                dr.Close();
                Response.Redirect("../Customer/Default.aspx");
            }
            conn.Close();
        }
    }
}