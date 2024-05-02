using Newtonsoft.Json;
using OMSMS6.Admin;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        // SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
        //// SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);


        String total;
        int prodid;
        private int j;
        private const string _key = "rzp_test_Qit3KulorLte0H";
        private const string _secret = "UpV5ntauZ58ccScdVF5XXN4s";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["uid"] == null)
                {
                    Response.Write("<script>alert('Please Login First'); window.location='../Customer/Default.aspx'</script>");
                    Response.Redirect("Default.aspx");


                }
                else
                {
                    LoadCart();
                }
            }
            /*  bindCityState();*/


        }


        protected void LoadCart()
        {

            con.Open();
            int uid = (int)Session["uid"];

            //string uid = "7"; // Assuming the user ID is always "1"
            SqlCommand cmd = new SqlCommand("SELECT CP.Id, P.Name AS ProductName,P.id AS ProductID, P.ImageName, PD.price, CP.Quantity , PD.id as prod_id FROM tblCartProduct CP  JOIN tblProductDetail PD ON CP.pid = PD.id JOIN tblProduct P ON PD.Pid = P.Id WHERE CP.Custid = @uid", con);

            cmd.Parameters.AddWithValue("@uid", uid);
            SqlDataReader reader = cmd.ExecuteReader();
            //prodid = reader.GetInt32(reader.GetOrdinal("ProductID"));

            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                viewcartlist.DataSource = dt;
                viewcartlist.DataBind();
                decimal totalAmount = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Price"]) * row.Field<int>("Quantity"));
                lbltotal.Text = totalAmount.ToString();
                Session["orderamount"] = string.Format("{0}", totalAmount);


            }
            else
            {
                // If cart is empty, show message or handle accordingly
                ScriptManager.RegisterStartupScript(this, GetType(), "showToastdanget", "showToastdanget('Empty Cart !!!');", true);
                /*lbltotal.Visible = false; // Hide total amount label
                viewcartlist.Visible = false; // Hide repeater*/

            }
            con.Close();
        }
        // Modify the method to fetch product IDs dynamically based on the user's cart items
        protected List<int> GetProductIds(int userId)
        {
            List<int> productIds = new List<int>();

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT PD.id AS ProductID FROM tblCartProduct CP JOIN tblProductDetail PD ON CP.pid = PD.id WHERE CP.Custid = @uid", con);
                cmd.Parameters.AddWithValue("@uid", userId);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int productId = reader.GetInt32(reader.GetOrdinal("ProductID"));
                    productIds.Add(productId);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error fetching product IDs: " + ex.Message);
            }

            return productIds;
        }

        protected void Confirm_order(object sender, EventArgs e)
        {

            Random random = new Random();
            int oid = random.Next(1, 999999); // Generate a random Order id
            Session["oid"] = oid;

            string pay_type = "";
            int uid = (int)Session["uid"]; // Assuming the user ID is always "1"

            if (rdbCOD.Checked)
            {

                int grandtotal;
                if (int.TryParse(lbltotal.Text, out grandtotal))
                {
                    pay_type = "COD";

                    string fname = txtfname.Text;
                    string lname = txtlname.Text;
                    string email = txtemail.Text;
                    string phone = txtcono.Text;
                    string address = txtaddress.Text;
                    string city = txtCity.Text;
                    int totalamt = Convert.ToInt32(lbltotal.Text);
                    string state = txtState.Text;
                    string pincode = txtZipCode.Text;
                    string finaladdress = address + " " + city + " " + state + " " + pincode;
                    string orderdate = DateTime.Now.ToString("yyyy-MM-dd");

                    try
                    {

                        List<int> productIds = GetProductIds(uid);
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                        {
                            con.Open();
                            string insertOrderQuery = "INSERT INTO tblOrder (Orderid, CustId, OrderDate, DeliveryAddress, Total, DeliveryStatus, PaymentType, PaymentStatus) " +
                            "VALUES (@oid, @uid, @orderdate, @address, @total, 'Pending', @pay_type, 0)";
                            SqlCommand cmd = new SqlCommand(insertOrderQuery, con);
                            cmd.Parameters.AddWithValue("@oid", oid);
                            cmd.Parameters.AddWithValue("@uid", uid);
                            cmd.Parameters.AddWithValue("@orderdate", orderdate);
                            cmd.Parameters.AddWithValue("@address", finaladdress);
                            cmd.Parameters.AddWithValue("@total", totalamt);
                            cmd.Parameters.AddWithValue("@pay_type", pay_type);
                            int i = cmd.ExecuteNonQuery();



                            // Fetch product details from the cart
                            string selectProductDetails = "SELECT CP.pid AS ProductID, CP.Quantity FROM tblCartProduct CP WHERE CP.Custid = @uid";
                            SqlCommand cmdSelect = new SqlCommand(selectProductDetails, con);
                            cmdSelect.Parameters.AddWithValue("@uid", uid);
                            SqlDataReader reader = cmdSelect.ExecuteReader();

                            // Insert order product details
                            string insertProductDetails = "INSERT INTO tblOrderProduct (OrderID, ProductID, Quantity) VALUES (@oid, @prdid, @qty)";
                            SqlCommand cmd1 = new SqlCommand(insertProductDetails, con);

                            try
                            {
                                // count the number of rows in reader
                                int count = 0;
                              
                                while (reader.Read())
                                {

                                    int prdid = reader.GetInt32(reader.GetOrdinal("ProductID"));
                                    string p  = reader["ProductID"].ToString();
                                    p = "Product ID : ";
                                    int qty = reader.GetInt32(reader.GetOrdinal("Quantity"));

                                    Response.Write("<script>alert('Product ID :  ' "+qty+");</script>");

                                   
                                    // Set parameters for the insert command
                                    cmd1.Parameters.Clear();
                                    cmd1.Parameters.AddWithValue("@oid", oid);
                                    cmd1.Parameters.AddWithValue("@prdid", prdid);
                                    cmd1.Parameters.AddWithValue("@qty", qty);

                                    // Execute the insert command
                                    int rowsAffected = cmd1.ExecuteNonQuery();
                                    count++;

                                    if (rowsAffected <= 0)
                                    {
                                         // alert user if the order product details were not inserted successfully
                                         Response.Write("<script>alert('Error inserting order product details');</script>");
                                    }
                                    else
                                    {
                                        Response.Write("<script>alert('Success product details');</script>");


                                        // Delete the product from the cart
                                        //string deleteProduct = "DELETE FROM tblCartProduct WHERE Custid = @uid AND pid = @prdid";
                                        //SqlCommand cmdDelete = new SqlCommand(deleteProduct, con);
                                        //cmdDelete.Parameters.AddWithValue("@uid", uid);
                                        //cmdDelete.Parameters.AddWithValue("@prdid", prdid);
                                        //int rowsDeleted = cmdDelete.ExecuteNonQuery();
                                        //if (rowsDeleted <= 0)
                                        //{
                                        //    // alert user if the product was not deleted from the cart
                                        //    Response.Write("<script>alert('Error deleting product from cart');</script>");
                                        //}
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Handle exceptions
                                Console.WriteLine("Error inserting order product details: " + ex.Message);
                            }
                            finally
                            {
                                // Close the SqlDataReader
                                reader.Close();
                            }



                            // Check if order insertion was successful
                            if (i > 0 && j > 0)
                            {
                                // Order placed successfully
                                Response.Write("<script>alert('Order has been placed successfully!');</script>");
                            }
                            emptyInputbox();
                        }
                        //  Response.Redirect("Success_Order.aspx");

                    }
                    catch (Exception ex)
                    {

                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
                else
                {
                    // lbltotal.Text is not a valid integer
                    Response.Write("<script>alert('Total amount is not a valid number.');</script>");
                }

            }

            else
            {
                pay_type = rdbonline.Text.ToString();
                string inputAmount = lbltotal.Text.ToString();
                decimal registrationAmount;


                int grandtotal;
                if (int.TryParse(inputAmount, out grandtotal))
                {
                    decimal amt = grandtotal;
                    string currency = "INR";
                    string name = "OMSMS";
                    string description = "Mobile Order";
                    string imageLogo = "../Res/Images/logo.png";

                    string profileName = txtfname.Text + " " + txtlname.Text;
                    string profileMobile = txtcono.Text;
                    string profileEmail = txtemail.Text;
                    String address = txtaddress.Text;
                    String city = txtCity.Text;
                    String state = txtState.Text;
                    String pincode = txtZipCode.Text;
                    String finaladdress = address + " " + city + " " + state + " " + pincode;

                    Session["total"] = amt;
                    Session["pay_type"] = pay_type;
                    Session["payer_name"] = profileName;
                    Session["payer_email"] = profileEmail;
                    Session["payer_phone"] = profileMobile;
                    Session["payer_address"] = finaladdress;

                    Dictionary<string, string> notes = new Dictionary<string, string>()
                {
                    { "note 1", "this is a payment note" }, { "note 2", "here another note, you can add max 15 notes" }
                };


                    string orderId = CreateOrder(amt, currency, notes);
                    string jsFunction = "OpenPaymentWindow('" + _key + "', '" + amt + "', '" + currency + "', '" + name + "', '" + description + "', '" + imageLogo + "', '" + orderId + "', '" + profileName + "', '" + profileEmail + "', '" + profileMobile + "', '" + JsonConvert.SerializeObject(notes) + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenPaymentWindow", jsFunction, true);
                }
                else
                {
                    // Handle the case where the user input is not a valid decimal
                    // For example:
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                }

            }
        }

        private void emptyInputbox()
        {
            txtfname.Text = "";
            txtlname.Text = "";
            txtemail.Text = "";
            txtcono.Text = "";
            txtaddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZipCode.Text = "";
        }

        private string CreateOrder(decimal amountInSubunits, string currency, Dictionary<string, string> notes)
        {
            try
            {
                int paymentCapture = 1;

                RazorpayClient client = new RazorpayClient(_key, _secret);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", amountInSubunits);
                options.Add("currency", currency);
                options.Add("payment_capture", paymentCapture);
                options.Add("notes", notes);

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                System.Net.ServicePointManager.Expect100Continue = false;

                Order orderResponse = client.Order.Create(options);
                var orderId = orderResponse.Attributes["id"].ToString();
                return orderId;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}