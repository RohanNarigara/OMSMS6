using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace OMSMS6.Customer
{
    public partial class Registration : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindState();
                ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "", true);
            }
        }

        // binding City in DropDownList
        protected void bindState()
        {
            conn.Close();
            conn.Open();
            SqlCommand selectState = new SqlCommand("SELECT * FROM tblState", conn);
            SqlDataAdapter daState = new SqlDataAdapter(selectState);
            DataTable dtState = new DataTable();
            daState.Fill(dtState);

            if (dtState.Rows.Count > 0)
            {
                ddlState.DataSource = dtState;
                ddlState.DataBind();
            }
            ddlState.SelectedValue = "8";

            SqlCommand selectCity = new SqlCommand("SELECT * FROM tblCity WHERE sid=8", conn);
            SqlDataAdapter daCity = new SqlDataAdapter(selectCity);
            DataTable dtCity = new DataTable();
            daCity.Fill(dtCity);
            if (dtCity.Rows.Count > 0)
            {
                ddlCity.DataSource = dtCity;
                ddlCity.DataBind();
            }
            ddlCity.SelectedValue = "34";
            conn.Close();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sid = Convert.ToInt32(ddlState.SelectedValue);
            if (sid > 0)
            {
                conn.Close();
                conn.Open();
                SqlCommand selectCity = new SqlCommand("SELECT * FROM tblCity WHERE sid =" + sid, conn);
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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            String name = txtName.Text;
            String email = txtEmail.Text;
            String contact = txtContact.Text;
            String password = txtPassword.Text;
            //String repeatPassword = txtRepeatPassword.Text;
            String gender = null;
            if (rbMale.Checked)
            {
                gender = "Male";
            }
            else if (rbFemale.Checked)
            {
                gender = "Female";
            }
            else if (rbOther.Checked)
            {
                gender = "Other";
            }
            String city = ddlCity.SelectedValue;
            //String address = txtAddress.Text;

            //String alert = "alert('" + name + ", " + email + ", " + contact + ", " + password + ", " + gender + ", " + city + ", " + address +"');";
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", alert, true);

            //ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", "toastr.success('This is a success message from C# code.');", true);

            SHA256 sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password.ToString()));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                sb.Append(hashedBytes[i].ToString("x2"));
            }

            String hashedPassword = sb.ToString();

            conn.Close();
            conn.Open();
            SqlCommand checkUser = new SqlCommand("SELECT * FROM tblUsers WHERE email= '" + email + "'", conn);
            SqlDataReader dr = checkUser.ExecuteReader();
            if (!dr.Read())
            {
                Random random = new Random();
                int OTP = random.Next(100000, 999999);
                Session["name"] = name;
                Session["email"] = email;
                Session["contact"] = contact;
                Session["password"] = hashedPassword;
                Session["gender"] = gender;
                Session["city"] = city;
                //Session["address"] = address;
                Session["OTP"] = OTP;

                bool isSent = SendMail(email, OTP);
                if (isSent)
                {
                    dr.Close();
                    Response.Redirect("Registration_OTP.aspx");
                }
            } else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript", "toastr.error('The Email Address is already registered with Us.');", true);
                Response.Write("<script>alert('This Email is already registered with Us!');</script>");
                dr.Close();
            }
        }

        private bool SendMail(String email, int OTP)
        {
            String smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            String smtpEmail = "21bmiit137@gmail.com";
            String smtpPassword = "htiqurjvqyieppbf";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(smtpEmail);
            mail.To.Add(email);
            mail.Subject = "Mobile Shop";
            mail.Body = $"OTP [One Time Password] for Registration is: {OTP}";

            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
            smtp.Credentials = new NetworkCredential(smtpEmail, smtpPassword);
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return true;
        }
    }
}