using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

namespace OMSMS6.Res
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ToastrScript", "", true);
                if (Request.Cookies["Email"] != null && Request.Cookies["Password"] != null)
                {
                    txtEmail.Text = Request.Cookies["Email"].Value;
                    txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            String email = txtEmail.Text.ToString();
            String password = txtPassword.Text.ToString();

            conn.Close();
            conn.Open();
            SqlCommand selectUser = new SqlCommand("SELECT * FROM tblUsers WHERE email = @email", conn);
            selectUser.Parameters.AddWithValue("@email", email);

            SqlDataReader dr = selectUser.ExecuteReader();
            if (dr.Read())
            {
                if ((int)dr["status"] == 1)
                {
                    if (chbRemme.Checked)
                    {
                        // Adding Cookies
                        Response.Cookies["Email"].Value = email;
                        Response.Cookies["Password"].Value = password;

                        // Setting Cookies Time-Out
                        Response.Cookies["Email"].Expires = DateTime.Now.AddDays(7);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(7);
                    }

                    SHA256 sha256 = SHA256.Create();
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashedBytes.Length; i++)
                    {
                        sb.Append(hashedBytes[i].ToString("x2"));
                    }

                    String hashedPassword = sb.ToString();

                    if (dr["password"].ToString() == hashedPassword)
                    {
                        if ((int)dr["role"] == 0)
                        {
                            Session["AdminEmail"] = dr["email"].ToString();
                            Session["AdminName"] = dr["name"].ToString();
                            Session["aid"] = dr["id"].ToString();
                            conn.Close();
                            Response.Redirect("~/Admin/Default.aspx");
                        }
                        else
                        {
                            Session["UserEmail"] = dr["email"].ToString();
                            Session["UserName"] = dr["name"].ToString();
                            Session["uid"] = dr["id"];
                            conn.Close();
                            Response.Redirect("~/Customer/Default.aspx");
                        }
                    }
                    else
                    {
                        //ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript", "toastr.error('Invalid Password! Please Validate Password!')", true);
                        Response.Write("<script>alert('Invalid Password! Please validate!');</script>");
                        dr.Close();
                    }
                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript", "toastr.error('Your Account is Blocked by Admin!')", true);
                    Response.Write("<script>alert('Your Account is blocked!');</script>");
                    dr.Close();
                }
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "ErrorScript", "toastr.error('Email not Found! Please Varify Email OR Register First!')", true);
                Response.Write("<script>alert('Email not Found! Please verify Email OR Register First!');</script>");
                dr.Close();
            }
        }
    }
}
