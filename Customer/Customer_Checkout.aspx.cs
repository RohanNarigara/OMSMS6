using Newtonsoft.Json;
using Razorpay.Api;
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

        //SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        String total;
        private const string _key = "rzp_test_Qit3KulorLte0H";
        private const string _secret = "UpV5ntauZ58ccScdVF5XXN4s";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["uid"] == null)
                {
                    //Response.Write("<script>alert('Please Login First'); window.location='../Customer/Default.aspx'</script>");
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
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");

            con.Open();
            int uid = (int)Session["uid"]; // Assuming the user ID is always "1"
            //string uid = "1"; // Assuming the user ID is always "1"
            SqlCommand cmd = new SqlCommand("SELECT CP.Id, P.Name AS ProductName, P.ImageName, PD.Price, CP.Quantity FROM tblCartProduct CP JOIN tblProduct P ON CP.Pid = P.Id JOIN tblProductDetail PD ON CP.Pid = PD.Pid WHERE CP.Custid = " + uid, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                viewcartlist.DataSource = dt;
                viewcartlist.DataBind();
                decimal totalAmount = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Price"]) * row.Field<int>("Quantity"));
                lbltotal.Text = string.Format("&#8377;{0}.00", totalAmount);
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

        //protected void Cancel_order(object sender, EventArgs e)
        //{

        //    /*  Response.Write("<script>alert('Order has been cancelled!');  </script>");*/
        //    Response.Redirect("Default.aspx");
        //}
        protected void Confirm_order(object sender, EventArgs e)
        {
            string pay_type = "";

            String uid = (string)Session["uid"]; // Assuming the user ID is always "1"
            //String uid = "1"; // Assuming the user ID is always "1"
            /*          String u_id = Session["u_id"].ToString();*/

            if (rdbCOD.Checked)
            {
                string inputAmount = (String)Session["orderamount"];
                decimal registrationAmount;
                pay_type = "COD";
                if (Decimal.TryParse(inputAmount, out registrationAmount))
                {
                    Random random = new Random();
                    int oid = random.Next(00001, 999999);
                    String fname = txtfname.Text;
                    String lname = txtlname.Text;
                    String email = txtemail.Text;
                    String phone = txtcono.Text;
                    String address = txtaddress.Text;
                    String city = txtCity.Text;
                    String state = txtState.Text;
                    String pincode = txtZipCode.Text;
                    String finaladdress = address + " " + city + " " + state + " " + pincode;
                    String orderdate = DateTime.Now.ToString("yyyy-MM-dd");

                    Session["oid"] = oid;
                    Session["total"] = lbltotal.Text;
                    Session["pay_type"] = pay_type;
                    Session["payer_name"] = fname + " " + lname;
                    Session["payer_email"] = email;
                    Session["payer_phone"] = phone;

                    Response.Redirect("Success_Order.aspx");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                }
            }
            else
            {
                pay_type = rdbonline.Text.ToString();
                string inputAmount = (String)Session["orderamount"];
                decimal registrationAmount;


                if (Decimal.TryParse(inputAmount, out registrationAmount))
                {
                    decimal amt = registrationAmount;
                    string currency = "INR";
                    string name = "OMSMS";
                    string description = "Mobile Order";
                    string imageLogo = "../Res/Images/logo.png";

                    string profileName = txtfname.Text + " " + txtlname.Text;
                    string profileMobile = txtcono.Text;
                    string profileEmail = txtemail.Text;

                    Session["total"] = total;
                    Session["pay_type"] = pay_type;
                    Session["payer_name"] = profileName;
                    Session["payer_email"] = profileEmail;
                    Session["payer_phone"] = profileMobile;
                    Session["payer_address"] = txtaddress.Text;

                    Dictionary<string, string> notes = new Dictionary<string, string>()
                {
                    { "note 1", "this is a payment note" }, { "note 2", "here another note, you can add max 15 notes" }
                };

                    // alert the total
                    Response.Write("<script>alert('Total Amount: " + total + "');</script>");


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