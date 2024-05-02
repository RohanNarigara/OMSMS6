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

namespace OMSMS6.Admin
{
    public partial class Change_Password : System.Web.UI.Page
    {
        //SqlConnection conn = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["aid"] == null)
            {
                Response.Redirect("../Customer/Default.aspx");
            }
            else
            {
                txtEmail.Text = Session["AdminEmail"].ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            conn.Close();
            conn.Open();
            SqlCommand selectUser = new SqlCommand("SELECT * FROM tblUsers WHERE id = @aid", conn);
            selectUser.Parameters.AddWithValue("@aid", Session["aid"]);
            SqlDataReader dr = selectUser.ExecuteReader();
            if (dr.Read())
            {

                SqlCommand updatePassword = new SqlCommand("UPDATE tblUsers SET password = @password WHERE id = @aid", conn);

                SHA256 sha256 = SHA256.Create();
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(txtOldPassword.Text.ToString()));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    sb.Append(hashedBytes[i].ToString("x2"));
                }

                String hashedPassword = sb.ToString();

                if (hashedPassword == dr["password"].ToString())
                {
                    dr.Close();
                    SHA256 sha = SHA256.Create();
                    byte[] hashed = sha.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Text.ToString()));

                    StringBuilder sb1 = new StringBuilder();
                    for (int i = 0; i < hashed.Length; i++)
                    {
                        sb1.Append(hashed[i].ToString("x2"));
                    }

                    updatePassword.Parameters.AddWithValue("@password", sb1.ToString());
                    updatePassword.Parameters.AddWithValue("@aid", Session["aid"]);
                    int isUpdated = updatePassword.ExecuteNonQuery();
                    conn.Close();
                    if (isUpdated > 0)
                    {
                        Session.RemoveAll();
                        Session["AdminEmail"] = null;
                        Session["AdminName"] = null;
                        Response.Write("<script>alert('Password Changed Successfully!'); window.location='../Customer/Default.aspx'</script>");
                        //Response.Redirect("../Customer/Default.aspx");
                    }
                    else
                    {
                        dr.Close();
                        conn.Close();
                        Response.Write("<script>alert('Error in Changing Password!'); </script>");
                        //Response.Write("<script>toastr.error('Error in Changing Password!')</script>");
                    }
                    //Response.Redirect("../Customer/Default.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Old Password is Incorrect!')</script>");
                    return;
                }
            }
            else
            {
                dr.Close();
                conn.Close();
                //Response.Write("<script>toastr.error('Email not Found! Please check it once!')</script>");
                Response.Write("<script>alert('Email not Found! Please check it once!');</script>");
            }
        }
    }
}