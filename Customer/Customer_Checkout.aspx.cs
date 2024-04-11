using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Customer
{
    public partial class Customer_Checkout : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            //LoadCart();
            /*  bindCityState();*/
        }


        //protected void LoadCart()
        //{
        //    SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");

        //    con.Open();
        //    string uid = "1"; // Assuming the user ID is always "1"
        //    SqlCommand cmd = new SqlCommand("SELECT CP.Id, P.Name AS ProductName, P.ImageName, PD.Price, CP.Quantity FROM tblCartProduct CP JOIN tblProduct P ON CP.Pid = P.Id JOIN tblProductDetail PD ON CP.Pid = PD.Pid WHERE CP.Custid = 1", con);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        DataTable dt = new DataTable();
        //        dt.Load(reader);
        //        viewcartlist.DataSource = dt;
        //        viewcartlist.DataBind();
        //        decimal totalAmount = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Price"]) * row.Field<int>("Quantity"));
        //        lbltotal.Text = string.Format("{0:C}", totalAmount);
        //    }
        //    else
        //    {
        //        // If cart is empty, show message or handle accordingly
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showToastdanget", "showToastdanget('Empty Cart !!!');", true);
        //        /*lbltotal.Visible = false; // Hide total amount label
        //        viewcartlist.Visible = false; // Hide repeater*/

        //    }
        //    con.Close();
        //}

        protected void Cancel_order(object sender, EventArgs e)
        {

            /*  Response.Write("<script>alert('Order has been cancelled!');  </script>");*/
            Response.Redirect("Default.aspx");
        }
        protected void Confirm_order(object sender, EventArgs e)
        {
            // Alert the user that the order has been confirmed
            Response.Write("<script>alert('Order has been confirmed!');</script>");
            // String address = txt_cust_address.Text;

        }

        /* protected void bindCityState()
         {
             con.Close();
             con.Open();
             SqlCommand selectState = new SqlCommand("SELECT * FROM tblState", con);
             SqlDataAdapter daState = new SqlDataAdapter(selectState);
             DataTable dtState = new DataTable();
             daState.Fill(dtState);

             if (dtState.Rows.Count > 0)
             {
                 ddlState.DataSource = dtState;
                 ddlState.DataBind();
             }
             ddlState.SelectedValue = "8";

             SqlCommand selectCity = new SqlCommand("SELECT * FROM tblCity WHERE sid=8", con);
             SqlDataAdapter daCity = new SqlDataAdapter(selectCity);
             DataTable dtCity = new DataTable();
             daCity.Fill(dtCity);
             if (dtCity.Rows.Count > 0)
             {
                 ddlCity.DataSource = dtCity;
                 ddlCity.DataBind();
             }
             ddlCity.SelectedValue = "34";
             con.Close();
         }

         protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
         {
             int sid = Convert.ToInt32(ddlState.SelectedValue);
             if (sid > 0)
             {
                 con.Close();
                 con.Open();
                 SqlCommand selectCity = new SqlCommand("SELECT * FROM tblCity WHERE sid =" + sid, con);
                 SqlDataAdapter da = new SqlDataAdapter(selectCity);
                 DataTable dt = new DataTable();
                 da.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     ddlCity.DataSource = dt;
                     ddlCity.DataBind();
                 }
                 con.Close();
             }
         }*/

    }
}