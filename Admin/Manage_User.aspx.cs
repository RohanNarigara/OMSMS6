using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Configuration;

namespace OMSMS6.Admin
{
    public partial class Manage_User : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        int cid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Request.QueryString["eid"] == null)
                {
                    Response.Redirect("../Admin/Users.aspx");
                }

                FetchInformation();
                BindDropDownLists();
                ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "", true);
            }
        }

        protected void BindDropDownLists()
        {
            int sid = 0;
            // selecting State
            conn.Close();
            conn.Open();
            SqlCommand selectState = new SqlCommand("SELECT s.id FROM tblState s INNER JOIN tblCity c ON s.id = c.sid INNER JOIN tblUsers u ON u.cityid = c.id", conn);
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

            // Binding City
            SqlCommand city = new SqlCommand("SELECT * FROM tblCity WHERE sid = @sid", conn);
            city.Parameters.AddWithValue("@sid", sid);
            SqlDataAdapter daCity = new SqlDataAdapter(city);
            DataTable dtCity = new DataTable();
            daCity.Fill(dtCity);
            if (dtCity.Rows.Count > 0)
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
            selectUser.Parameters.AddWithValue("@id", Convert.ToInt32(Request.QueryString["eid"]));
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
                }
                else if (dr["gender"].Equals("Female"))
                {
                    rbFemale.Checked = true;
                }
                else
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            String name = txtName.Text;
            String contact = txtContact.Text;
            //String repeatPassword = txtRepeatPassword.Text;
            String city = ddlCity.SelectedValue;
            String address = txtAddress.Text;
            int id = Convert.ToInt32(Request.QueryString["eid"]);

            //String alert = "alert('" + name + ", " + email + ", " + contact + ", " + password + ", " + gender + ", " + city + ", " + address +"');";
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", alert, true);

            //ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", "toastr.success('This is a success message from C# code.');", true);

            conn.Close();
            conn.Open();
            // Updating User
            SqlCommand update = new SqlCommand("UPDATE tblUsers SET name = @name, contact = @contact, gender = @gender, cityid = @city, address = @address WHERE id = @id", conn);
            update.Parameters.AddWithValue("@name", name);
            update.Parameters.AddWithValue("@contact", contact);
            if (rbMale.Checked)
            {
                update.Parameters.AddWithValue("@gender", "Male");
            }
            else if (rbFemale.Checked)
            {
                update.Parameters.AddWithValue("@gender", "Female");
            }
            else
            {
                update.Parameters.AddWithValue("@gender", "Other");
            }
            update.Parameters.AddWithValue("@city", city);
            update.Parameters.AddWithValue("@address", address);
            update.Parameters.AddWithValue("@id", id);
            int isUpdated = update.ExecuteNonQuery();
            conn.Close();
            if (isUpdated > 0)
            {
                Response.Write("<script>alert('User Updated Successfully!'); window.location='../Admin/Users.aspx'</script>");
            }
            else
            {
                Response.Write("<script>alert('Error in Updating User!');</script>");
            }
        }
    }
}