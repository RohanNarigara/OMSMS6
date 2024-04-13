using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Res
{
    public partial class Forgot_Password : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source = LAPTOP-SHON9L4N\\SQLEXPRESS; Initial Catalog=omsms; Integrated Security=True;");
        //SqlConnection conn = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            SqlCommand selectUser = new SqlCommand("SELECT * FROM tblUsers WHERE email = @email", conn);
            selectUser.Parameters.AddWithValue("@email", txtEmail.Text.ToString());
            SqlDataReader dr = selectUser.ExecuteReader();
            if (dr.Read())
            {
                String email = txtEmail.Text.ToString();
                String newPassword = txtPassword.Text.ToString();
                int OTP = new Random().Next(100000, 999999);

                Session["OTP"] = OTP;
                Session["email"] = email;
                Session["userPassword"] = newPassword;

                if (SendMail(email, OTP))
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "toastr.success('OTP sent on your Email Address!')", true);
                    dr.Close();
                    conn.Close();
                    Response.Redirect("Forgot_Password_OTP.aspx");
                }
                else
                {

                    Response.Write("<script>alert('Error in sending Email!');</script>");
                    //ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "toastr.error('Error in sending Email!')", true);
                }

            }
            else
            {

                Response.Write("<script>alert('Email not Found!');</script>");
                //ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "toastr.error('Email not Found!')", true);
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
            mail.Body = $"OTP [One Time Password] for Forgot Password reset is: {OTP}";

            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
            smtp.Credentials = new NetworkCredential(smtpEmail, smtpPassword);
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return true;
        }
    }
}