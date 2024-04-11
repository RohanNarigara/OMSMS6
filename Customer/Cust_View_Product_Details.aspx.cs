using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OMSMS6.Customer
{
    public partial class CUst_View_All_Product : System.Web.UI.Page
    {
        String connection = "Data Source = LAPTOP-SHON9L4N\\SQLEXPRESS; Initial Catalog=omsms; Integrated Security=True;";
        int pid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    pid = int.Parse(Request.QueryString["id"]);
                    FetchProductDetailsFromDatabase(pid);
                    bindColor();
                    bindStorage(0);
                }
                else
                {
                    Response.Redirect("Cust_View_All_Product.aspx");
                }
            }
        }

        protected void FetchProductDetailsFromDatabase(int pid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Close();
            con.Open();
            SqlCommand selectProduct = new SqlCommand("SELECT * FROM tblProduct WHERE id = @id", con);
            selectProduct.Parameters.AddWithValue("@id", pid);
            SqlDataReader dr = selectProduct.ExecuteReader();
            if (dr.Read())
            {
                lblProductName.Text = dr["name"].ToString();
                //lblProductPrice.Text = dr["price"].ToString();
                //lblProductDescription.Text = dr["description"].ToString();
                imgProduct.ImageUrl = "../Res/Uploads/" + dr["imageName"].ToString();
            }
            dr.Close();
            con.Close();

        }
        protected void btnOrderNow_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Quantity must be a positive integer.');</script>");
        }

        protected void bindColor()
        {
            SqlConnection con = new SqlConnection(connection);
            con.Close();
            con.Open();
            SqlCommand selectColor = new SqlCommand("SELECT DISTINCT c.*, pd.cid FROM tblColor c LEFT JOIN tblProductDetail pd ON c.id = pd.cid WHERE pd.pid = @pid ORDER BY id ASC;", con);
            selectColor.Parameters.AddWithValue("@pid", pid);
            SqlDataReader dr = selectColor.ExecuteReader();
            if (dr.HasRows)
            {
                ddlColor.DataSource = dr;
                ddlColor.DataBind();
            }
            ddlColor.Items.Insert(0, new ListItem("-- Select Color --", ""));
            dr.Close();
            con.Close();
        }

        protected void bindStorage(int cid)
        {
            SqlConnection con = new SqlConnection(connection);
            con.Close();
            con.Open();
            if (cid > 0)
            {
                int pid = int.Parse(Request.QueryString["id"]);
                SqlCommand selectStorage = new SqlCommand("SELECT DISTINCT s.*, pd.sid FROM tblStorage s LEFT JOIN tblProductDetail pd ON s.id = pd.sid WHERE pd.pid = @pid AND pd.cid = @cid ORDER BY id ASC;", con);
                selectStorage.Parameters.AddWithValue("@pid", pid);
                selectStorage.Parameters.AddWithValue("@cid", cid);
                SqlDataReader dr = selectStorage.ExecuteReader();
                if (dr.HasRows)
                {
                    ddlStorage.DataSource = dr;
                    ddlStorage.DataBind();
                }
                ddlStorage.Items.Insert(0, new ListItem("-- Select Storage --", ""));
                dr.Close();
            }
            else
            {
                ddlStorage.Items.Clear();
                ddlStorage.Items.Insert(0, new ListItem("-- Select Color First --", ""));
            }
        }

        protected void ddlColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cid = 0;
            if (!string.IsNullOrEmpty(ddlColor.SelectedValue) && int.TryParse(ddlColor.SelectedValue, out cid) && cid > 0)
            {
                cid = Convert.ToInt32(ddlColor.SelectedValue);
                SqlConnection con = new SqlConnection(connection);
                con.Close();
                con.Open();
                int pid = int.Parse(Request.QueryString["id"]);
                SqlCommand selectStorage = new SqlCommand("SELECT DISTINCT s.*, pd.sid FROM tblStorage s LEFT JOIN tblProductDetail pd ON s.id = pd.sid WHERE pd.pid = @pid AND pd.cid = @cid ORDER BY id ASC;", con);
                selectStorage.Parameters.AddWithValue("@pid", pid);
                selectStorage.Parameters.AddWithValue("@cid", cid);
                SqlDataReader dr = selectStorage.ExecuteReader();
                if (dr.HasRows)
                {
                    bindStorage(cid);
                }
                else
                {
                    ddlStorage.Items.Clear();
                    ddlStorage.Items.Insert(0, new ListItem("-- Not Available --", ""));

                }
            }
            else
            {
                bindStorage(0);
            }
        }

        protected void ddlStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sid = 0; int cid = 0;
            if (!string.IsNullOrEmpty(ddlStorage.SelectedValue) && int.TryParse(ddlStorage.SelectedValue, out sid) && sid > 0)
            {
                sid = Convert.ToInt32(ddlStorage.SelectedValue);
                cid = Convert.ToInt32(ddlColor.SelectedValue);
                SqlConnection con = new SqlConnection(connection);
                con.Close();
                con.Open();
                int pid = int.Parse(Request.QueryString["id"]);
                SqlCommand selectPrice = new SqlCommand("SELECT * FROM tblProductDetail WHERE pid = @pid AND cid = @cid AND sid = @sid", con);
                selectPrice.Parameters.AddWithValue("@pid", pid);
                selectPrice.Parameters.AddWithValue("@cid", cid);
                selectPrice.Parameters.AddWithValue("@sid", sid);
                SqlDataReader dr = selectPrice.ExecuteReader();
                if (dr.Read())
                {
                    lblProductPrice.Text = "₹" + dr["price"].ToString();
                    lblProductDescription.Text = dr["description"].ToString();
                }
                else
                {
                    lblProductPrice.Text = "Not Available";
                    lblProductDescription.Text = "Not Available";
                }
                dr.Close();
                con.Close();
            }
            else
            {
                lblProductPrice.Text = "Not Available";
                lblProductDescription.Text = "Not Available";
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["uid"] == null)
            {
                Response.Write("<script>alert('Please login to add product to cart!');</script>");
            }
            else
            {
                int productId = int.Parse(Request.QueryString["id"]);

                int quantity = int.Parse(txtCount.Text);
                int price = int.Parse(lblProductPrice.Text); // Placeholder for product price
                int customerId = (int)Session["uid"]; // Assuming the customer ID is 1
                int total = price * quantity;

                try
                {
                    //String constr = "Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;";
                    //using (SqlConnection con = new SqlConnection(constr))
                    using (SqlConnection con = new SqlConnection(connection))
                    {
                        con.Open();
                        string query = "INSERT INTO tblCartProduct (Pid, Quantity, Total, Custid) VALUES (" + productId + "," + quantity + "," + total + "," + customerId + ")";
                        SqlCommand cmd = new SqlCommand(query, con);
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            // Show success alert and redirect
                            Response.Write("<script>alert('Product added to cart successfully!  '  " + quantity + "); </script>");
                        }
                        else
                        {
                            // Show error alert
                            Response.Write("<script>alert('An error occurred while adding the product to cart');</script>");
                        }
                        con.Close(); // Close connection after executing the command
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Response.Write("<script>alert('An error occurred while adding the product to cart: " + ex.Message + "');</script>");
                }
            }
        }
    }
}