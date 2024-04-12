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
                        DeletecartItem(cartpId);
                    }
                }
            }
        }

        public void LoadCart()
        {
            con.Open();
            String uid = Session["uid"].ToString();
            SqlCommand selectItem = new SqlCommand("SELECT p.name as pname, p.imageName, c.name as cname, s.storage as storage, pd.price, cp.Quantity, cp.Total, cp.Id FROM tblCartProduct cp INNER JOIN tblProductDetail pd ON cp.Pid = pd.id INNER JOIN tblProduct p ON pd.Pid = p.id INNER JOIN tblColor c ON pd.cid = c.id INNER JOIN tblStorage s ON pd.sid = s.id WHERE cp.CustId = @uid", con);
            selectItem.Parameters.AddWithValue("@uid", uid);
            SqlDataReader reader = selectItem.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                viewcartlist.DataSource = dt;
                viewcartlist.DataBind();

                decimal totalAmount = dt.AsEnumerable().Sum(row => Convert.ToDecimal(row["price"]) * row.Field<int>("Quantity"));
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
            SqlCommand cmdSelect = new SqlCommand("SELECT pd.price, cp.Quantity FROM tblCartProduct cp INNER JOIN tblProductDetail pd ON cp.Pid = pd.id WHERE cp.Id = @cartmid", con);
            cmdSelect.Parameters.AddWithValue("@cartmid", cartmId);
            SqlDataReader dr = cmdSelect.ExecuteReader();
            if (dr.Read())
            {
                int currentQuantity = Convert.ToInt32(dr["Quantity"]);
                decimal price = Convert.ToDecimal(dr["Price"]);
                dr.Close();
                // Check if current quantity is greater than 1 before decrementing
                if (currentQuantity > 1)
                {
                    SqlCommand cmdUpdate = new SqlCommand("UPDATE tblCartProduct SET Quantity = Quantity - 1, Total = @Total WHERE Id = @cartmid", con);
                    cmdUpdate.Parameters.AddWithValue("@cartmid", cartmId);
                    cmdUpdate.Parameters.AddWithValue("@Total", (currentQuantity - 1) * price);
                    cmdUpdate.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmdDelete = new SqlCommand("delete from tblCartProduct WHERE Id = @cartmid", con);
                    cmdDelete.Parameters.AddWithValue("@cartmid", cartmId);
                    cmdDelete.ExecuteNonQuery();
                }
            }
            dr.Close();
            con.Close();

            // Redirect back to the cart page
            Response.Redirect("cart.aspx");
        }

        private void UpdatecartPItem(int cartpId)
        {

            con.Open();
            SqlCommand cmdSelect = new SqlCommand("SELECT pd.price, cp.Quantity FROM tblCartProduct cp INNER JOIN tblProductDetail pd ON cp.Pid = pd.id WHERE cp.Id = @cartpId", con);
            cmdSelect.Parameters.AddWithValue("@cartpId", cartpId);
            SqlDataReader dr = cmdSelect.ExecuteReader();
            if (dr.Read())
            {
                int currentQuantity = Convert.ToInt32(dr["Quantity"]);
                decimal price = Convert.ToDecimal(dr["Price"]);
                dr.Close();
                SqlCommand cmdUpdate = new SqlCommand("UPDATE tblCartProduct SET Quantity = Quantity + 1, Total = @Total WHERE Id = @cartpId", con);
                cmdUpdate.Parameters.AddWithValue("@cartpId", cartpId);
                cmdUpdate.Parameters.AddWithValue("@Total", (currentQuantity + 1) * price);
                cmdUpdate.ExecuteNonQuery();
            }

            // Redirect back to the product page after deletings
            Response.Redirect("cart.aspx");
        }

        private void DeletecartItem(int cartId)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM tblCartProduct WHERE Id = @cartid", con);
            cmd.Parameters.AddWithValue("@cartid", cartId);
            cmd.ExecuteNonQuery();
            // Redirect back to the product page after deletings
            Response.Redirect("cart.aspx");
        }

        protected void emtycart_Click(object sender, EventArgs e)
        {
            con.Open();
            String uid = Session["uid"].ToString();
            SqlCommand cmd = new SqlCommand("DELETE FROM tblCartProduct WHERE CustId = @uid", con);
            cmd.Parameters.AddWithValue("@uid", uid);
            cmd.ExecuteNonQuery();
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