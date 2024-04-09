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
    public partial class Other : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source = LAPTOP-SHON9L4N\\SQLEXPRESS; Initial Catalog=omsms; Integrated Security=True;");
        string name = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindState();
                bindCity();
                bindBrand();
                bindColor();
                bindStorage();
            }
        }

        protected void bindState()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectStates = new SqlCommand("SELECT * FROM tblState ORDER BY name ASC", conn);
            SqlDataReader dr = selectStates.ExecuteReader();
            if (dr.HasRows)
            {
                rptState.DataSource = dr;
                rptState.DataBind();
            }
            dr.Close();
            conn.Close();
        }

        protected void bindCity()
        {
            conn.Close();
            conn.Open();
            //SqlCommand selectCities = new SqlCommand("SELECT * FROM tblCity ORDER BY sid ASC", conn);
            SqlCommand selectCities = new SqlCommand("SELECT c.id, c.name AS cname, s.name AS sname FROM tblCity c INNER JOIN tblState s ON c.sid = s.id", conn);
            SqlDataReader dr = selectCities.ExecuteReader();
            if (dr.HasRows)
            {
                rptCity.DataSource = dr;
                rptCity.DataBind();
            }
            dr.Close();
            conn.Close();
        }

        protected void bindBrand()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectBrands = new SqlCommand("SELECT * FROM tblBrand ORDER BY name ASC", conn);
            SqlDataReader dr = selectBrands.ExecuteReader();
            if (dr.HasRows)
            {
                rptBrand.DataSource = dr;
                rptBrand.DataBind();
            }
            dr.Close();
            conn.Close();
        }

        protected void bindColor()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectColors = new SqlCommand("SELECT * FROM tblColor ORDER BY name ASC", conn);
            SqlDataReader dr = selectColors.ExecuteReader();
            if (dr.HasRows)
            {
                rptColor.DataSource = dr;
                rptColor.DataBind();
            }
            dr.Close();
            conn.Close();
        }

        protected void bindStorage()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectStorages = new SqlCommand("SELECT * FROM tblStorage ORDER BY storage ASC", conn);
            SqlDataReader dr = selectStorages.ExecuteReader();
            if (dr.HasRows)
            {
                rptStorage.DataSource = dr;
                rptStorage.DataBind();
            }
            dr.Close();
            conn.Close();
        }
    }
}