using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace OMSMS6.Customer
{
    public partial class Checkout : System.Web.UI.Page
    {

        //SqlConnection con = new SqlConnection("Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;");
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-SHON9L4N\\SQLEXPRESS;Initial Catalog=omsms;Integrated Security=True;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["uid"] == null)
                {
                    Response.Write("<script>alert('Please Login First'); window.location='../Customer/Default.aspx'</script>");
                }
                else
                {
                    LoadCart();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["cartid"]))
                {
                    int cartId;
                    if (int.TryParse(Request.QueryString["cartid"], out cartId))
                    {
                        DeletecartItem(cartId);
                    }
                }

                if (!String.IsNullOrEmpty(Request.QueryString["cartmid"]))
                {
                    int cartmId;
                    if (int.TryParse(Request.QueryString["cartmid"], out cartmId))
                    {
                        UpdatecartMItem(cartmId);
                    }
                }

                if (!String.IsNullOrEmpty(Request.QueryString["cartpid"]))
                {
                    int cartpId;
                    if (int.TryParse(Request.QueryString["cartpid"], out cartpId))
                    {
                        UpdatecartPItem(cartpId);
                    }
                }
            }
        }

        public void LoadCart()
        {
            con.Open();


            String uid = Session["uid"].ToString();
            //String uid = 1.ToString();

            SqlCommand cmd = new SqlCommand("SELECT CP.Id, P.Name AS ProductName, P.ImageName, PD.Price, CP.Quantity FROM tblCartProduct CP JOIN tblProduct P ON CP.Pid = P.Id JOIN tblProductDetail PD ON CP.Pid = PD.Pid WHERE CP.Custid =" + uid + ";", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                viewcartlist.DataSource = dt;
                viewcartlist.DataBind();
                decimal totalAmount = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["Price"]) * row.Field<int>("Quantity"));
                lbltotal.Text = string.Format("&#8377;{0}.00", totalAmount);

            }
            else
            {
                Response.Write("<script>alert('Cart is Empty !!!'); window.location='../Customer/Default.aspx';</script>");
                //ScriptManager.RegisterStartupScript(this, GetType(), "showToastdanget", "showToastdanget('Empty Cart !!!');", true);
                //lbltotal.Visible = false; // Hide total amount label
                //viewcartlist.Visible = false; // Hide repeater
                //emtycart.Visible = false;
                //checkout.Visible = false;

            }
            con.Close();
        }

        private void UpdatecartMItem(int cartmId)
        {
            con.Open();
            using (SqlCommand cmdSelect = new SqlCommand("SELECT Quantity FROM tblCartProduct WHERE Id =" + cartmId + ";", con))
            {
                cmdSelect.Parameters.AddWithValue("@cartmid", cartmId);
                int currentQuantity = Convert.ToInt32(cmdSelect.ExecuteScalar());

                // Check if current quantity is greater than 1 before decrementing
                if (currentQuantity > 1)
                {
                    using (SqlCommand cmdUpdate = new SqlCommand("UPDATE tblCartProduct SET Quantity = Quantity - 1 WHERE Id = @cartmid", con))
                    {
                        cmdUpdate.Parameters.AddWithValue("@cartmid", cartmId);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (SqlCommand cmdDelete = new SqlCommand("delete from tblCartProduct WHERE Id = @cartmid", con))
                    {
                        cmdDelete.Parameters.AddWithValue("@cartmid", cartmId);
                        cmdDelete.ExecuteNonQuery();
                    }
                }
            }

            con.Close();

            // Redirect back to the cart page
            Response.Redirect("cart.aspx");
        }

        private void UpdatecartPItem(int cartpId)
        {

            con.Open();
            using (SqlCommand cmd = new SqlCommand("update tblCartProduct set Quantity=Quantity+1 WHERE Id = @cartpid", con))
            {
                cmd.Parameters.AddWithValue("@cartpid", cartpId);
                cmd.ExecuteNonQuery();
            }


            // Redirect back to the product page after deletings
            Response.Redirect("cart.aspx");
        }

        private void DeletecartItem(int cartId)
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM tblCartProduct WHERE Id = @cartid", con))
            {
                cmd.Parameters.AddWithValue("@cartid", cartId);
                cmd.ExecuteNonQuery();
            }


            // Redirect back to the product page after deletings
            Response.Redirect("cart.aspx");
        }




        protected void emtycart_Click(object sender, EventArgs e)
        {
            con.Open();
            /*            String u_id = Session["u_id"].ToString();*/
            String u_id = Session["uid"].ToString();
            using (SqlCommand cmd = new SqlCommand("DELETE FROM tblCartProduct WHERE CustId =" + u_id, con))
            {
                cmd.ExecuteNonQuery();
            }


            // Redirect back to the product page after deletings
            Response.Redirect("cart.aspx");
        }

        protected void checkout_Click(object sender, EventArgs e)
        {
            String chek = "checkout";
            Session["checkout"] = chek;
            Response.Redirect("Customer_Checkout.aspx");
        }
    }
}