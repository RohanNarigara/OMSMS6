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
        //String connection = "Data Source=Vishvas;Initial Catalog=OMSMS;Integrated Security=True;";
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
                    lblProductPrice.Text = dr["price"].ToString();
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
                SqlConnection con = new SqlConnection(connection);
                con.Close();
                con.Open();
                int pid = int.Parse(Request.QueryString["id"]);
                int quantity = txtCount.Text == "" ? 0 : Convert.ToInt32(txtCount.Text);
                int cid = Convert.ToInt32(ddlColor.SelectedValue);
                int sid = Convert.ToInt32(ddlStorage.SelectedValue);



                SqlCommand selectPid = new SqlCommand("SELECT * FROM tblProductDetail WHERE pid = @pid AND cid = @cid AND sid = @sid", con);
                selectPid.Parameters.AddWithValue("@pid", pid);
                selectPid.Parameters.AddWithValue("@cid", cid);
                selectPid.Parameters.AddWithValue("@sid", sid);
                SqlDataReader dr = selectPid.ExecuteReader();
                if (dr.Read())
                {
                    int pdid = Convert.ToInt32(dr["id"]);
                    //Response.Write("<script>alert('" + pdid + "');</script>");

                    dr.Close();
                    if (quantity > 0)
                    {
                        SqlCommand checkCart = new SqlCommand("SELECT * FROM tblCartProduct WHERE Custid = @Custid AND Pid = @Pid", con);
                        checkCart.Parameters.AddWithValue("@Pid", pdid);
                        checkCart.Parameters.AddWithValue("@Custid", Session["uid"]);
                        SqlDataReader drCart = checkCart.ExecuteReader();
                        if (drCart.Read())
                        {
                            drCart.Close();
                            SqlCommand updateCart = new SqlCommand("UPDATE tblCartProduct SET Quantity = Quantity + @Quantity, Total = @Total WHERE Custid = @Custid AND Pid = @Pid", con);
                            updateCart.Parameters.AddWithValue("@Custid", Session["uid"]);
                            updateCart.Parameters.AddWithValue("@Pid", pdid);
                            updateCart.Parameters.AddWithValue("@Quantity", quantity);
                            updateCart.Parameters.AddWithValue("@Total", quantity * Convert.ToInt32(lblProductPrice.Text));
                            int isupdated = updateCart.ExecuteNonQuery();
                            if (isupdated > 0)
                            {
                                Response.Write("<script>alert('Product update to cart successfully!');</script>");
                                con.Close();
                            }
                            else
                            {
                                Response.Write("<script>alert('Failed to add product to cart!');</script>");
                            }
                            con.Close();
                        }
                        else
                        {
                            drCart.Close();
                            SqlCommand insertCart = new SqlCommand("INSERT INTO tblCartProduct (Custid, Pid, Quantity, Total) VALUES (@Custid, @Pid, @Quantity, @Total)", con);
                            insertCart.Parameters.AddWithValue("@Custid", Session["uid"]);
                            insertCart.Parameters.AddWithValue("@Pid", pdid);
                            insertCart.Parameters.AddWithValue("@Quantity", quantity);
                            insertCart.Parameters.AddWithValue("@Total", quantity * Convert.ToInt32(lblProductPrice.Text));
                            int isInserted = insertCart.ExecuteNonQuery();
                            if (isInserted > 0)
                            {
                                Response.Write("<script>alert('Product added to cart successfully!');</script>");
                                con.Close();
                            }
                            else
                            {
                                Response.Write("<script>alert('Failed to add product to cart!');</script>");
                            }
                            con.Close();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Quantity must be a positive integer.');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Quantity must be a positive integer.');</script>");
                }
            }
        }
    }
}