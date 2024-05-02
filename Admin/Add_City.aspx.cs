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
    public partial class Add_City : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindState();
            }
        }

        protected void bindState()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectState = new SqlCommand("SELECT * FROM tblState", conn);
            SqlDataReader dr = selectState.ExecuteReader();
            if (dr.HasRows)
            {
                ddlState.DataSource = dr;
                ddlState.DataBind();
            }
            conn.Close();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            string name = txtCity.Text;
            int sid = Convert.ToInt32(ddlState.SelectedValue);

            SqlCommand checkCity = new SqlCommand("SELECT * FROM tblCity WHERE name = @name AND sid = @sid", conn);
            checkCity.Parameters.AddWithValue("@name", name);
            checkCity.Parameters.AddWithValue("@sid", sid);
            SqlDataReader dr = checkCity.ExecuteReader();

            if (!dr.Read())
            {
                dr.Close();
                SqlCommand insertCity = new SqlCommand("INSERT INTO tblCity (name, sid) VALUES (@name, @sid)", conn);
                insertCity.Parameters.AddWithValue("@name", name);
                insertCity.Parameters.AddWithValue("@sid", sid);
                int isInserted = insertCity.ExecuteNonQuery();
                if (isInserted > 0)
                {
                    Response.Write("<script>alert('City Added Successfully!'); window.location='../Admin/Other.aspx'</script>");
                }
                else
                {
                    dr.Close();
                    Response.Write("<script>alert('Error in City Addition!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('City Already Exists!');</script>");
                dr.Close();
            }

            conn.Close();
        }
    }
}