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
using OMSMS6.Admin;

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
                        //int userid = Session['uid'];
                        int userid = 7;
                        DateTime currentDate = DateTime.Now;
                        string formattedDate = currentDate.ToString("yyyy-MM-dd");
                        int total1 = Convert.ToInt32(total);

                        SqlCommand cmd = new SqlCommand("INSERT INTO tblOrder (OrderId, CustId, OrderDate, DeliveryAddress, Total, DeliveryStatus, PaymentType, PaymentStatus) VALUES (" + oid + ", " + userid + " , '" + formattedDate + "', '" + address + "', " + Convert.ToInt32(total) + ", 'Pending', 'Online', 1)", con);
                        cmd.ExecuteNonQuery();

                        lbl1.Text = "Payment type :" + pay_type;
                        int uid = (int)Session["uid"];
                        List<int> productIds = GetProductIds(uid);
                        // Insert product details for each product in the cart
                        foreach (int productId in productIds)
                        {

                            // Retrieve quantity for the product
                            int qty = GetProductQuantity(uid, productId);

                            // Insert product details into tblOrderProduct
                            Console.WriteLine($"Inserting product details for product ID: {productId}");

                            string insertProductDetails = "INSERT INTO tblOrderProduct (Orderid, Pid, Quantity, cid, sid) " +
                                                          "SELECT @OrderId, cp.Pid, cp.Quantity, pd.cid, pd.sid " +
                                                          "FROM tblCartProduct cp " +
                                                          "INNER JOIN tblProductDetail pd ON cp.Pid = pd.id " +
                                                          "WHERE cp.CustId = @uid AND cp.Pid = @prdid";
                            SqlCommand cmd1 = new SqlCommand(insertProductDetails, con);
                            cmd1.Parameters.AddWithValue("@uid", uid);
                            cmd1.Parameters.AddWithValue("@OrderId", oid);
                            cmd1.Parameters.AddWithValue("@prdid", productId);

                            int rowsAffected = cmd1.ExecuteNonQuery();
                            if (rowsAffected <= 0)
                            {
                                Response.Write("<script>alert('Error inserting order product details');</script>");
                            }
                        }

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

        private int GetProductQuantity(int userId, int productId)
        {
            int quantity = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    con.Open();
                    string selectQuantityQuery = "SELECT Quantity FROM tblCartProduct WHERE Custid = @uid AND pid = @pid";
                    SqlCommand cmdSelect = new SqlCommand(selectQuantityQuery, con);
                    cmdSelect.Parameters.AddWithValue("@uid", userId);
                    cmdSelect.Parameters.AddWithValue("@pid", productId);
                    SqlDataReader reader = cmdSelect.ExecuteReader();
                    if (reader.Read())
                    {
                        quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error fetching product quantity: " + ex.Message + "');</script>");
            }
            return quantity;
        }

        protected List<int> GetProductIds(int userId)
        {
            List<int> productIds = new List<int>();

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    con.Open();
                    string selectProductIdsQuery = "SELECT PD.id AS ProductID FROM tblCartProduct CP JOIN tblProductDetail PD ON CP.pid = PD.id WHERE CP.Custid = @uid";
                    SqlCommand cmd = new SqlCommand(selectProductIdsQuery, con);
                    cmd.Parameters.AddWithValue("@uid", userId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int productId = reader.GetInt32(reader.GetOrdinal("ProductID"));
                        productIds.Add(productId);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error fetching product IDs: " + ex.Message);
            }

            return productIds;
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
