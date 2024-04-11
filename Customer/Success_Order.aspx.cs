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

namespace OMSMS6.Customer
{
    public partial class Success_Order : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();

                String pay_type = Session["pay_type"].ToString();
                Response.Write("<script>alert('Order   ',"+pay_type+");</script>");

                if (pay_type == "COD")
                {
                    String orderId = (String)Session["oid"].ToString();
                    String total = (String)Session["total"].ToString();
                    
                    String payer_name = (String)Session["payer_name"].ToString();
                    String payer_email = (String)Session["payer_email"].ToString();
                    String payer_phone = (String)Session["payer_phone"].ToString();
                    String address = (String)Session["address"].ToString();

                    String order_status = "Pendding";
                    int payment_status = 0;
                    String payment_type = "COD";

                    lbl1.Text = "Payment type :" + pay_type;
                    Label3.Text = "Order id :" + orderId;
                    Label4.Text = "profile name :" + payer_name + " :: amount :: " + total + " :: profile email :" + payer_email + "::: contact :" + payer_phone + "::delvery: " + address;

                    try
                    {

                        lbl1.Text = "Payment type :" + pay_type;
                        Label3.Text = "Order id :" + orderId;
                        Label4.Text = "profile name :" + payer_name + " :: amount :: " + total + " :: profile email :" + payer_email + "::: contact :" + payer_phone + "::delvery: " + address;
                        con.Open();


                        int oid = Convert.ToInt32(orderId);
                        int userid = 1;

                        SqlCommand cmd = new SqlCommand("INSERT INTO tblOrder (OrderId, CustId, OrderDate, DeliveryAddress, Total, DeliveryStatus, PaymentType, PaymentStatus) VALUES (" + oid + ", "+userid+" , GETDATE(), '" + address + "', " + Convert.ToInt32(total) + ", '" + order_status + "', '" + payment_type + "', " + Convert.ToInt32(payment_status) + ")", con);
                        cmd.ExecuteNonQuery();


                        lbl1.Text = "Payment type :" + pay_type;
                        Label3.Text = "Order id :" + orderId;
                        Label4.Text = "profile name :" + payer_name + " :: amount :: " + total + " :: profile email :" + payer_email + "::: contact :" + payer_phone + "::delvery: " + address;

                        foreach (RepeaterItem item in viewcartlist.Items)
                        {
                            Label lblProductId = (Label)item.FindControl("lblProductId");
                            Label lblProductName = (Label)item.FindControl("lblProductName");
                            Label lblPrice = (Label)item.FindControl("lblPrice");
                            Label lblQuantity = (Label)item.FindControl("lblQuantity");
                            Label lblColor = (Label)item.FindControl("lblColor");
                            Label lblStorage = (Label)item.FindControl("lblStorage");

                            int pid = Convert.ToInt32(lblProductId.Text);
                            String pname = lblProductName.Text;
                            int price = Convert.ToInt32(lblPrice.Text);
                            int quantity = Convert.ToInt32(lblQuantity.Text);
                            String color = lblColor.Text;
                            String storage = lblStorage.Text;

                      /*      SqlCommand cmd1 = new SqlCommand("INSERT INTO tblOrderProduct (OrderId, Pid, Pname, Price, Quantity, Color, Storage) VALUES (" + oid + ", " + pid + ", '" + pname + "', " + price + ", " + quantity + ", '" + color + "', '" + storage + "')", con);
                            cmd1.ExecuteNonQuery();*/
                        }
                        Label4.Text = "Stock complted";




                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                        String bill = "genrate";
                        Session["bill"] = bill;
                        String oid1 = (String)Session["oid"].ToString();

                        Response.Redirect("Cust_Bill.aspx?orderId=" + oid1);
                    }
                }
            }
            else
            {
                // in case of payment is not COD
                String pay_type = Session["pay_type"].ToString();
                if (pay_type == "Online")
                {
                    String orderId = (String)Session["oid"].ToString();
                    String total = (String)Session["total"].ToString();
                    String payer_name = (String)Session["payer_name"].ToString();
                    String payer_email = (String)Session["payer_email"].ToString();
                    String payer_phone = (String)Session["payer_phone"].ToString();
                    String address = (String)Session["address"].ToString();

                    String order_status = "Pendding";
                    int payment_status = 1;
                    String payment_type = "Online";

                    lbl1.Text = "Payment type :" + pay_type;
                    Label3.Text = "Order id :" + orderId;
                    Label4.Text = "profile name :" + payer_name + " :: amount :: " + total + " :: profile email :" + payer_email + "::: contact :" + payer_phone + "::delvery: " + address;

                    try
                    {
                        lbl1.Text = "Payment type :" + pay_type;
                        Label3.Text = "Order id :" + orderId;
                        Label4.Text = "profile name :" + payer_name + " :: amount :: " + total + " :: profile email :" + payer_email + "::: contact :" + payer_phone + "::delvery: " + address;
                        con.Open();

                        int oid = Convert.ToInt32(orderId);
                        int userid = 1;

                        SqlCommand cmd = new SqlCommand("INSERT INTO tblOrder (OrderId, CustId, OrderDate, DeliveryAddress, Total, DeliveryStatus, PaymentType, PaymentStatus) VALUES (" + oid + ", " + userid + " , GETDATE(), '" + address + "', " + Convert.ToInt32(total) + ", '" + order_status + "', '" + payment_type + "', " + Convert.ToInt32(payment_status) + ")", con);
                        cmd.ExecuteNonQuery();

                        lbl1.Text = "Payment type :" + pay_type;

                        foreach(RepeaterItem item in viewcartlist.Items)
                        {
                            Label lblProductId = (Label)item.FindControl("lblProductId");
                            Label lblProductName = (Label)item.FindControl("lblProductName");
                            Label lblPrice = (Label)item.FindControl("lblPrice");
                            Label lblQuantity = (Label)item.FindControl("lblQuantity");
                            Label lblColor = (Label)item.FindControl("lblColor");
                            Label lblStorage = (Label)item.FindControl("lblStorage");

                            int pid = Convert.ToInt32(lblProductId.Text);
                            String pname = lblProductName.Text;
                            int price = Convert.ToInt32(lblPrice.Text);
                            int quantity = Convert.ToInt32(lblQuantity.Text);
                            String color = lblColor.Text;
                            String storage = lblStorage.Text;

/*
                            int oid = Convert.ToInt32(orderId);
                            int userid = 1;*/

                            /*SqlCommand cmd = new SqlCommand("INSERT INTO tblOrderProduct (OrderId, C, Pname, Price, Quantity, Color, Storage) VALUES (" + oid + ", " + pid + ", '" + pname + "', " + price + ", " + quantity + ", '" + color + "', '" + storage + "')", con);
                            cmd.ExecuteNonQuery();*/
                        }




                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                        String bill = "genrate";
                        Session["bill"] = bill;
                        String oid1 = (String)Session["oid"].ToString();

                        Response.Redirect("Cust_Bill.aspx?orderId="+ oid1);
                    }
                }
            }
        }
        private void binddata()
        {
            con.Open();
                     
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
