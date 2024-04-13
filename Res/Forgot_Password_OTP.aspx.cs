using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Res
{
    public partial class Forgot_Password_OTP : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "", true);
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", "toastr.success('OTP sent on your Email Address!')", true);
            }

            if (Session["OTP"] != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "SuccessScript", "toastr.success('OTP sent on your Email Address!')", true);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            String email = (String)Session["email"];
            String newPassword = (String)Session["userPassword"];
            int userOTP = Convert.ToInt32(txtOTP.Text.ToString());
            int OTP = Convert.ToInt32(Session["OTP"]);
            if (userOTP == OTP)
            {
                SqlCommand updatePassword = new SqlCommand("UPDATE tblUsers SET password = @password WHERE email = @email", conn);

                SHA256 sha256 = SHA256.Create();
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(newPassword.ToString()));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    sb.Append(hashedBytes[i].ToString("x2"));
                }

                String hashedPassword = sb.ToString();

                updatePassword.Parameters.AddWithValue("@password", hashedPassword);
                updatePassword.Parameters.AddWithValue("@email", email);
                int isUpdated = updatePassword.ExecuteNonQuery();
                if (isUpdated > 0)
                {
                    Session["OTP"] = null;
                    Session["email"] = null;
                    Session["userPassword"] = null;

                    //ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "toastr.success('Password Updated Successfully!')", true);
                    Response.Write("<script>alert('Password changed Successfully!'); window.location='../Res/Login.aspx'</script>");
                    conn.Close();
                    //Response.Redirect("Login.aspx");
                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "toastr.error('Error in Updating Password!')", true);
                    Response.Write("<script>alert('Error in updating Password!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('OTP not matched!');</script>");
                //ClientScript.RegisterStartupScript(this.GetType(), "Script", "alert('Invalid OTP!')", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "toastr.error('Invalid OTP!')", true);
            }
            conn.Close();
        }
    }
}