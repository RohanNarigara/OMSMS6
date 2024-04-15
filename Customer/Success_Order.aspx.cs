using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;
using System.Web.Profile;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.EnterpriseServices;
using System.Configuration;

namespace OMSMS6.Customer
{
    public partial class Success_Order : System.Web.UI.Page
    {
        //SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
        //SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        String pay_type = "Online";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["uid"] == null)
            {
                Response.Write("<script>alert('Please Login First'); window.location='../Customer/Default.aspx'</script>");
            }
            else
            {
                binddata();
                
                if (pay_type != "COD")
                {
                    string orderId = (string)Session["oid"].ToString();
                    string total = (string)Session["total"].ToString();
                    string payer_name = (string)Session["payer_name"].ToString();
                    string payer_email = (string)Session["payer_email"].ToString();
                    string payer_phone = (string)Session["payer_phone"].ToString();
                    string address = (string)Session["payer_address"].ToString();

                    string order_status = "Pendding";
                    int payment_status = 1;
                    string payment_type = "Online";

                    try
                    {
                        lbl1.Text = "Payment type :" + pay_type;
                        Label3.Text = "Order id :" + orderId;
                        Label4.Text = "profile name :" + payer_name + " :: amount :: " + total + " :: profile email :" + payer_email + "::: contact :" + payer_phone + "::delvery: " + address;
                        con.Open();



                        int oid = Convert.ToInt32(orderId);
                        int userid = Session['uid'];
                        DateTime currentDate = DateTime.Now;
                        string formattedDate = currentDate.ToString("yyyy-MM-dd");
                        int total1 = Convert.ToInt32(total);

                        SqlCommand cmd = new SqlCommand("INSERT INTO tblOrder (OrderId, CustId, OrderDate, DeliveryAddress, Total, DeliveryStatus, PaymentType, PaymentStatus) VALUES (" + oid + ", " + userid + " , '" + formattedDate + "', '" + address + "', " + Convert.ToInt32(total) + ", 'Pending', 'Online', 1)", con);
                        cmd.ExecuteNonQuery();

                        lbl1.Text = "Payment type :" + pay_type;

                        //foreach (RepeaterItem item in viewcartlist.Items)
                        //{
                        //    Label lblProductId = (Label)item.FindControl("lblProductId");
                        //    Label lblProductName = (Label)item.FindControl("lblProductName");
                        //    Label lblPrice = (Label)item.FindControl("lblPrice");
                        //    Label lblQuantity = (Label)item.FindControl("lblQuantity");
                        //    Label lblColor = (Label)item.FindControl("lblColor");
                        //    Label lblStorage = (Label)item.FindControl("lblStorage");

                        //    int pid = Convert.ToInt32(lblProductId.Text);
                        //    string pname = lblProductName.Text;
                        //    int price = Convert.ToInt32(lblPrice.Text);
                        //    int quantity = Convert.ToInt32(lblQuantity.Text);
                        //    string color = lblColor.Text;
                        //    string storage = lblStorage.Text;
                        //}

                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                        string bill = "genrate";
                        Session["bill"] = bill;
                        string oid1 = (string)Session["oid"].ToString();

                        //Response.Redirect("Cust_Bill.aspx?orderId=" + oid1);
                    }
                }

            }


        }
        private void binddata()
        {
            con.Close();
            con.Open();
            int uid = (int)Session["uid"];
            SqlCommand cmd = new SqlCommand("SELECT CP.CustId, C.Name AS Color, CP.Quantity, P.Id AS ProductId, P.Name AS ProductName, PD.Price, S.Storage FROM tblCartProduct CP INNER JOIN tblProduct P ON CP.Pid = P.Id INNER JOIN tblProductDetail PD ON CP.Pid = PD.Pid INNER JOIN tblColor C ON PD.Cid = C.Id INNER JOIN tblStorage S ON PD.Sid = S.Id WHERE CP.CustId = 1", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                viewcartlist.DataSource = dt;
                viewcartlist.DataBind();
                decimal totalAmount = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Price"]) * row.Field<int>("Quantity"));


            }
            else
            {
                // No items in cart, display appropriate message
                ScriptManager.RegisterStartupScript(this, GetType(), "showToastdanget", "showToastdanget('Empty Cart !!!');", true);

                viewcartlist.Visible = false; // Hide repeater


            }

            con.Close();

        }
    }
}
